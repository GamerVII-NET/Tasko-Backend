namespace Tasko.Domains.Interfaces;

public interface IRepository<Model>
where Model : class
{
    Task<Model> FindAsync(Guid id, CancellationToken cancellationToken);
    Task CreateAsync(Model model, CancellationToken cancellationToken);
    Task<Model> UpdateAsync(Model model, CancellationToken cancellationToken);
    Task DeleteAsync(Guid guid, CancellationToken cancellationToken);
    Task<IEnumerable<Model>> GetAsync(CancellationToken cancellationToken);
}