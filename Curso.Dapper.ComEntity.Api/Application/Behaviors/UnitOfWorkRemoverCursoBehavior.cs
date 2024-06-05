using Curso.Dapper.ComEntity.Api.Data.Database.Interfaces;
using Curso.Dapper.ComEntity.Api.Domain.Commands.Curso;
using MediatR;

namespace Curso.Dapper.ComEntity.Api.Application.Behaviors;

public class UnitOfWorkRemoverCursoBehavior : IPipelineBehavior<RemoverCursoCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UnitOfWorkRemoverCursoBehavior> _logger;

    public UnitOfWorkRemoverCursoBehavior(IUnitOfWork unitOfWork,
        ILogger<UnitOfWorkRemoverCursoBehavior> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Unit> Handle(RemoverCursoCommand request,
                             RequestHandlerDelegate<Unit> next,
                             CancellationToken cancellationToken)
    {
        Unit response;

        try
        {
            response = await next();
            _unitOfWork.Commit();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao remover curso");
            _unitOfWork.RollBack();
        }

        return response;
    }
}
