using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Net;
using System.Threading;
using Tasko.Domains.Models.RequestResponse;
using Tasko.Domains.Models.RequestResponses;
using Tasko.Domains.Models.Structural;
using Tasko.Jwt.Models;
using Tasko.Jwt.Services;

namespace Tasko.AuthService.Infrastructure.Repositories
{
    public class AuthRepository : AuthRepositoryBase, IAuthRepository
    {
        public AuthRepository(IMongoDatabase databaseContext) : base(databaseContext) { }

        public async Task<IResult> RefreshTokenAuthorizationAsync(HttpContext context, IMapper mapper, ValidationParameter jwtValidationParameter, CancellationToken cancellationToken)
        {
            if (!context.Request.Cookies.ContainsKey("RefreshToken"))
            {
                var validationResponse = new List<ValidationFailure> {
                    new ValidationFailure("RefreshToken", "The required refresh token parameter is not set in the Cookie")
                };

                return Results.BadRequest(new BadRequestResponse<List<ValidationFailure>>(validationResponse, "The required refresh token parameter is not set in the Cookie!", StatusCodes.Status400BadRequest));
            }

            context.Request.Cookies.TryGetValue("RefreshToken", out string token);

            var filter = RefreshTokenFilter.Eq("Token", token);

            var refreshToken = await RefreshTokensCollection.Find(filter).FirstOrDefaultAsync(cancellationToken);

            if (refreshToken is null || !refreshToken.IsActive)
            {
                var resposne = new List<ValidationFailure> {
                    new ValidationFailure("RefreshToken", "The specified Refresh Token was not found or is invalid!")
                };
                return Results.BadRequest(new BadRequestResponse<List<ValidationFailure>>(resposne, "The specified Refresh Token was not found or is invalid!", StatusCodes.Status400BadRequest));
            }

            var userFilter = UserFilter.Eq("Id", refreshToken.UserId);
            var user = await UserCollection.Find(userFilter).FirstOrDefaultAsync();

            if (user is null || user.IsDeleted)
            {

                var deletedValidationResponse = new List<ValidationFailure> {
                    new ValidationFailure("RefreshToken", "The token owner has not been found or blocked!")
                };

                return Results.BadRequest(new BadRequestResponse<List<ValidationFailure>>(deletedValidationResponse, "The token owner has not been found or blocked!", StatusCodes.Status400BadRequest));
            }

            var newRefreshToken = JwtServices.CreateRefreshToken();

            refreshToken.RevokedAt = DateTime.UtcNow;
            refreshToken.RevokedByIp = GetRealIpAddress(context.Connection.RemoteIpAddress);
            refreshToken.ReasonRevoked = $"Attempted reuse of revoked ancestor token: {newRefreshToken}";

            await RefreshTokensCollection.ReplaceOneAsync(filter, refreshToken, cancellationToken: cancellationToken);

            await SaveRefreshToken(user, newRefreshToken, GetRealIpAddress(context.Connection.RemoteIpAddress), cancellationToken);
            var userPermissions = await GetUserPermissions(user, cancellationToken);
            var userRolesPermissions = await GetUserRolesPermissions(user, cancellationToken);
            var permissions = userPermissions.Concat(userRolesPermissions).ToList();
            string accessToken = JwtServices.CreateToken(jwtValidationParameter, user, permissions);

            context.Response.Cookies.Append("RefreshToken", newRefreshToken, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Unspecified
            });

            var response = new UserAuthRead
            {
                User = mapper.Map<IUser, UserRead>(user),
                Token = accessToken
            };

            return Results.Ok(new RequestResponse<UserAuthRead>(response, StatusCodes.Status200OK));
        }

        public async Task<IResult> AuthorizationAsync(UserAuth userAuth, ValidationParameter jwtValidationParameter, IMapper mapper, IValidator<IUserAuth> validator, IPAddress ipAddress, IResponseCookies cookies, CancellationToken cancellationToken)
        {
            #region Validate user
            var validationResult = validator.Validate(userAuth);

            if (!validationResult.IsValid)
            {
                var result = new BadRequestResponse<List<ValidationFailure>>(validationResult.Errors, "Ошибка валидации данных");
                return Results.BadRequest(result);
            }
            #endregion

            #region Check user
            var user = await FindUserAsync(userAuth.Login) as User;

            if (user == null)
            {
                var resposne = new List<ValidationFailure> {
                    new ValidationFailure("Password", "Неверный логин или пароль")
                };
                return Results.BadRequest(new BadRequestResponse<List<ValidationFailure>>(resposne, "Пользователь с указанными данными не сущуствует!", StatusCodes.Status409Conflict));
            }
            #endregion

            #region Validate password
            var validatePassword = BCrypt.Net.BCrypt.Verify(userAuth.Password, user.Password);
            if (!validatePassword)
            {
                var passwordValidationResponse = new List<ValidationFailure> {
                    new ValidationFailure("Response", "Неверный логин или пароль")
                };
                return Results.BadRequest(new BadRequestResponse<List<ValidationFailure>>(passwordValidationResponse, "Неверный логин или пароль", StatusCodes.Status401Unauthorized));

            }
            #endregion

            #region Generate data

            string refreshToken = JwtServices.CreateRefreshToken();
            var userPermissions = await GetUserPermissions(user, cancellationToken);
            var userRolesPermissions = await GetUserRolesPermissions(user, cancellationToken);
            var permissions = userPermissions.Concat(userRolesPermissions).ToList();
            string token = JwtServices.CreateToken(jwtValidationParameter, user, permissions);
            await SaveRefreshToken(user, refreshToken, GetRealIpAddress(ipAddress), cancellationToken);

            #endregion

            cookies.Append("RefreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Unspecified
            });

            var response = new UserAuthRead
            {
                User = mapper.Map<IUser, UserRead>(user),
                Token = token
            };

            return Results.Ok(new RequestResponse<UserAuthRead>(response, StatusCodes.Status200OK));
        }
        
        #region Other methods
        public async Task<IEnumerable<IRole>> GetUserRoles(User user, CancellationToken cancellationToken)
        {
            var userRolesIdFilter = Builders<UserRole>.Filter.Eq(d => d.UserId, user.Id);
            var userRoles = await UserRolesCollection.Find(userRolesIdFilter).ToListAsync(cancellationToken);
            var rolesIdFilter = Builders<Role>.Filter.In(d => d.Id, userRoles.Select(c => c.RoleId));
            return await RolesCollection.Find(rolesIdFilter).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<IPermission>> GetUserRolesPermissions(User user, CancellationToken cancellationToken)
        {
            var roles = await GetUserRoles(user, cancellationToken);
            var rolePermissionsIdFilter = Builders<RolePermission>.Filter.In(d => d.RoleId, roles.Select(c => c.Id));
            var rolePermissions = await RolePermissionsCollection.Find(rolePermissionsIdFilter).ToListAsync(cancellationToken);
            var permissionsIdFilter = Builders<Permission>.Filter.In(d => d.Id, rolePermissions.Select(c => c.PermissionId));
            return await PermissionCollection.Find(permissionsIdFilter).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<IPermission>> GetUserPermissions(User user, CancellationToken cancellationToken)
        {

            var userPermissionsIdFilter = Builders<UserPermission>.Filter.Eq(d => d.UserId, user.Id);
            var userPermissions = await UserPermissionsCollection.Find(userPermissionsIdFilter).ToListAsync(cancellationToken);
            var permissionsIdFilter = Builders<Permission>.Filter.In(d => d.Id, userPermissions.Select(c => c.PermissionId));
            return await PermissionCollection.Find(permissionsIdFilter).ToListAsync(cancellationToken);
        }

        public async Task<IUser> FindUserAsync(string login)
        {
            var filter = UserFilter.Eq("Login", login);
            return await UserCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task SaveRefreshToken(User user, string refreshToken, string ipAddress, CancellationToken cancellationToken)
        {
            RefreshToken token = new RefreshToken
            {
                Token = refreshToken,
                IssuedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(30),
                UserId = user.Id,
                CreatedByIp = ipAddress
            };

            await RefreshTokensCollection.InsertOneAsync(token, cancellationToken: cancellationToken);
        }

        internal static string GetRealIpAddress(IPAddress address)
        {
            if (address != null && address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
            {
                return Dns.GetHostEntry(address).AddressList.First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToString();
            }

            return string.Empty;
        } 
        #endregion

    }
}
