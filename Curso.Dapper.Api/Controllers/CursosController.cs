using Curso.Dapper.Api.Entidades;
using Curso.Dapper.Api.Enums;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Curso.Dapper.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CursosController : ControllerBase
{
    private readonly string _connectionString;

    public CursosController(IConfiguration configuration) 
        => _connectionString = configuration.GetConnectionString("DefaultConnection")!;

    [HttpGet(Name = "ObterCursos")]
    public async Task<IActionResult> Obter()
    {
        var sql = @"SELECT Id, Nome, Descricao, TipoCurso, Coordenador, Professor, DataCriacao 
                    FROM dbo.Cursos (NOLOCK)";

        using var connection = new SqlConnection(_connectionString);

        var cursos = await connection.QueryAsync<Entidades.Curso>(sql);
        return Ok(cursos);
    }

    [HttpGet("obter-todos-por-tipo",Name = "ObterTodosPorTipo")]
    public async Task<IActionResult> ObterTodosPorTipo([FromQuery] Enums.TipoCurso tipoCurso)
    {
        var sql = @"SELECT Id, Nome, Descricao, TipoCurso, Coordenador, Professor, DataCriacao 
                    FROM dbo.Cursos (NOLOCK)";

        var cursosGraduacao = new List<CursoGraduacao>();
        var cursosPosGraduacao = new List<CursoPosGraduacao>();
        var cursosTecnico = new List<CursoTecnico>();

        using var connection = new SqlConnection(_connectionString);

        await connection.OpenAsync();

        using (var reader = await connection.ExecuteReaderAsync(sql))
        {
            var graduacaoParser = reader.GetRowParser<CursoGraduacao>();
            var tecnicoParser = reader.GetRowParser<CursoTecnico>();
            var posGraduacaoParser = reader.GetRowParser<CursoPosGraduacao>();

            while (await reader.ReadAsync())
            {
                var discriminator = (TipoCurso)Enum
                    .Parse(typeof(TipoCurso),reader.GetString(reader.GetOrdinal(nameof(TipoCurso))));
                switch (discriminator)
                {
                    case TipoCurso.Graduacao:
                        cursosGraduacao.Add(graduacaoParser(reader));
                        break;
                    case TipoCurso.PosGraduacao:
                        cursosPosGraduacao.Add(posGraduacaoParser(reader));
                        break;
                    case TipoCurso.Tecnico:
                        cursosTecnico.Add(tecnicoParser(reader));
                        break;
                    default:
                        break;
                }
            }
        }

        await connection.CloseAsync();

        return Ok(new
        {
            Graduacao = cursosGraduacao,
            PosGraduacao = cursosPosGraduacao,
            Tecnico = cursosTecnico
        });
    }

    [HttpGet("{id}", Name = "ObterPorId")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var sql = @"SELECT Id, Nome, Descricao, TipoCurso, Coordenador, Professor, DataCriacao
                    FROM dbo.Cursos (NOLOCK)
                    WHERE Id = @id";

        using var connection = new SqlConnection(_connectionString);

        var curso = await connection.QueryFirstOrDefaultAsync<Entidades.Curso>(sql, new { id });

        if (curso is null)
            return NotFound();

        return Ok(curso);
    }

    [HttpPost(Name = "InserirCurso")]
    public async Task<IActionResult> Inserir([FromBody] Entidades.Curso curso)
    {
        var sql = @"INSERT INTO dbo.Cursos (Nome, Descricao, TipoCurso, Coordenador, Professor, DataCriacao)
                    VALUES (@Nome, @Descricao, @TipoCurso, @Coordenador, @Professor, @DataCriacao)";

        using var connection = new SqlConnection(_connectionString);

        await connection.ExecuteAsync(sql, new { curso.Nome,
                curso.Descricao,TipoCurso = curso.TipoCurso.ToString(),
                curso.Coordenador, curso.Professor, curso.DataCriacao });

        return CreatedAtRoute("ObterPorId", new { id = curso.Id }, curso);
    }

}
