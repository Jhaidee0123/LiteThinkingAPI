using System.Linq.Expressions;

namespace LiteThinking.Domain.Ports.Repositories;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();

    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter);

    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, int take, int skip);

    Task<T> GetByIdAsync(Expression<Func<T, bool>> filter);

    Task CreateAsync(T entity);

    void Remove(T entity);

    IQueryable<T> Query();

    IUnitOfWork UnitOfWork { get; }
}
