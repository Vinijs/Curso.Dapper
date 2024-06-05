using Curso.Dapper.ComEntity.Api.Data.Database.Interfaces;
using Curso.Dapper.ComEntity.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Entities = Curso.Dapper.ComEntity.Api.Domain.Entities;

namespace Curso.Dapper.ComEntity.Api.Data.Database.Context;

public class CursoDapperContext : DbContext, IUnitOfWork
{
    private IDbTransaction? _transaction;

    public CursoDapperContext(DbContextOptions<CursoDapperContext> options)
        : base(options)
    {
    }

    public DbSet<Entities.Curso> Cursos { get; set; } = null!;
    public DbSet<Aluno> Alunos { get; set; } = null!;
    public DbSet<AlunosCursos> AlunosCursos { get; set; } = null!;
    public DbSet<FilaEnvioEmail> FilaEnvioEmails { get; set; } = null!;
    public DbSet<Turma> Turmas { get; set; } = null!;
    public DbSet<Turno> Turnos { get; set; } = null!;

    public IDbTransaction Transaction 
    {
        get
        {
            if(_transaction is not null)
                return _transaction;

            var connection = Database.GetDbConnection();

            if (connection.State != ConnectionState.Open)
                connection.Open();

            _transaction = connection.BeginTransaction();

            return _transaction;
        }
    }

    public void Commit()
    {
        if(_transaction is null)
            return;

        _transaction.Commit();
        _transaction = null;
    }

    public void RollBack()
    {
        if (_transaction is null)
            return;

        _transaction.Rollback();
        _transaction = null;
    }

    public override void Dispose()
    {
        _transaction?.Dispose();
        base.Dispose();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CursoDapperContext).Assembly);
    }
}
