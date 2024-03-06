using System.Linq.Expressions;

namespace BookLoansManager.Core.Interfaces;

public interface IDbService
{
    public void IncludeNavigations<TEntity>() where TEntity : class;
    public Task<TEntity?> GetSingleAsync<TEntity>(Expression<Func<TEntity, bool>> expression) 
        where TEntity : class;
    public Task<TDto> GetSingleAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression, bool include = false)
        where TEntity : class where TDto : class;
    public Task<List<TDto>> GetAsync<TEntity, TDto>(bool include = false) where TEntity : class where TDto : class;
    public Task<TEntity> AddAsync<TEntity, TDto>(TDto dto) where TEntity : class where TDto : class;
    public Task<bool> UpdateAsync<TEntity>(Expression<Func<TEntity, bool>> expression, Action<TEntity> updateAction) where TEntity : class, IEntity;
    public Task<bool> DeleteAsync<TEntity>(int id) where TEntity : class, IEntity;
    public Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class;
    public Task<bool> SaveAsync();
}