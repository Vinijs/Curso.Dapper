using MediatR;

namespace Curso.Dapper.ComEntity.Api.Domain.Commands.Curso;

public class RemoverCursoCommand : IRequest<Unit>
{
    public RemoverCursoCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}
