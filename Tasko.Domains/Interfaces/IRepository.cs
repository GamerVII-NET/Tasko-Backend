namespace Tasko.Domains.Interfaces
{
    public interface IRepository<Model, DtoCreateModel, DtoUpdateModel> 
    where Model : class
    where DtoCreateModel : class
    where DtoUpdateModel : class
    {
        public Task<Model> FindAsync(Guid id);
        public Task<Model> CreateAsync(DtoCreateModel dtoCreateModel);
        public Task<Model> UpdateAsync(DtoUpdateModel dtoUpdateModel);
        public Task DeleteAsync(Guid guid);
        public Task<IEnumerable<Model>> GetAsync();
    }
}
