namespace Curso.Dapper.ComEntity.Api.Data.Database.Repositories;

public partial class CursoRepository
{
    const string _sqlObterTodos = @"SELECT Id, Nome, Descricao, TipoCurso, Coordenador, Professor, DataCriacao 
                    FROM dbo.Cursos (NOLOCK)";

    const string _sqlObterPorId = @"SELECT Id, Nome, Descricao, TipoCurso, Coordenador, Professor, DataCriacao
                    FROM dbo.Cursos (NOLOCK)
                    WHERE Id = @id";

    const string _sqlInserir = @"INSERT INTO dbo.Cursos (Nome, Descricao, TipoCurso, Coordenador, Professor, DataCriacao)
                    VALUES (@Nome, @Descricao, @TipoCurso, @Coordenador, @Professor, @DataCriacao)";

    const string _sqlAtualizar = @"UPDATE dbo.Cursos 
                  SET Nome = @Nome,
                      Descricao = @Descricao,
                      TipoCurso = @TipoCurso,
                      Coordenador = @Coordenador,
                      Professor = @Professor,
                      DataCriacao = @DataCriacao
                  WHERE Id = @id";

    const string _sqlExcluir = @"DELETE FROM dbo.Cursos Where Id = @id";
}
