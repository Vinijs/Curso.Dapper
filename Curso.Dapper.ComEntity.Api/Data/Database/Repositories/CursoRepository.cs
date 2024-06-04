using Curso.Dapper.ComEntity.Api.Data.Database.Context;
using Curso.Dapper.ComEntity.Api.Domain.Interfaces;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace Curso.Dapper.ComEntity.Api.Data.Database.Repositories;

public partial class CursoRepository : ICursoRepository
{
    private readonly CursoDapperContext _context;

    public CursoRepository(CursoDapperContext context) 
        => _context = context;

    public async Task Atualizar(Domain.Entities.Curso entity) 
        => await _context.Database.GetDbConnection()
            .ExecuteAsync(_sqlAtualizar, entity);
    public async Task Excluir(int id) 
        => await _context.Database.GetDbConnection()
            .ExecuteAsync(_sqlExcluir, new { id });

    public async Task Inserir(Domain.Entities.Curso entity) 
        => await _context.Database.GetDbConnection()
            .ExecuteAsync(_sqlInserir, entity);

    public Task<Domain.Entities.Curso> ObterPorId(int id) 
        => _context.Database.GetDbConnection()
            .QueryFirstOrDefaultAsync<Domain.Entities.Curso>(_sqlObterPorId, new { id });

    public Task<IEnumerable<Domain.Entities.Curso>> ObterTodos() 
        => _context.Database.GetDbConnection()
            .QueryAsync<Domain.Entities.Curso>(_sqlObterTodos);
    public void Dispose() 
        => _context?.Dispose();
}
