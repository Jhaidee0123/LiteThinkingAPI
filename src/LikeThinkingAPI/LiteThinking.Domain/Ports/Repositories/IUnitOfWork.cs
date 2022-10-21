namespace LiteThinking.Domain.Ports.Repositories;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken token = default);
}

