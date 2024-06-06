using Curso.Dapper.ComEntity.Api.Application.Interfaces;
using Curso.Dapper.ComEntity.Api.Data.Database.Interfaces;
using Curso.Dapper.ComEntity.Api.Domain.Commands.Curso;
using Curso.Dapper.ComEntity.Api.Domain.Services.Interfaces;

namespace Curso.Dapper.ComEntity.Api.Application.Handlers.Commands.Curso;

public class CadastrarCursoCommandHandler : ICommandHandler<CadastrarCursoCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CadastrarCursoCommandHandler> _logger;
    private readonly ICursoDapperDomainService _cursoDapperDomainService;

    public CadastrarCursoCommandHandler(ICursoDapperDomainService cursoDapperDomainService,
                                        IUnitOfWork unitOfWork,
                                        ILogger<CadastrarCursoCommandHandler> logger)
    {
        _cursoDapperDomainService = cursoDapperDomainService;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(CadastrarCursoCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var curso = new Domain.Entities.Curso
            {
                //Validação do command

                //Mapper para converter o comando em entidade
                Nome = command.Nome,
                Descricao = command.Descricao,
                Coordenador = command.Coordenador,
                Professor = command.Professor,
                TipoCurso = command.TipoCurso,
                DataCriacao = DateTime.Now,
            };

            //Validação do curso

            await _cursoDapperDomainService.CadastrarCurso(curso);

            //Alguma outra logica de negócio

            _unitOfWork.Commit();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao cadastrar curso");
            _unitOfWork.RollBack();
        }
    }
}
