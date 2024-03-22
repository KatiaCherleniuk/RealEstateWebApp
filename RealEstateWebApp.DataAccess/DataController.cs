using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace RealEstateWebApp.DataAccess
{
    public abstract class DataController : BaseDataController
    {
        public DataController(IConfiguration configuration, string tableName) : base(configuration, tableName)
        {
        }

        protected Task<bool> InsertAsync<T>(T parameters, string[] outputParamNames = null)
            where T :new()
        {
            return PerformNonQuerySafe("Insert", parameters, outputParamNames);
        }

        protected Task<bool> DeleteByIdAsync<T>(T id)
        {
            return PerformNonQuery("Delete", new {Id = id});
        }

        protected Task<bool> InsertWithIdAsync<T>(T parameters)
            where T :new()
        {
            return InsertAsync(parameters, new string[1] {"Id"});
        }

        protected Task<bool> UpdateAsync<T>(T parameters)
            where T :new()
        {
            return PerformNonQuerySafe("Update", parameters);
        }

        protected Task<TModel> GetByIdAsync<TId, TModel>(TId id)
        {
            return GetOneAsync<TModel>("GetById", new {Id = id});
        }

        protected async Task<bool> PerformNonQuerySafe<T>(string storedProcedureName, T parametersModel, string[] outputParamNames = null)
            where T :new()
        {
            var data = new T();
            var properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var prop in properties)
            {
                prop.SetValue(data, prop.GetValue(parametersModel));
            }

            var res = await PerformNonQuery(storedProcedureName, data, outputParamNames);

            if (outputParamNames != null)
            {
                foreach (var outputName in outputParamNames)
                {
                    var prop = properties.FirstOrDefault(info => string.Equals(info.Name, outputName, StringComparison.CurrentCultureIgnoreCase));
                    if (prop != null)
                        prop.SetValue(parametersModel, prop.GetValue(data));
                }
            }

            return res;
        }
        
    }
}