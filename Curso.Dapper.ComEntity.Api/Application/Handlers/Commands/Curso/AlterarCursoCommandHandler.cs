using Curso.Dapper.ComEntity.Api.Application.Interfaces;
using Curso.Dapper.ComEntity.Api.Data.Database.Interfaces;
using Curso.Dapper.ComEntity.Api.Domain.Commands.Curso;
using Curso.Dapper.ComEntity.Api.Domain.Services.Interfaces;

namespace Curso.Dapper.ComEntity.Api.Application.Handlers.Commands.Curso;

public class AlterarCursoCommandHandler : ICommandHandler<AlterarCursoCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AlterarCursoCommandHandler> _logger;
    private readonly ICursoDapperDomainService _cursoDapperDomainService;

    public AlterarCursoCommandHandler(IUnitOfWork unitOfWork,
                                      ILogger<AlterarCursoCommandHandler> logger,
                                      ICursoDapperDomainService cursoDapperDomainService)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _cursoDapperDomainService = cursoDapperDomainService;
    }

    public async Task Handle(AlterarCursoCommand command, CancellationToken cancellationToken)
    {
        try
        {
            //Validacao do command

            //Mapper para converter o command em entidade
            var curso = new Domain.Entities.Curso
            {
                Id = command.Id,
                Nome = command.Nome,
                Descricao = command.Descricao,
                Coordenador = command.Coordenador,
                Professor = command.Professor,
                TipoCurso = command.TipoCurso,
                DataCriacao = DateTime.Now
            };

            //Validacao do curso

            await _cursoDapperDomainService.AlterarCurso(curso);

            //Alguma outra logica de negócio

            _unitOfWork.Commit();

            //Envio de email

            //Envio de notificação

            //_unitOfWork.Commit();
        }
        catch (Exception ex)
        {
            _unitOfWork.RollBack();
            _logger.LogError(ex, "Erro ao cadastrar curso");
        }
    }
}
