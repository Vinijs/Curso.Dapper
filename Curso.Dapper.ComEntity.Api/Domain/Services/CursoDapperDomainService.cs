using Curso.Dapper.ComEntity.Api.Domain.Interfaces;
using Curso.Dapper.ComEntity.Api.Domain.Services.Interfaces;

namespace Curso.Dapper.ComEntity.Api.Domain.Services;

public sealed class CursoDapperDomainService : ICursoDapperDomainService
{
    private readonly ICursoRepository _cursoRepository;

    public CursoDapperDomainService(ICursoRepository cursoRepository) 
        => _cursoRepository = cursoRepository;

    public async Task AlterarCurso(Entities.Curso curso)
    {
        //Logica de negócio

        var cursoExistente = await _cursoRepository.ObterPorId(curso.Id)
            ?? throw new Exception("Curso não encontrado");

        cursoExistente.Nome = curso.Nome;
        cursoExistente.Descricao = curso.Descricao;
        cursoExistente.Coordenador = curso.Coordenador;
        cursoExistente.Professor = curso.Professor;
        cursoExistente.TipoCurso = curso.TipoCurso;

        await _cursoRepository.Atualizar(curso);

        //Alguma outra logica de negócio
    }

    public async Task CadastrarCurso(Entities.Curso curso)
    {
        //Lógica de negócio
        await _cursoRepository.Inserir(curso);

        //Alguma outra lógica de negócio
    }

    public async Task RemoverCurso(int id)
    {
        //Logica de negócio
        await _cursoRepository.Excluir(id);
    }
}
