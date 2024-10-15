using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Repository;

public class ContextRepository(SqlServerContext context) : IContext
{
    private SqlServerContext _context = context;
    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _context.Database.BeginTransactionAsync();
    }

    public void ClearTracker()
    {
        _context.ChangeTracker.Clear();
    }
}
