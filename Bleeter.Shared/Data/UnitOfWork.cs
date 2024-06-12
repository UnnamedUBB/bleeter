using Bleeter.Shared.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Bleeter.Shared.Data;

public class UnitOfWork<T> : IUnitOfWork, IDisposable
    where T : DbContext
{
    private readonly T _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(T context)
    {
        _context = context;
    }

    public async Task BeginTransactionAsync()
    {
        if (_transaction is not null)
            throw new Exception("");

        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction == null)
            return;

        try
        {
            await _context.SaveChangesAsync();
            await _transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await _transaction.RollbackAsync();
            throw;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null) 
            await _transaction.RollbackAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
        _transaction?.Dispose();
    }
}