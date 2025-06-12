namespace Thunders.TechTest.ApiService.Infra.Repository.Interface.Shared;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task AddAsync(TEntity entity);
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(int id);
    Task UpdateAsync(TEntity entity);
    Task<bool> Delete(int id);
}
