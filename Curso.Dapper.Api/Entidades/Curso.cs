namespace Curso.Dapper.Api.Entidades;

public class Curso
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public string Coordenador { get; set; }
    public string Professor { get; set; }
    public DateTime Criacao { get; set; }
    public IEnumerable<Turma> Turmas { get; set; }
}
