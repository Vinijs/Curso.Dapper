using System.Data;

namespace Curso.Dapper.ComEntity.Api.Data.Database.Interfaces;

public interface IUnitOfWork
{
    IDbTransaction Transaction { get; }
    void Commit();
    void RollBack();
}
