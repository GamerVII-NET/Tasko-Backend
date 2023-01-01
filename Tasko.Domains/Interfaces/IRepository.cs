using System.Linq.Expressions;

namespace Tasko.Domains.Interfaces;

public interface IRepository<Model>
where Model : class
{
    Task<Model> FindOneAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Model> FindOneAsync(Expression<Func<Model, bool>> expression, CancellationToken cancellationToken = default);
    Task<IEnumerable<Model>> FindManyAsync(Expression<Func<Model, bool>> expression, CancellationToken cancellationToken = default);
    Task<Model> CreateAsync(Model model, CancellationToken cancellationToken = default);
    Task<Model> UpdateAsync(Model model, CancellationToken cancellationToken = default);
    Task<Model> DeleteAsync(Guid guid, CancellationToken cancellationToken = default);
    Task<IEnumerable<Model>> GetAsync(CancellationToken cancellationToken = default);
}