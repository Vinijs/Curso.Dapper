using Curso.Dapper.ComEntity.Api.Domain.Common;
using MediatR;

namespace Curso.Dapper.ComEntity.Api.Domain.Queries.Curso;

public class ObterTodosCursosQuery: Query, IRequest<IEnumerable<Entities.Curso>>
{
}
