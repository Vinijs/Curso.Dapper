namespace Curso.Dapper.ComEntity.Api.Domain.Services.Interfaces;

public interface ICursoDapperDomainService
{
    Task CadastrarCurso(Entities.Curso curso);
}
