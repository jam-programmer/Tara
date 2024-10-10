namespace Application.Contracts
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TModel> GetByIdAsync<TModel>(Expression<Func<TEntity, bool>>
            condition, TypeAdapterConfig? config = null);

        Task<List<TModel>> GetListAsync<TModel>(Expression<Func<TEntity, bool>>
            condition, TypeAdapterConfig? config = null); 
        
        Task<TModel> GetAsync<TModel>(Expression<Func<TEntity, bool>>
            condition, TypeAdapterConfig? config = null, CancellationToken cancellation = default); 
        
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>>
            condition, CancellationToken cancellation = default);

        Task InsertAsync(TEntity entity, CancellationToken cancellation = default);
        Task InsertAsync(List<TEntity> entities, CancellationToken cancellation = default);
        Task UpdateAsync(TEntity entity, CancellationToken cancellation = default);
        Task UpdateAsync(List<TEntity> entities, CancellationToken cancellation = default);


        Task<bool> ExistAsync(Expression<Func<TEntity, bool>>
            condition, CancellationToken cancellation = default);


        Task DeleteAsync(TEntity entity,CancellationToken cancellation=default);
        Task DeleteAsync(List<TEntity> entities, CancellationToken cancellation=default);


        Task<IQueryable<TEntity>> GetByQueryAsync();


    }
}
