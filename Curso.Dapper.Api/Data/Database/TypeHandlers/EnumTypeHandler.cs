using Dapper;
using System.Data;

namespace Curso.Dapper.Api.Data.Database.TypeHandlers;

public class EnumTypeHandler<TEnum> : SqlMapper.TypeHandler<TEnum>
    where TEnum : struct, Enum
{
    public override TEnum Parse(object value)
    {
        return (TEnum)Enum.Parse(typeof(TEnum), value.ToString());
    }
    public override void SetValue(IDbDataParameter parameter, TEnum value)
    {
        parameter.Value = value.ToString();
    }
}
