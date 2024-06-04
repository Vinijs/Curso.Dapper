using Curso.Dapper.ComEntity.Api.Domain.Interfaces;
using Curso.Dapper.ComEntity.Api.Domain.Services.Interfaces;

namespace Curso.Dapper.ComEntity.Api.Domain.Services;

public sealed class CursoDapperDomainService : ICursoDapperDomainService
{
    private readonly ICursoRepository _cursoRepository;

    public CursoDapperDomainService(ICursoRepository cursoRepository) 
        => _cursoRepository = cursoRepository;

    public async Task CadastrarCurso(Entities.Curso curso)
    {
        //Lógica de negócio
        await _cursoRepository.Inserir(curso);
    }
}
