﻿using Curso.Dapper.Api.Entidades;
using Curso.Dapper.Api.Extensions;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Curso.Dapper.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TurnosController : ControllerBase
{
    

    public readonly string _connectionString;
    public TurnosController(IConfiguration configuration) 
        => _connectionString = configuration.GetConnectionString("DefaultConnection");

    [HttpGet(Name = "ObterTurnosComProcedure")]
    public async Task<IActionResult> Obter()
    {
        using var connection = new SqlConnection(_connectionString);
        var nomeProc = "dbo.ObterTurnos";

        var turnos = await connection.QueryAsync<Turno>(nomeProc, commandType: CommandType.StoredProcedure);
        return Ok(turnos);
    }

    [HttpGet("obter-todos-reader", Name = "ObterTurnosComProcedureUsandoReader")]
    public async Task<IActionResult> ObterComReader()
    {
        var connection = new SqlConnection(_connectionString);

        await connection.OpenAsync();
        
        var nomeProc = "dbo.ObterTurnos";

        var reader = await connection.ExecuteReaderAsync(nomeProc, commandType: CommandType.StoredProcedure);

        var turnos = new List<Turno>();

        while (reader.Read())
        {
            var turno = new Turno
            {
                Id = reader.GetInt32(0),
                Nome = reader.GetString(1),
                DataCriacao = reader.GetDateTime(2)
            };

            turnos.Add(turno);
        }

        await connection.CloseAsync();

        return Ok(turnos);
    }

    [HttpGet("obter-todos-data-table", Name = "ObterTurnosComProcedureUsandoDataTable")]
    public async Task<IActionResult> ObterComReaderEDataTable()
    {
        using var connection = new SqlConnection(_connectionString);



        var nomeProc = "dbo.ObterTurnos";
        var reader = await connection.ExecuteReaderAsync(nomeProc, commandType: CommandType.StoredProcedure);

        var datatable = new DataTable();
        datatable.Load(reader);

        var turnos = new List<Turno>();

        foreach (DataRow row in datatable.Rows)
        {
            var turno = new Turno
            {
                Id = (int)row["Id"],
                Nome = (string)row["Nome"],
                DataCriacao = (DateTime)row["DataCriacao"]
            };
            turnos.Add(turno);
        }

        return Ok(turnos);
    }

    [HttpGet("{id}", Name = "ObterTurnoPorIdProcedure")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        var nomeProc = "dbo.ObterTurnoPorId";

        var turno = await connection.QueryFirstOrDefaultAsync<Turno>(nomeProc,
            new { id }, commandType: CommandType.StoredProcedure);
        return Ok(turno);
    }

    [HttpGet("obter-por-ids-e-nomes", Name = "ObterPorIdsENomes")]
    public async Task<IActionResult> ObterPorIdsENomes(string ids, string nomes)
    {
        using var connection = new SqlConnection(_connectionString);
        var nomeProc = "[dbo].[BuscarTurnosPorNomeEIds]";
        var parametroNomes = nomes.Split(',');
        var parametroIds = ids.Split(',');
        var dicionario = new Dictionary<int, string>();

        for (int i = 0; i <parametroIds.Length; i++)
        {
            dicionario.Add(int.Parse(parametroIds[i]), parametroNomes[i]);
        }

        var turnos = await connection.QueryAsync<Turno>(nomeProc, new { TabelaTurnos = dicionario.GetTableValuedParameter()}
        , commandType: CommandType.StoredProcedure);

        return Ok(turnos);
    }

    [HttpPost(Name = "InserirTurnoProcedure")]
    public async Task<IActionResult> Inserir(Turno turno)
    {
        using var connection = new SqlConnection(_connectionString);
        var nomeProc = "dbo.InserirTurno";

        var id = await connection.ExecuteScalarAsync<int>(nomeProc,
            new { turno.Nome }, commandType: CommandType.StoredProcedure);
        return CreatedAtRoute("ObterPorId", new { id }, turno);
    }

    [HttpPut("{id}", Name = "AtualizarTurnoProcedure")]
    public async Task<IActionResult> Atualizar(int id, Turno turno)
    {
        using var connection = new SqlConnection(_connectionString);
        var nomeProc = "dbo.AtualizarTurno";

        var linhasAfetadas = await connection.ExecuteScalarAsync<int>(nomeProc, new { id, turno.Nome },
            commandType: CommandType.StoredProcedure);
        return NoContent();
    }

    [HttpDelete("{id}", Name = "ExcluirTurnoProcedure")]
    public async Task<IActionResult> Excluir(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        var nomeProc = "dbo.ExcluirTurno";

        var linhasAfetadas = await connection.ExecuteScalarAsync<int>(nomeProc, new { id },
            commandType: CommandType.StoredProcedure);
        return NoContent();
    }
}
