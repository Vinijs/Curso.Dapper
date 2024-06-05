using Curso.Dapper.ComEntity.Api.Data.Database.Context;
using Curso.Dapper.ComEntity.Api.Data.Database.Extensions;
using Curso.Dapper.ComEntity.Api.Data.Database.Interfaces;
using Curso.Dapper.ComEntity.Api.Domain.Interfaces;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace Curso.Dapper.ComEntity.Api.Data.Database.Repositories;

public partial class CursoRepository : ICursoRepository
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly CursoDapperContext _context;

    public IUnitOfWork UnitOfWork => _context;

    public CursoRepository(CursoDapperContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public IUnitOfWork UnitofWork => _context;

    public CursoRepository(CursoDapperContext context) 
        => _context = context;

    public async Task Atualizar(Domain.Entities.Curso entity) 
        => await _context.Database.GetDbConnection()
            .ExecuteAsync(_sqlAtualizar, new
            {
                entity.Id,
                Nome = entity.Nome.ToDbStringParam(),
                Descricao = entity.Descricao.ToDbStringParam(),
                Coordenador = entity.Coordenador.ToDbStringParam(),
                Professor = entity.Professor.ToDbStringParam(),
                TipoCurso = entity.TipoCurso.ToDbStringParam(),
            }, _unitOfWork.Transaction);
    public async Task Excluir(int id) 
        => await _context.Database.GetDbConnection()
            .ExecuteAsync(_sqlExcluir, new { id }, _unitOfWork.Transaction);

    public async Task Inserir(Domain.Entities.Curso entity) 
        => await _context.Database.GetDbConnection()
            .ExecuteAsync(_sqlInserir, entity, _context.Transaction);

    public Task<Domain.Entities.Curso> ObterPorId(int id) 
        => _context.Database.GetDbConnection()
            .QueryFirstOrDefaultAsync<Domain.Entities.Curso>(_sqlObterPorId, new { id });

    public Task<IEnumerable<Domain.Entities.Curso>> ObterTodos() 
        => _context.Database.GetDbConnection()
            .QueryAsync<Domain.Entities.Curso>(_sqlObterTodos);
    public void Dispose() 
        => _context?.Dispose();
}
