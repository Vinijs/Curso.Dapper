namespace Curso.Dapper.ComEntity.Api.Domain.Interfaces;

public interface IRepository<TEntity> : IDisposable where TEntity : class
{
    Task<IEnumerable<TEntity>> ObterTodos();
    Task<TEntity> ObterPorId(int id);
    Task Inserir(TEntity entity);
    Task Atualizar(TEntity entity);
    Task Excluir(int id);
}
