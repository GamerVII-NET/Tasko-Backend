using Tasko.Domains.Models.DTO.User;

namespace Tasko.Service.Infrastructure.Repositories;

internal class UserRepository : BaseUserRepository, IRepository<IUser, IUserCreate, IUserUpdate>
{
    public UserRepository(IMongoDatabase mongoDatabase, ValidationParameter validationParameter) : base(mongoDatabase, validationParameter) { }
    public async Task<IEnumerable<IUser>> GetAsync() => await UserCollection.Find(_ => true).ToListAsync();
    public async Task<IUser> FindAsync(Guid id) => await UserCollection.Find(u => u.Id == id).SingleOrDefaultAsync();
    public async Task<IUser> CreateAsync(IUserCreate dtoCreateModel) => throw new NotImplementedException();
    public async Task<IUser> UpdateAsync(IUserUpdate dtoUpdateModel) => throw new NotImplementedException();
    public async Task DeleteAsync(Guid guid) => throw new NotImplementedException();
}