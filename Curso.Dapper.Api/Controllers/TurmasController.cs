using Curso.Dapper.Api.Data.Database.Dtos;
using Curso.Dapper.Api.Entidades;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Curso.Dapper.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TurmasController: ControllerBase
{
    public readonly string _connectionString;

    public TurmasController(IConfiguration configuration) 
        => _connectionString = configuration.GetConnectionString("DefaultConnection");

    [HttpGet(Name = "BuscarTurmas")]
    public async Task<IActionResult> Get()
    {
        using var connection = new SqlConnection(_connectionString);
        var turmas = await connection.QueryAsync<Turma>("SELECT * FROM Turmas");
        return Ok(turmas);
    }

    [HttpGet("turmas-com-turnos", Name = "BuscarTurmasComTurnos")]
    public async Task<IActionResult> GetTurmasComTurnos()
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = @"SELECT t.Id, t.Nome, t.NomeCurso, t.DataCriacao,
                    t.IdCurso, t.IdTurno, tu.Id, tu.Nome, tu.DataCriacao
                    FROM Turmas t 
                    INNER JOIN Turnos tu ON tu.Id = t.IdTurno";

        var turmas = await connection.QueryAsync<Turma, Turno, Turma>(sql, (turma, turno) => 
        {
            turma.Turno = turno;
            return turma;
        });
        return Ok(turmas);
    }

    [HttpGet("com-turno-dynamic/{id}", Name = "BuscarTurmaPorIdUsandoDynamic")]
    public async Task<IActionResult> GetTurmaWithDynamic(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = @"SELECT t.Id AS IdTurma, t.Nome, t.NomeCurso, t.DataCriacao AS DataCriacaoTurma,
                    t.IdCurso, t.IdTurno, tu.Id AS IdTurno, tu.Nome AS NomeTurno, tu.DataCriacao AS DataCriacaoTurno
                    FROM Turmas t 
                    INNER JOIN Turnos tu ON tu.Id = t.IdTurno
                    WHERE t.Id = @id";

        var dadoDinamico = await connection.
            QueryFirstOrDefaultAsync(sql, new { id });

        var turma = new Turma
        {
            Id = dadoDinamico.IdTurma,
            Nome = dadoDinamico.Nome,
            NomeCurso = dadoDinamico.NomeCurso,
            DataCriacao = dadoDinamico.DataCriacaoTurma,
            IdCurso = dadoDinamico.IdCurso,
            IdTurno = dadoDinamico.IdTurno,
            Turno = new Turno
            {
                Id = dadoDinamico.IdTurno,
                Nome = dadoDinamico.NomeTurno,
                DataCriacao = dadoDinamico.DataCriacaoTurno,
            }
        };

        if (turma is null)
            return NotFound();

        return Ok(turma);
    }

    [HttpGet("turmas-turnos-separados-multiple-select", Name = "BuscarTodasTurmasETurnos")]
    public async Task<IActionResult> GetTodasTurmasETurnos()
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = @"SELECT * FROM Turmas;
                    SELECT * FROM Turnos";

        using var multi = await connection.QueryMultipleAsync(sql);

        var turmas = await multi.ReadAsync<Turma>();
        var turnos = await multi.ReadAsync<Turno>();

        foreach (var turma in turmas)
        {
            turma.Turno = turnos.FirstOrDefault(x => x.Id == turma.IdTurno)!;
        }

        return Ok(turmas);
    }

    [HttpGet("informacoes-turma", Name = "InformacoesTurma")]
    public async Task<IActionResult> GetInformacoesTurma()
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = @"SELECT
                    t.Nome,
                    t.NomeCurso,
                    tu.Nome AS NomeTurno
                    FROM Turmas (NOLOCK) t
                    INNER JOIN Turnos (NOLOCK) tu ON tu.Id = t.IdTurno";

        var informacoesTurma = await connection.QueryAsync<InformacoesTurmaDto>(sql);

        return Ok(informacoesTurma);
    }
}
