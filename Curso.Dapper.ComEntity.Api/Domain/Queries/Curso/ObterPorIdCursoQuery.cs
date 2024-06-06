using Curso.Dapper.ComEntity.Api.Domain.Common;
using MediatR;

namespace Curso.Dapper.ComEntity.Api.Domain.Queries.Curso;

public class ObterPorIdCursoQuery : Query, IRequest<Entities.Curso>
{
    public ObterPorIdCursoQuery(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}
