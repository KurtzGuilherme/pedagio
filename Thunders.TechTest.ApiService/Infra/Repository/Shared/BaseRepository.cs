using Microsoft.EntityFrameworkCore;
using Thunders.TechTest.ApiService.Domain.Exceptions;
using Thunders.TechTest.ApiService.Infra.Data.SqlServer;
using Thunders.TechTest.ApiService.Infra.Repository.Interface.Shared;

namespace Thunders.TechTest.ApiService.Infra.Repository.Shared;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    internal SqlServerContext dbContext;
    internal DbSet<TEntity> DbSet;

    public BaseRepository(SqlServerContext defaultContext)
    {
        dbContext = defaultContext;
        DbSet = dbContext.Set<TEntity>();
    }

    public async Task AddAsync(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullOrEmptyException(nameof(entity));
        
        await DbSet.AddAsync(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<TEntity>> GetAllAsync()
        => await DbSet.ToListAsync();

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        if (id == 0)
            throw new ArgumentNullOrEmptyException(nameof(id));

      return  await DbSet.FindAsync(id);
    }

    public async Task<bool> Delete(int id)
    {
        if (id == 0)
            throw new ArgumentNullOrEmptyException(nameof(id));

        var entity = await DbSet.FindAsync(id);

        if (entity != null)
        {
            DbSet.Remove(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task UpdateAsync(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullOrEmptyException(nameof(entity));

        DbSet.Update(entity);
        await dbContext.SaveChangesAsync();
    }
}
