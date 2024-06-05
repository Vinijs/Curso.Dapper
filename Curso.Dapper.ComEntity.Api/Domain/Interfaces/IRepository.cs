using Curso.Dapper.ComEntity.Api.Data.Database.Interfaces;

namespace Curso.Dapper.ComEntity.Api.Domain.Interfaces;

public interface IRepository<TEntity> : IDisposable where TEntity : class
{
    IUnitOfWork UnitofWork { get; } 
    Task<IEnumerable<TEntity>> ObterTodos();
    Task<TEntity> ObterPorId(int id);
    Task Inserir(TEntity entity);
    Task Atualizar(TEntity entity);
    Task Excluir(int id);
}
