namespace Curso.Dapper.Api.Entidades;

public class AlunosCursos
{
    public int Id { get; set; }
    public int IdAluno { get; set; }
    public int IdCurso { get; set; }
    public DateTime DataCriacao { get; set; }
}
