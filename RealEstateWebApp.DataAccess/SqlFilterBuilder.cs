using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using RealEstateWebApp.Models.FilterAndOrder;
using RealEstateWebApp.Models.Property;
using Microsoft.Extensions.Logging;
using RealEstateWebApp.Models;

namespace RealEstateWebApp.DataAccess
{
    public class SqlFilterBuilder
    {
        private readonly ILogger<SqlFilterBuilder> _logger;

        public SqlFilterBuilder(ILogger<SqlFilterBuilder> logger)
        {
            _logger = logger;
        }

        public DynamicParameters Parameters { get; private set; }
        public string Sql { get; private set; }
        
        public void MakeSql(int categoryId, IEnumerable<BaseFilterValueModel> filters, BaseOrderModel order, ServiceType? type = null)
        {
            Parameters = new DynamicParameters();
            var filterSql = MakeFilters(categoryId, filters, type);
            Sql = MakeOrder(filterSql, order);
            _logger.LogDebug(Sql);
        }

        private string MakeOrder(string filterSql, BaseOrderModel order)
        {
            return order switch
            {
                OrderByPropertyModel orderByPropertyModel => MakeOrderByProperty(filterSql, orderByPropertyModel),
                OrderByRecordOwnFieldModel orderByRecordOwnFieldModel => MakeOrderByOwnField(filterSql, orderByRecordOwnFieldModel),
                _ => throw new ArgumentOutOfRangeException(nameof(order))
            };
        }

        private string MakeOrderByOwnField(string filterSql, OrderByRecordOwnFieldModel order)
        {
            var orderSql = order.IsDesc ? "DESC" : "ASC";
            
            return "SELECT R.Id \n"
                   + $"FROM (\n{filterSql}\n) as F \n"
                   + "LEFT JOIN Records AS R ON R.Id = F.RecordId\n"
                   + $"ORDER BY R.{order.RecordFieldName} {orderSql}, F.RecordId DESC"; //todo add escape here
        }

        private string MakeOrderByProperty(string filterSql, OrderByPropertyModel order)
        {
            var key = "@OrderProperty_"+order.PropertyId;
            Parameters.Add(key, order.PropertyId);
            
            var orderSql = order.IsDesc ? "DESC" : "ASC";
            
            var field = order.PropertyType switch
            {
                PropertyType.Text => "ValueString",
                PropertyType.Int => "ValueNumber",
                PropertyType.Float => "ValueNumber",
                PropertyType.ListSingle => "",
                _ => throw new ArgumentOutOfRangeException()
            };
            
            var orderPart = order.PropertyType == PropertyType.ListSingle 
                ? $"LEFT JOIN PropertyValues AS PV ON PV.Id = RPV.ValueId \nORDER BY PV.Title {orderSql}"
                : $"ORDER BY RPV.{field} {orderSql}";
            
            return "SELECT F.RecordId AS Id \n"
                   + $"FROM (\n{filterSql}\n) as F \n"
                   + $"LEFT JOIN RecordPropertyValues AS RPV ON RPV.RecordId = F.RecordId AND RPV.PropertyId = {key} \n"
                   + $"{orderPart} , F.RecordId DESC";
        }

        private string MakeFilters(int categoryId, IEnumerable<BaseFilterValueModel> filters, ServiceType? categoryType = null)
        {
            if (filters == null || !filters.Any())
            {
                var key = "@CategoryId"; 
                Parameters.Add(key, categoryId); 
                if(categoryType != null)
                {
                    var type = "@Type";
                    Parameters.Add(type, categoryType);
                    return $"SELECT Id AS RecordId FROM Records WHERE CategoryId = {key} AND Type = {type}";
                }

                return $"SELECT Id AS RecordId FROM Records WHERE CategoryId = {key}";
            }

            var filtersSql = new List<string>();
            foreach (var filter in filters)
            {
                var sql = filter switch
                {
                    ListFilterValueModel listFilterValueModel => MakeListSql(listFilterValueModel),
                    NumberFilterValueModel numberFilterValueModel => MakeNumberSql(numberFilterValueModel),
                    ValueIdFilterValueModel valueIdFilterValueModel => MakeValueIdSql(valueIdFilterValueModel),
                    StringFilterValueModel stringFilterValueModel => MakeStringSql(stringFilterValueModel),
                    _ => throw new ArgumentOutOfRangeException(nameof(filter))
                };
                filtersSql.Add(sql);
            }

            return string.Join("\nINTERSECT\n", filtersSql);
        }

        private string MakeListSql(ListFilterValueModel filter)
        {
            var key = "@Property_" + filter.PropertyId;
            var listCount = $"@Property_{filter.PropertyId}_ListCount";
            var list = $"@Property_{filter.PropertyId}_List";

            var sql = $"SELECT RecordId FROM RecordPropertyValues WHERE PropertyId = {key} and {listCount} = (" +
                      "\n\tSELECT COUNT (*) FROM (" +
                      "\n\t\tSELECT VALUE FROM STRING_SPLIT (ValueList, ',')" +
                      "\n\t\tINTERSECT" +
                      $"\n\t\tSELECT id FROM {list}" +
                      "\n\t) as i" +
                      "\n)";
            
            Parameters.Add(key, filter.PropertyId);
            Parameters.Add(listCount, filter.Values.Count());
            Parameters.Add(list, filter.Values.AsSqlIdTable());
            
            return sql;
        }

        private string MakeStringSql(StringFilterValueModel filter)
        {
            var key = "@Property_" + filter.PropertyId;
            var value = "@PropertyValue_" + filter.PropertyId;
            var sql = $"SELECT RecordId FROM RecordPropertyValues WHERE PropertyId = {key} AND ValueString LIKE {value}";
            
            Parameters.Add(key, filter.PropertyId);
            
            var likeValue = "%" + filter.Value
                .Replace("[", "[[]")
                .Replace("%", "[%]") + "%";
            Parameters.Add(value, likeValue);
            return sql;
        }

        private string MakeValueIdSql(ValueIdFilterValueModel filter)
        {
            var key = "@Property_" + filter.PropertyId;
            var value = "@PropertyValue_" + filter.PropertyId;
            var sql = $"SELECT RecordId FROM RecordPropertyValues WHERE PropertyId = {key} AND ValueId = {value}";
            
            Parameters.Add(key, filter.PropertyId);
            Parameters.Add(value, filter.ValueId);
            return sql;
        }

        private string MakeNumberSql(NumberFilterValueModel filter)
        {
            var key = "@Property_" + filter.PropertyId;

            var filters = new List<string>();
            if (filter.Min != null)
            {
                var valueMin = "@PropertyValueMin_" + filter.PropertyId;
                filters.Add(valueMin + " <= ValueNumber");
                Parameters.Add(valueMin, filter.Min);
            }
            if (filter.Max != null)
            {
                var valueMax = "@PropertyValueMax_" + filter.PropertyId;
                filters.Add("ValueNumber <= " + valueMax);
                Parameters.Add(valueMax, filter.Max);
            }

            var sql = $"SELECT RecordId FROM RecordPropertyValues WHERE PropertyId = {key} AND ({string.Join(" AND ", filters)})";
            
            Parameters.Add(key, filter.PropertyId);
            return sql;
        }
    }
}