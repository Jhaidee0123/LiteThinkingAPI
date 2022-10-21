using LiteThinking.Domain.Ports.Repositories;
using LiteThinking.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LiteThinking.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public IQueryable<T> Query() => _context.Set<T>().AsQueryable();

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter)
    {
        return await _context.Set<T>().Where(filter).ToListAsync();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, int take, int skip)
    {
        return await _context.Set<T>().Where(filter).Take(take).Skip(skip).ToListAsync();
    }

    public virtual async Task<T> GetByIdAsync(Expression<Func<T, bool>> filter)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(filter);
    }

    public virtual async Task CreateAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public virtual void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
    }
}
