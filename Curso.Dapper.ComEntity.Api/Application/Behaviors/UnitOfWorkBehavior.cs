using Curso.Dapper.ComEntity.Api.Data.Database.Interfaces;
using MediatR;

namespace Curso.Dapper.ComEntity.Api.Application.Behaviors;

public sealed class UnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : notnull
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UnitOfWorkBehavior<TRequest, TResponse>> _logger;

    public UnitOfWorkBehavior(IUnitOfWork unitOfWork,
                              ILogger<UnitOfWorkBehavior<TRequest, TResponse>> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request,
                                  RequestHandlerDelegate<TResponse> next, 
                                  CancellationToken cancellationToken)
    {
        TResponse response;
        try
        {
            if (!typeof(TRequest).Name.EndsWith("Command"))
                return await next();

            response = await next();

            _unitOfWork.Commit();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao executar comando");
            _unitOfWork.RollBack();
            throw;
        }

        return response;
    }
}
