


namespace Infrastructure.Repository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly SqlServerContext _context;
    public Repository(SqlServerContext context)
    {
        _context = context;
    }

    private async Task SaveChanges(CancellationToken cancellation = default) => await _context.SaveChangesAsync(cancellation);


    public async Task DeleteAsync(TEntity entity, CancellationToken cancellation = default)
    {
        _context.Remove(entity);
        await SaveChanges(cancellation);
    }

    public async Task DeleteAsync(List<TEntity> entities, CancellationToken cancellation = default)
    {
        _context.RemoveRange(entities);
        await SaveChanges(cancellation);
    }

    public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellation = default)
    {
        return await _context.Set<TEntity>().AnyAsync(condition);
    }

    public async Task<TModel> GetByIdAsync<TModel>
        (Expression<Func<TEntity, bool>> condition, TypeAdapterConfig? config = null)
    {
        return (await _context.Set<TEntity>().SingleOrDefaultAsync(condition)).Adapt<TModel>(config);
    }

    public async Task<List<TModel>> GetListAsync<TModel>
        (Expression<Func<TEntity, bool>> condition, TypeAdapterConfig? config = null)
    {
        var entities = await _context.Set<TEntity>().Where(condition).ToListAsync();
        return entities.Adapt<List<TModel>>(config);
    }







    public async Task InsertAsync(TEntity entity, CancellationToken cancellation = default)
    {
        await _context.Set<TEntity>().AddAsync(entity, cancellation);
        await SaveChanges(cancellation);
    }

    public async Task InsertAsync(List<TEntity> entities, CancellationToken cancellation = default)
    {
        await _context.Set<TEntity>().AddRangeAsync(entities, cancellation);
        await SaveChanges(cancellation);
    }

    public Task<IQueryable<TEntity>> GetByQueryAsync()
    {
        return Task.FromResult(_context.Set<TEntity>().AsQueryable());
    }


    public async Task UpdateAsync(TEntity entity, CancellationToken cancellation = default)
    {
       _context.Update(entity);
        await SaveChanges(cancellation);
    }

    public async Task UpdateAsync(List<TEntity> entities, CancellationToken cancellation = default)
    {
        _context.UpdateRange(entities);
        await SaveChanges(cancellation);
    }

    public async Task<TModel> GetAsync<TModel>(Expression<Func<TEntity, bool>> condition, TypeAdapterConfig? config = null, CancellationToken cancellation = default)
    {
        return (await _context.Set<TEntity>().SingleOrDefaultAsync(condition, cancellation)).Adapt<TModel>(config);    ;
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellation = default)
    {
        return await _context.Set<TEntity>().SingleOrDefaultAsync(condition, cancellation);
    }
}
