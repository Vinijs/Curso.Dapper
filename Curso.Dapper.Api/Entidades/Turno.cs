namespace Curso.Dapper.Api.Entidades;

public class Turno
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; }
}
