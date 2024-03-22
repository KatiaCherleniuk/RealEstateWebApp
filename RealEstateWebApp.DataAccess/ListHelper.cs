using System.Collections.Generic;
using System.Data;
using Dapper;

namespace RealEstateWebApp.DataAccess
{
    public static class ListHelper
    {
        public static DynamicParameters AsSqlIdTableParameters(this IEnumerable<int> idList, string parameterName)
        {
            var parameters = new DynamicParameters();
            parameters.Add(parameterName, idList.AsSqlIdTable());
            return parameters;
        }

        public static SqlMapper.ICustomQueryParameter AsSqlIdTable(this IEnumerable<int> idList)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            foreach (var id in idList)
                dataTable.Rows.Add(id);
            return dataTable.AsTableValuedParameter("utIntTable");
        }
    }
}