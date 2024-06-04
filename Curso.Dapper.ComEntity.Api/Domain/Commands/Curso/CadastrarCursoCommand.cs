using Curso.Dapper.ComEntity.Api.Domain.Common;
using Curso.Dapper.ComEntity.Api.Domain.Enums;

namespace Curso.Dapper.ComEntity.Api.Domain.Commands.Curso;

public class CadastrarCursoCommand : Command, ICommand
{
    public CadastrarCursoCommand(string nome, string descricao, string coordenador, string professor, TipoCurso tipoCurso)
    {
        Nome = nome;
        Descricao = descricao;
        Coordenador = coordenador;
        Professor = professor;
        TipoCurso = tipoCurso;
    }

    public string Nome { get; set; }
    public string Descricao { get; set; }
    public string Coordenador { get; set; }
    public string Professor { get; set; }
    public TipoCurso TipoCurso { get; set; }
}
