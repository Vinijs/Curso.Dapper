using Curso.Dapper.ComEntity.Api.Domain.Enums;
using Dapper;

namespace Curso.Dapper.ComEntity.Api.Data.Database.Extensions;

public static class DbStringExtensions
{
    public static DbString ToDbStringParam(this string parametro)
        => new()
        {
            Value = parametro,
            Length = parametro.Length,
            IsAnsi = true,
            IsFixedLength = false
        };

    public static DbString ToDbStringParam(this TipoCurso tipoCurso)
        => new()
        {
            Value = tipoCurso.ToString(),
            Length = tipoCurso.ToString().Length,
            IsAnsi = true,
            IsFixedLength = false
        };
}
