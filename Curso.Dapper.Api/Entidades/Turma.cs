namespace Curso.Dapper.Api.Entidades;

public class Turma
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string NomeCurso { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; }
    public int IdCurso { get; set; }
    public int IdTurno { get; set; }
    public Turno Turno { get; set; }
}
