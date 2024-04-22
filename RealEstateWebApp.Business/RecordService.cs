using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstateWebApp.DataAccess.Repositories;
using RealEstateWebApp.DataAccess.Repositories.Records;
using RealEstateWebApp.Models.FilterAndOrder;
using RealEstateWebApp.Models.RecordValue;
using RealEstateWebApp.Models.RecordViewModels;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using RealEstateWebApp.Models.Record;
using Newtonsoft.Json;
using RealEstateWebApp.Models.Address;
using RealEstateWebApp.Models;

namespace RealEstateWebApp.Business
{
    public class RecordService
    {
        private readonly IRecordRepository _recordRepository;
        private readonly IRecordValueRepository _recordValueRepository;
        private readonly ILogger<FilterRequestModel> _filterRequestLogger;
        private readonly FileService _fileService;
        public RecordService(IRecordRepository recordRepository,
            IRecordValueRepository recordValueRepository,
            // ReSharper disable once ContextualLoggerProblem
            ILogger<FilterRequestModel> filterRequestLogger,
            FileService fileService)
        {
            _recordRepository = recordRepository;
            _recordValueRepository = recordValueRepository;
            _filterRequestLogger = filterRequestLogger;
            _fileService = fileService;
        }

        public RecordEditViewModel MakeNewRecordForCreate(int categoryId)
        {
            return new RecordEditViewModel()
            {
                CategoryId = categoryId,
                Values = new List<RecordPropertyValueBasicModel>()
            };
        }

        public async Task<RecordEditViewModel> GetRecordForEdit(int recordId)
        {
            var valuesTask =  _recordValueRepository.GetRecordValuesByRecordId(recordId);
            var recordTask = _recordRepository.GetRecordForEdit(recordId);

            await Task.WhenAll(valuesTask, recordTask);
            var res = new RecordEditViewModel(recordTask.Result.ToBasicModel())
            {
                Values = valuesTask.Result.ToList()
            };

            return res;
        }
        
        public async Task Create(RecordEditViewModel recordModel)
        {
            recordModel.CreatedAt = DateTime.Now;
            var recordSQl = new RecordSQLModel(recordModel);
            await _recordRepository.Create(recordSQl);
            var tasks = new List<Task>();
            foreach (var value in recordModel.Values)
            {
                value.RecordId = recordSQl.Id;
                var task = _recordValueRepository.UpdateOrCreate(new RecordPropertyValueEditModel(value));
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);
        }

        public async Task Update(RecordEditViewModel recordModel)
        {
            await _recordRepository.Update(new RecordSQLModel(recordModel));
            var tasks = new List<Task>();
            foreach (var value in recordModel.Values)
            {
                value.RecordId = recordModel.Id;
                var task = _recordValueRepository.UpdateOrCreate(new RecordPropertyValueEditModel(value));
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);
        }

        public async Task<FilterResponseModel> GetRecordsByFilterRequest(FilterRequestModel filterModel)
        {
            var recordIdList = await _recordRepository.GetRecordsIdByFiltersAndOrder(filterModel.CategoryId, filterModel.Filters, filterModel.Order);
            var pageIdList = recordIdList.Skip((filterModel.Page - 1) * filterModel.PageSize).Take(filterModel.PageSize).ToArray();
            var recordsTask = _recordRepository.GetRecordForViewSimplifiedByIdList(pageIdList);
            var valuesTask =  _recordValueRepository.GetRecordValuesByIdList(pageIdList);
            await Task.WhenAll(recordsTask, valuesTask);
            
            var res = new FilterResponseModel
            {
                TotalItems = recordIdList.Count(),
                Records = recordsTask.Result.Select(r => {
                        r.Values = valuesTask.Result.Where(v => v.RecordId == r.Id);
                        return r;
                    }
                )
            };

            LogFilterRequest(filterModel, res.TotalItems);
            
            return res;
        }
        public async Task<FilterResponseModel> GetRecordsByFilterRequestForCards(FilterRequestModel filterModel, ServiceType? type)
        {
            var recordIdList = await _recordRepository.GetRecordsIdByFiltersAndOrder(filterModel.CategoryId, filterModel.Filters, filterModel.Order, type);
            var pageIdList = recordIdList.Skip((filterModel.Page - 1) * filterModel.PageSize).Take(filterModel.PageSize).ToArray();
            var recordsTask = _recordRepository.GetRecordForViewSimplifiedByIdList(pageIdList);
            await Task.WhenAll(recordsTask);
            var res = new FilterResponseModel
            {
                TotalItems = recordIdList.Count(),
                Records = recordsTask.Result
            };
            return res;
        }



        private void LogFilterRequest(FilterRequestModel filterModel, int totalItems)
        {
            const LogLevel logLevel = LogLevel.Debug;
            if (!_filterRequestLogger.IsEnabled(logLevel))
                return;
            
            var debStr = new StringBuilder();
            debStr.AppendLine(new string('-', 10));
            debStr.AppendLine($"page: {filterModel.Page}, pageSize: {filterModel.PageSize}");
            debStr.AppendLine("filters:");
            foreach (var filter in filterModel.Filters)
            {
                debStr.Append($"PropertyId: {filter.PropertyId} ");
                switch (filter)
                {
                    case StringFilterValueModel filterStr:
                        debStr.Append("(string) ");
                        debStr.Append($"'{filterStr.Value}'");
                        break;
                    
                    case NumberFilterValueModel filterNumber:
                        debStr.Append("(number) ");
                        debStr.Append($"min: {filterNumber.Min}, max: {filterNumber.Max}");
                        break;
                    
                    case ValueIdFilterValueModel filterValueId:
                        debStr.Append("(valueId) = ");
                        debStr.Append(filterValueId.ValueId);
                        break;
                    
                    case ListFilterValueModel filterList:
                        debStr.Append("(list) in [");
                        debStr.Append(string.Join(',', filterList.Values));
                        debStr.Append(']');
                        break;
                    
                    default:
                        debStr.Append("(unknown)");
                        break;
                }

                debStr.AppendLine();

            }
            debStr.AppendLine(new string('-', 10));
            debStr.AppendLine($"RES\ttotal items: {totalItems}");
            debStr.AppendLine(new string('-', 10));
            _filterRequestLogger.Log(logLevel, debStr.ToString());
        }

        public async Task<bool> HasRecordsForUser(int itemId)
        {
            //todo add author for records
            return true;
        }
        public async Task<bool> DeleteRecord(int recordId, int categoryId)
        {
            _fileService.DeleteRecordFiles(recordId, categoryId);
            return await _recordRepository.Delete(recordId);
        }
    }
}