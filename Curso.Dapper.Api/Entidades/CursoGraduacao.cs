namespace Curso.Dapper.Api.Entidades;

public class CursoGraduacao : Curso
{
    public string TemaTcc { get; set; }

    public CursoGraduacao() 
        => TipoCurso = Enums.TipoCurso.Graduacao;
}
