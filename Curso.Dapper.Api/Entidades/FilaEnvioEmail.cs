namespace Curso.Dapper.Api.Entidades;

public class FilaEnvioEmail
{
    public int Id { get; set; }
    public string CorpoEmail { get; set; } = null!;
    public bool Enviado { get; set; }
    public string De { get; set; } = null!;
    public string Para { get; set; } = null!;
}
