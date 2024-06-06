using Curso.Dapper.ComEntity.Api.Data.Database.Interfaces;
using Curso.Dapper.ComEntity.Api.Domain.Commands.Curso;
using Curso.Dapper.ComEntity.Api.Domain.Services.Interfaces;
using MediatR;

namespace Curso.Dapper.ComEntity.Api.Application.Handlers.Commands.Curso;

public class RemoverCursoCommandHandler : IRequestHandler<RemoverCursoCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RemoverCursoCommandHandler> _logger;
    private readonly ICursoDapperDomainService _cursoDapperDomainService;

    public RemoverCursoCommandHandler(ICursoDapperDomainService cursoDapperDomainService,
                                      IUnitOfWork unitOfWork,
                                      ILogger<RemoverCursoCommandHandler> logger)
    {
        _cursoDapperDomainService = cursoDapperDomainService;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Unit> Handle(RemoverCursoCommand request, CancellationToken cancellationToken)
    {

        try
        {
            // Validação do command

            await _cursoDapperDomainService.RemoverCurso(request.Id);

            _unitOfWork.Commit();

            return Unit.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao remover curso");
            _unitOfWork.RollBack();
            return Unit.Value;
        }
    }
}
