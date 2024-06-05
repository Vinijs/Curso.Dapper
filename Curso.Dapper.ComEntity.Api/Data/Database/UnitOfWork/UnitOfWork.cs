using Curso.Dapper.ComEntity.Api.Data.Database.Context;
using Curso.Dapper.ComEntity.Api.Data.Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Curso.Dapper.ComEntity.Api.Data.Database.UnitOfWork;

public sealed class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly CursoDapperContext _context;
    private IDbTransaction? _transaction;

    public UnitOfWork(CursoDapperContext context) 
        => _context = context;

    public IDbTransaction Transaction
    {
        get
        {
                if (_transaction is not null)
                    return _transaction;

                var connection = _context.Database.GetDbConnection();

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                _transaction = connection.BeginTransaction();

                return _transaction;
        }
    }

    public void Commit()
    {
        if (_transaction is null)
            return;

        _transaction.Commit();
        _transaction = null;
    }
    public void RollBack()
    {
        if (_transaction is null)
            return;

        _transaction.Rollback();
        _transaction = null;
    }
    public void Dispose()
    {
        _context?.Dispose();
        _transaction?.Dispose();
    }
}
