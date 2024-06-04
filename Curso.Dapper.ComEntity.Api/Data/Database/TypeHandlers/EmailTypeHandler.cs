using Curso.Dapper.ComEntity.Api.Domain.ValueObjects;
using Dapper;
using System.Data;

namespace Curso.Dapper.ComEntity.Api.Data.Database.TypeHandlers;

public class EmailTypeHandler : SqlMapper.TypeHandler<Email>
{
    public override void SetValue(IDbDataParameter parameter, Email email)
    {
        parameter.Value = email.Endereco;
    }
    public override Email Parse(object email)
    {
        return new Email(email.ToString()!);
    }
}
