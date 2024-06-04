using Curso.Dapper.ComEntity.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Entities = Curso.Dapper.ComEntity.Api.Domain.Entities;

namespace Curso.Dapper.ComEntity.Api.Data.Database.Context;

public class CursoDapperContext : DbContext
{
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CursoDapperContext).Assembly);
    }
}
