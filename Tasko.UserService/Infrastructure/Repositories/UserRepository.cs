using Tasko.Domains.Models.Structural.Providers;
using MongoDB.Driver;
using Tasko.General.Interfaces;

namespace Tasko.UserService.Infrasructure.Repositories
{
    #region Interfaces
    public interface IUserRepository
    {
        Task<IEnumerable<IUser>> GetUsersAsync();
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(Guid id);
        Task<IUser> FindUserAsync(Guid id);
        Task<IUser> FindUserAsync(string login);
        Task<IUser> FindUserAsync(string login, string password);
    }
    #endregion

    #region Abstracts
    public class UserRepositoryBase
    {
        public UserRepositoryBase(IMongoDatabase databaseContext)
        {
            Filter = Builders<User>.Filter;
            UserCollection = databaseContext.GetCollection<User>("Users");
        }

        internal IMongoCollection<User> UserCollection { get; set; }

        internal FilterDefinitionBuilder<User> Filter { get; }
    }

    #endregion

    #region Providers
    public class UserRepository : UserRepositoryBase, IUserRepository
    {
        #region Constructors
        public UserRepository(IMongoDatabase databaseContext) : base(databaseContext) { }
        #endregion

        #region Methods

        #region Find
        public async Task<IUser> FindUserAsync(Guid id) => await UserCollection.Find(u => u.Id == id).SingleOrDefaultAsync();
        public async Task<IUser> FindUserAsync(string login)
        {
            var filter = Filter.Eq("Login", login);
            return await UserCollection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<IUser> FindUserAsync(string login, string password)
        {
            var loginFilter = Filter.Eq(x => x.Login, login);
            var passwordFilter = Filter.Eq(x => x.Password, password);
            var combineFilters = Filter.And(loginFilter, passwordFilter);
            return await UserCollection.Find(combineFilters).FirstOrDefaultAsync();
        }
        #endregion

        public async Task<IEnumerable<IUser>> GetUsersAsync() => await UserCollection.Find(_ => true).ToListAsync();
        public async Task CreateUserAsync(User user) => await UserCollection.InsertOneAsync(user);
        public async Task UpdateUserAsync(User user)
        {
            var filter = Filter.Eq("Id", user.Id);
            await UserCollection.ReplaceOneAsync(filter, user);
        }
        public async Task DeleteUserAsync(Guid id) => await UserCollection.DeleteOneAsync(c => c.Id == id);
        #endregion
    } 
    #endregion
}