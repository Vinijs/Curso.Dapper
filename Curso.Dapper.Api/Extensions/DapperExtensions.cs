using Dapper;
using Microsoft.SqlServer.Server;
using System.Collections.Generic;
using System.Data;

namespace Curso.Dapper.Api.Extensions;

public static class DapperExtensions
{
    public const string BUSCAR_POR_NOME_E_ID_TURNOS_TYPE = "[dbo].[NomesTurnosType]";

    private static IEnumerable<SqlDataRecord> CreateSqlDataRecord(Dictionary<int,string> dicionario)
    {
        var metaDataTexto = new SqlMetaData("Value", SqlDbType.Text);
        var metaDataNumeros = new SqlMetaData("Value", SqlDbType.Int);
        var record = new SqlDataRecord(metaDataTexto, metaDataNumeros);
        foreach (var item in dicionario)
        {
            record.SetString(0, item.Value);
            record.SetInt32(1, item.Key);
            yield return record;
        }
    }

    public static SqlMapper.ICustomQueryParameter GetTableValuedParameter(this Dictionary<int, string> list)
    {
        list ??= new Dictionary<int, string>();

        return CreateSqlDataRecord(list).AsTableValuedParameter(BUSCAR_POR_NOME_E_ID_TURNOS_TYPE);
    }
}
