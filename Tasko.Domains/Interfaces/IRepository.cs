using System.Linq.Expressions;

namespace Tasko.Domains.Interfaces;

public interface IRepository<Interface, Model>
where Model : class
{
    Task<Interface> FindOneAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Interface> FindOneAsync(Expression<Func<Model, bool>> expression, CancellationToken cancellationToken = default);
    Task<IEnumerable<Interface>> FindManyAsync(Expression<Func<Model, bool>> expression, CancellationToken cancellationToken = default);
    Task<Interface> CreateAsync(Model model, CancellationToken cancellationToken = default);
    Task<Interface> UpdateAsync(Model model, CancellationToken cancellationToken = default);
    Task<Interface> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Interface>> GetAsync(CancellationToken cancellationToken = default);
}