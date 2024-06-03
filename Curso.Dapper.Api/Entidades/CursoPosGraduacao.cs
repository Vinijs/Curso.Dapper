namespace Curso.Dapper.Api.Entidades;

public class CursoPosGraduacao : Curso
{
    public string TemaTcc { get; set; }
    public CursoPosGraduacao() 
        => TipoCurso = Enums.TipoCurso.PosGraduacao;
}
