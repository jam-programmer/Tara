using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Contracts;

public interface IContext
{
    Task<IDbContextTransaction> BeginTransactionAsync();
    void ClearTracker();
}
