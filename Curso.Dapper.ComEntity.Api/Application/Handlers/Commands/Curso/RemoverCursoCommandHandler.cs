using Curso.Dapper.ComEntity.Api.Domain.Commands.Curso;
using Curso.Dapper.ComEntity.Api.Domain.Services.Interfaces;
using MediatR;

namespace Curso.Dapper.ComEntity.Api.Application.Handlers.Commands.Curso;

public class RemoverCursoCommandHandler : IRequestHandler<RemoverCursoCommand, Unit>
{
    private readonly ICursoDapperDomainService _cursoDapperDomainService;

    public RemoverCursoCommandHandler(ICursoDapperDomainService cursoDapperDomainService) 
        => _cursoDapperDomainService = cursoDapperDomainService;

    public async Task<Unit> Handle(RemoverCursoCommand request, CancellationToken cancellationToken)
    {

    // Validação do command

    await _cursoDapperDomainService.RemoverCurso(request.Id);

        return Unit.Value;
    }
}
