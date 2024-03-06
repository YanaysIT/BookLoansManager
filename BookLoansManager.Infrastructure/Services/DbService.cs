using AutoMapper;
using BookLoansManager.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;

namespace BookLoansManager.Infrastructure.Services;

public class DbService : IDbService
{
    public readonly BookLoansManagerContext _context;
    public readonly IMapper _mapper;

    public DbService(BookLoansManagerContext context, IMapper mapper) 
        => (_context, _mapper) = (context, mapper);

    public void IncludeNavigations<TEntity>() where TEntity : class
    {
        var entityType = _context.Model.FindEntityType(typeof(TEntity));
        if (entityType == null) return;

        var navigations = entityType.GetNavigations().Select(n => n.Name);

        foreach ( var name in navigations) 
            _context.Set<TEntity>().Include(name).Load();
    }

    public async Task<TEntity?> GetSingleAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class =>
        await _context.Set<TEntity>().SingleOrDefaultAsync(expression);

    public async Task<TDto> GetSingleAsync<TEntity,TDto>(Expression<Func<TEntity, bool>> expression, bool include = false)
        where TEntity : class where TDto : class
    {
        if (include) 
            IncludeNavigations<TEntity>();
        var entity = await GetSingleAsync(expression);
        return _mapper.Map<TDto>(entity);
    }

    public async Task<List<TDto>> GetAsync<TEntity,TDto>(bool include = false) where TEntity : class where TDto : class
    {
        if (include)
            IncludeNavigations<TEntity>();
        var entity = await _context.Set<TEntity>().ToListAsync();
        return _mapper.Map<List<TDto>>(entity);
    }

    public async Task<TEntity> AddAsync<TEntity, TDto>(TDto dto) where TEntity : class where TDto : class 
    {
        var entity = _mapper.Map<TEntity>(dto);
        await _context.Set<TEntity>().AddAsync(entity);
        return entity;
    }

    public async Task<bool> UpdateAsync<TEntity>(Expression<Func<TEntity, bool>> expression, Action<TEntity> updateAction) where TEntity : class, IEntity
    {
        try 
        {
            var entity = await GetSingleAsync(expression);
            if (entity == null)
                return false;
            updateAction(entity);
        }
        catch 
        {
            throw;
        }
        return true;
    }

    public async Task<bool> DeleteAsync<TEntity>(int id) where TEntity : class, IEntity
    {
        try 
        {
            var entity = await GetSingleAsync<TEntity>(e => e.Id == id);
            if (entity is null)
                return false;
            _context.Remove(entity);
        }
        catch 
        {
            throw;
        }
        return true;
    }

    public async Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> expression)
        where TEntity : class => await _context.Set<TEntity>().AnyAsync(expression); 

    public async Task<bool> SaveAsync() => await _context.SaveChangesAsync() >= 0;
}