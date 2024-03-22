using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace RealEstateWebApp.DataAccess
{
    public abstract class BaseDataController
    {
        private readonly IConfiguration _configuration;
        private readonly string _tableName;

        protected string ConnectionString => _configuration.GetConnectionString("DefaultConnection");

        protected BaseDataController(IConfiguration configuration, string tableName)
        {
            _configuration = configuration;
            _tableName = tableName;
        }

        protected async Task<DbConnection> GetConnection()
        {
            var db = new SqlConnection(ConnectionString);
            await db.OpenAsync();
            return db;
        }

        protected string PrepareStoredProcedureName(string storedProcedureEnding, bool customName = false)
        {
            return customName ? storedProcedureEnding : $"sp_{_tableName}_{storedProcedureEnding}";
        }

        protected async Task<bool> PerformNonQuery(string storedProcedureName, object parameters,
            string[] outputParamNames = null, bool customName = false)
        {
            var storedProcedure = PrepareStoredProcedureName(storedProcedureName, customName);
            var param = PrepareParams(parameters, outputParamNames);

            using (var connection = await GetConnection())
            {
                var rows = await connection.QueryAsync(storedProcedure, param, commandType: CommandType.StoredProcedure);
                TryBindOutputToModel(parameters, param, outputParamNames);
            }

            return true;
        }

        protected async Task<TModel> PerformScalar<TModel>(string storedProcedureName, object parameters, string[] outputParamNames = null, bool customName = false)
        {
            var storedProcedure = PrepareStoredProcedureName(storedProcedureName, customName);
            var param = PrepareParams(parameters, outputParamNames);

            using (var connection = await GetConnection())
            {
                var res = await connection.ExecuteScalarAsync<TModel>(storedProcedure, param, commandType: CommandType.StoredProcedure);
                TryBindOutputToModel(parameters, param, outputParamNames);

                return res;
            }
        }

        protected async Task<IEnumerable<TModel>> GetManyAsync<TModel>(string storedProcedureName, object parameters = null, string[] outputParamNames = null, bool customName = false)
            where TModel : class
        {
            var storedProcedure = PrepareStoredProcedureName(storedProcedureName, customName);
            var param = PrepareParams(parameters, outputParamNames);
            
            using (var connection = await GetConnection())
            {
                var rows = await connection.QueryAsync<TModel>(storedProcedure, param, commandType: CommandType.StoredProcedure);
                TryBindOutputToModel(parameters, param, outputParamNames);
                return rows;
            }
        }

        protected async Task<TModel> GetOneAsync<TModel>(string storedProcedureName, object parameters = null, string[] outputParamNames = null, bool customName = false)
        {
            var storedProcedure = PrepareStoredProcedureName(storedProcedureName, customName);
            var param = PrepareParams(parameters, outputParamNames);

            using (var connection = await GetConnection())
            {
                var row = await connection.QueryFirstOrDefaultAsync<TModel>(storedProcedure, param, commandType: CommandType.StoredProcedure);
                TryBindOutputToModel(parameters, param, outputParamNames);
                return row;
            }
        }

        private DynamicParameters PrepareParams(object parameters, string[] output)
        {
            var res = new DynamicParameters(parameters);
            if (output != null)
            {
                foreach (var outputName in output)
                    res.Add(outputName, direction: ParameterDirection.InputOutput);
            }

            return res;
        }

        protected void TryBindOutputToModel(object model, DynamicParameters dynamicParameters, string[] output)
        {
            if (output == null || output.Length == 0 || model is DynamicParameters)
                return;
            var properties = model.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var outputName in output)
            {
                var prop = properties.FirstOrDefault(info =>
                    string.Equals(info.Name, outputName, StringComparison.CurrentCultureIgnoreCase));
                if (prop == null)
                    continue;
                prop.SetValue(model, dynamicParameters.Get<object>(outputName));
            }
        }
    }
}