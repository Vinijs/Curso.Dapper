using Curso.Dapper.ComEntity.Api.Domain.Interfaces;
using Curso.Dapper.ComEntity.Api.Domain.Queries.Curso;
using MediatR;

namespace Curso.Dapper.ComEntity.Api.Application.Handlers.Queries.Curso;

public class ObterCursoPorIdQueryHandler : IRequestHandler<ObterPorIdCursoQuery, Domain.Entities.Curso>
{
    private readonly ICursoRepository _cursoRepository;
    public ObterCursoPorIdQueryHandler(ICursoRepository cursoRepository) 
        => _cursoRepository = cursoRepository;

    public async Task<Domain.Entities.Curso> Handle(ObterPorIdCursoQuery request, CancellationToken cancellationToken) 
        => await _cursoRepository.ObterPorId(request.Id);

}
