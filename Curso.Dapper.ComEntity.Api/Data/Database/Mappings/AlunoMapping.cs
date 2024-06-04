using Curso.Dapper.ComEntity.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Curso.Dapper.ComEntity.Api.Data.Database.Mappings;

public class AlunoMapping : IEntityTypeConfiguration<Aluno>
{
    public void Configure(EntityTypeBuilder<Aluno> builder)
    {
        builder.ToTable("Alunos");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("Id");

        builder.Property(x => x.Nome)
            .HasColumnName("Nome")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.DataNascimento)
            .HasColumnName("DataNascimento")
            .IsRequired();

        builder.Property(x => x.Ativo)
            .HasColumnName("Ativo")
            .IsRequired();

        builder.Property(x => x.DataCriacao)
            .HasColumnName("DataCriacao")
            .IsRequired();

        builder.OwnsOne(x => x.Email, email =>
        {
            email.Property(x => x.Endereco)
            .HasColumnName("Email")
            .HasMaxLength(100)
            .IsRequired();
        });
    }
}
