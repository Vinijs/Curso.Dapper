using Curso.Dapper.ComEntity.Api.Domain.Interfaces;
using Curso.Dapper.ComEntity.Api.Domain.Queries.Curso;
using MediatR;

namespace Curso.Dapper.ComEntity.Api.Application.Handlers.Queries.Curso;

public class ObterTodosCursosQueryHandler : IRequestHandler<ObterTodosCursosQuery, IEnumerable<Domain.Entities.Curso>>
{
    private readonly ICursoRepository _cursoRepository;

    public ObterTodosCursosQueryHandler(ICursoRepository cursoRepository) 
        => _cursoRepository = cursoRepository;

    public async Task<IEnumerable<Domain.Entities.Curso>> Handle(ObterTodosCursosQuery request, CancellationToken cancellationToken) 
        => await _cursoRepository.ObterTodos();
}
