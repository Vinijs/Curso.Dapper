using Curso.Dapper.Api.Entidades;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Curso.Dapper.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AlunosController : ControllerBase
{
    private readonly string _connectionString;
    public AlunosController(IConfiguration configuration)
        => _connectionString = configuration.GetConnectionString("DefaultConnection")!;

    [HttpGet(Name = "BuscarAlunos")]
    public async Task<IActionResult> Get()
    {
        using var connection = new SqlConnection(_connectionString);
        var alunos = await connection.QueryAsync<Aluno>("SELECT * FROM Alunos");
        return Ok(alunos);
    }

    [HttpGet("obter-por-ids",Name = "BuscarAlunosPorIds")]
    public async Task<IActionResult> ObterAlunosPorIds(string ids)
    {
        using var connection = new SqlConnection(_connectionString);

        var idsSeparados = ids.Split(",", StringSplitOptions.RemoveEmptyEntries);


        var alunos = await connection
            .QueryAsync<Aluno>("SELECT * FROM Alunos WHERE Id IN @idsSeparados", new { idsSeparados });
        return Ok(alunos);
    }

    [HttpGet("com-cursos", Name = "BuscarAlunosComCursos")]
    public async Task<IActionResult> GetAlunosComCursos()
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = @"SELECT *
                    FROM Alunos a
                    INNER JOIN AlunosCursos ac ON ac.IdAluno = a.Id
                    INNER JOIN Cursos c ON c.Id = ac.IdCurso";

        var alunosDicionario = new Dictionary<int, Aluno>();

        _ = await connection.QueryAsync<Aluno, AlunosCursos,Entidades.Curso, Aluno>(sql,
            (aluno, alunoCurso, curso ) =>
            {
                if (!alunosDicionario.TryGetValue(aluno.Id, out var alunoEntrada))
                {
                    alunoEntrada = aluno;
                    alunosDicionario.Add(alunoEntrada.Id, alunoEntrada);
                }

                if(alunoEntrada.Id == alunoCurso.IdAluno && alunoCurso.IdCurso == curso.Id)
                    alunoEntrada.Cursos.Add(curso);


                return aluno;
            });
        return Ok(alunosDicionario.Values);
    }

    [HttpGet("{id}", Name = "BuscarAlunoPorId")]
    public async Task<IActionResult> Get(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        var aluno = await connection.
            QueryFirstOrDefaultAsync<Aluno>("SELECT * FROM Alunos WHERE Id = @id", new { id });
        if (aluno is null)
            return NotFound();

        return Ok(aluno);
    }

    [HttpGet("com-cursos/{id}", Name = "BuscarAlunoComCursosPorId")]
    public async Task<IActionResult> GetAlunoComCursos(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = @"SELECT *
                    FROM Alunos a
                    INNER JOIN AlunosCursos ac ON ac.IdAluno = a.Id
                    INNER JOIN Cursos c ON c.Id = ac.IdCurso
                    WHERE a.Id = @id";

        Aluno alunoRetorno = null!; 

        _ = await connection.
            QueryAsync<Aluno, Entidades.Curso, Aluno>(sql,
            (aluno, curso) => 
            {
                alunoRetorno ??= new Aluno();

                aluno.Cursos.Add(curso);
                return aluno;
            }, new { id });
        if (alunoRetorno is null)
            return NotFound();

        return Ok(alunoRetorno);
    }

    [HttpGet("dynanic/{id}", Name = "BuscarAlunoPorIdUsandoDynamic")]
    public async Task<IActionResult> GetWithDynamic(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        var dadoDinamico = await connection.
            QueryFirstOrDefaultAsync("SELECT * FROM Alunos WHERE Id = @id", new { id });

        var dataCadastro = dadoDinamico.DataCadastro;

        var aluno = new Aluno
        {
            Id = dadoDinamico.Id,
            Nome = dadoDinamico.Nome,
            Email = dadoDinamico.Email,
            DataNascimento = dadoDinamico.DataNascimento,
            Ativo = dadoDinamico.Ativo,
            DataCriacao = dadoDinamico.DataCriacao
        };
        
        if (aluno is null)
            return NotFound();

        return Ok(aluno);
    }

    [HttpPost("cadastrar-com-email-proprio",Name = "CadastrarAlunoComEmailProprio")]
    public async Task<IActionResult> PostComEmail([FromBody] Aluno aluno)
    {
        using var connection = new SqlConnection(_connectionString);
        int id = 0;

        connection.Open();

        using var transaction = connection.BeginTransaction();

        try
        {
            if (await AlunoExiste(aluno.Email, connection, transaction))
            {
                return BadRequest(new { mensagem = "Aluno já cadastrado" });
            }

            var sql = @"INSERT INTO Alunos (Nome, Email, DataNascimento,Ativo, DataCriacao)
                       VALUES (@Nome, @Email, @DataNascimento,1, GETDATE());
                       SELECT CAST(SCOPE_IDENTITY() as int)";

            id = await connection.ExecuteScalarAsync<int>(sql, aluno, transaction: transaction);
            aluno.Id = id;

            throw new Exception("Erro ao cadastrar aluno");

            var filaEmail = new FilaEnvioEmail
            {
                CorpoEmail = $"<h1>Seja bem-vindo {aluno.Nome}</h1>",
                De = "direcao@email.com",
                Para = aluno.Email,
                Enviado = false,
            };

            sql = @"INSERT INTO FilaEnvioEmail (De, Para, CorpoEmail, Enviado)
                VALUES (@De, @Para, @CorpoEmail, @Enviado);
                SELECT CAST(SCOPE_IDENTITY() as int)";

            var idFila = await connection.ExecuteScalarAsync<int>(sql, filaEmail, transaction: transaction);
            filaEmail.Id = idFila;

            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            return BadRequest(new { mensagem = "Erro ao cadastrar aluno" });
        }

        return CreatedAtRoute("BuscarAlunoPorId", new { id }, aluno);
    }



    [HttpPut("{id}", Name = "AtualizarAluno")]
    public async Task<IActionResult> Put(int id,[FromBody] Aluno aluno)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = @"UPDATE Alunos SET Nome = @Nome, Email = @Email, DataNascimento = @DataNascimento
         WHERE Id = @id";
        var linhasAfetadas = await connection
            .ExecuteAsync(sql, new { id, aluno.Nome, aluno.Email, aluno.DataNascimento
                                     });
        if (linhasAfetadas == 0)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}", Name = "ExcluirAluno")]
    public async Task<IActionResult> Delete(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = @"DELETE FROM Alunos WHERE Id = @id";
        var linhasAfetadas = await connection.ExecuteAsync(sql, new { id });
        if (linhasAfetadas == 0)
        {
            return NotFound();
        }

        return NoContent();
    }

    private static async Task<bool> AlunoExiste(string email, IDbConnection connection, IDbTransaction transaction = null!)
    {
        return await connection
            .QueryFirstOrDefaultAsync<bool>("SELECT 1 FROM Alunos WHERE Email = @email",
                                          new { email }, transaction: transaction);
    }
}
