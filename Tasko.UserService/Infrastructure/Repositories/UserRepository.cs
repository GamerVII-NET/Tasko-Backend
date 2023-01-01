using Tasko.Domains.Models.DTO.User;
using Tasko.Domains.Models.Structural;

namespace Tasko.Service.Infrastructure.Repositories;

internal class UserRepository : BaseUserRepository, IUserRepository
{
    public UserRepository(IMongoDatabase mongoDatabase, ValidationParameter validationParameter) : base(mongoDatabase, validationParameter) { }
    public async Task<IEnumerable<IUser>> GetAsync(CancellationToken cancellationToken) => await UserCollection.Find(_ => true).ToListAsync(cancellationToken);
    public async Task<IUser> FindAsync(Guid id, CancellationToken cancellationToken) => await UserCollection.Find(u => u.Id == id).SingleOrDefaultAsync(cancellationToken);
    public async Task CreateAsync(IUser model, CancellationToken cancellationToken) => await UserCollection.InsertOneAsync(model, cancellationToken);
    public async Task<IUser> UpdateAsync(IUser model, CancellationToken cancellationToken) => throw new NotImplementedException();
    public async Task DeleteAsync(Guid guid, CancellationToken cancellationToken) => throw new NotImplementedException();
}