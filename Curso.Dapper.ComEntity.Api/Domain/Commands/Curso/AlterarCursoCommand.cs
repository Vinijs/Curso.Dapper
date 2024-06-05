using Curso.Dapper.ComEntity.Api.Domain.Common;
using Curso.Dapper.ComEntity.Api.Domain.Enums;

namespace Curso.Dapper.ComEntity.Api.Domain.Commands.Curso;

public class AlterarCursoCommand : Command, ICommand
{
    public AlterarCursoCommand(int id, string nome, string descricao,
                               string coordenador, string professor,
                               TipoCurso tipoCurso)
    {
        Id = id;
        Nome = nome;
        Descricao = descricao;
        Coordenador = coordenador;
        Professor = professor;
        TipoCurso = tipoCurso;
    }

    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public string Coordenador { get; set; }
    public string Professor { get; set; }
    public TipoCurso TipoCurso { get; set; }
}
