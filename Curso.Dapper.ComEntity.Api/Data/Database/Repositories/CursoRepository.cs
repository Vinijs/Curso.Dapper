using Curso.Dapper.ComEntity.Api.Data.Database.Context;
using Curso.Dapper.ComEntity.Api.Data.Database.Extensions;
using Curso.Dapper.ComEntity.Api.Data.Database.Interfaces;
using Curso.Dapper.ComEntity.Api.Data.Database.Repositories.Base;
using Curso.Dapper.ComEntity.Api.Domain.Interfaces;

namespace Curso.Dapper.ComEntity.Api.Data.Database.Repositories;

public partial class CursoRepository : BaseRepository ,ICursoRepository
{
    public CursoRepository(CursoDapperContext context, 
                           IUnitOfWork unitOfWork,
                           ILogger<CursoRepository> logger)
        : base(unitOfWork, context, logger) {}

    public async Task Atualizar(Domain.Entities.Curso entity)
        => await ExecuteWithRetry(_sqlAtualizar, new
        {
            entity.Id,
            Nome = entity.Nome.ToDbStringParam(),
            Descricao = entity.Descricao.ToDbStringParam(),
            Coordenador = entity.Coordenador.ToDbStringParam(),
            Professor = entity.Professor.ToDbStringParam(),
            TipoCurso = entity.TipoCurso.ToDbStringParam(),
        });
    public async Task Excluir(int id) 
        => await ExecuteWithRetry(_sqlExcluir, new { id });

    public async Task Inserir(Domain.Entities.Curso entity) 
        => await ExecuteWithRetry(_sqlInserir, entity);

    public async Task<Domain.Entities.Curso> ObterPorId(int id) 
        => await  QueryFirstOrDefaultWithRetry<Domain.Entities.Curso>(_sqlObterPorId, new { id });

    public async Task<IEnumerable<Domain.Entities.Curso>> ObterTodos() 
        => await QueryWithRetry<Domain.Entities.Curso>(_sqlObterTodos);
    public void Dispose() 
        => Context?.Dispose();
}
