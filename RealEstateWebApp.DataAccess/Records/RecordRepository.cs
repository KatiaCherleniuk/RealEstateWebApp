using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using RealEstateWebApp.Models.FilterAndOrder;
using RealEstateWebApp.Models.Record;
using RealEstateWebApp.Models.RecordViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RealEstateWebApp.Models;

namespace RealEstateWebApp.DataAccess.Repositories.Records
{
    public class RecordRepository : DataController, IRecordRepository
    {
        private SqlFilterBuilder _sqlBuilder;

        public RecordRepository(
            IConfiguration configuration,
            // ReSharper disable once ContextualLoggerProblem
            ILogger<SqlFilterBuilder> sqlFilterBuilderLogger) : base(configuration, "Records")
        {
            _sqlBuilder = new SqlFilterBuilder(sqlFilterBuilderLogger);
        }

        public Task<bool> Create(RecordSQLModel recordModel)
        {
            return InsertWithIdAsync(recordModel);
        }

        public Task<bool> Delete(int recordId)
        {
            return DeleteByIdAsync(recordId);
        }

        public Task<RecordSQLModel> GetRecordForEdit(int recordId)
        {
            return GetByIdAsync<int, RecordSQLModel>(recordId);
        }

        public Task<bool> Update(RecordSQLModel recordModel)
        {
            return UpdateAsync(recordModel);
        }

        public async Task<IEnumerable<int>> GetRecordsIdByFiltersAndOrder(int categoryId, IEnumerable<BaseFilterValueModel> filters, BaseOrderModel order, ServiceType? type = null)
        {
            _sqlBuilder.MakeSql(categoryId, filters, order, type);
            using (var connection = await GetConnection())
            {
                var rows = await connection.QueryAsync<int>(_sqlBuilder.Sql, _sqlBuilder.Parameters);
                return rows;
            }
        }

        public Task<IEnumerable<RecordSimplifiedViewModel>> GetRecordForViewSimplifiedByIdList(IEnumerable<int> idList)
        {
            return GetManyAsync<RecordSimplifiedViewModel>("GetRecordForViewSimplifiedByIdList", idList.AsSqlIdTableParameters("@IdList"));
        }
    }
}