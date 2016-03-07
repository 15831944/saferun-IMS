
             
           
 

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Repository.Pattern.Repositories;
using Service.Pattern;

using SAFERUN.IMS.Web.Models;
using SAFERUN.IMS.Web.Repositories;

using System.Data;
using System.Reflection;
namespace SAFERUN.IMS.Web.Services
{
    public class ScheduleDetailService : Service< ScheduleDetail >, IScheduleDetailService
    {

        private readonly IRepositoryAsync<ScheduleDetail> _repository;
		 private readonly IDataTableImportMappingService _mappingservice;
        public  ScheduleDetailService(IRepositoryAsync< ScheduleDetail> repository,IDataTableImportMappingService mappingservice)
            : base(repository)
        {
            _repository=repository;
			_mappingservice = mappingservice;
        }
        
                 public  IEnumerable<ScheduleDetail> GetByParentSKUId(int  parentskuid)
         {
            return _repository.GetByParentSKUId(parentskuid);
         }
                  public  IEnumerable<ScheduleDetail> GetByComponentSKUId(int  componentskuid)
         {
            return _repository.GetByComponentSKUId(componentskuid);
         }
                  public  IEnumerable<ScheduleDetail> GetByStationId(int  stationid)
         {
            return _repository.GetByStationId(stationid);
         }
                  public  IEnumerable<ScheduleDetail> GetByShiftId(int  shiftid)
         {
            return _repository.GetByShiftId(shiftid);
         }
                  public  IEnumerable<ScheduleDetail> GetByProductionScheduleId(int  productionscheduleid)
         {
            return _repository.GetByProductionScheduleId(productionscheduleid);
         }
                   
        

		public void ImportDataTable(System.Data.DataTable datatable)
        {
            foreach (DataRow row in datatable.Rows)
            {
                 
                ScheduleDetail item = new ScheduleDetail();
                foreach (DataColumn col in datatable.Columns)
                {
                    var sourcefieldname = col.ColumnName;
                    var mapping = _mappingservice.FindMapping("ScheduleDetail", sourcefieldname);
                    if (mapping != null && row[sourcefieldname] != DBNull.Value)
                    {
                        
                        Type scheduledetailtype = item.GetType();
						PropertyInfo propertyInfo = scheduledetailtype.GetProperty(mapping.FieldName);
						propertyInfo.SetValue(item, Convert.ChangeType(row[sourcefieldname], propertyInfo.PropertyType), null);
                        //scheduledetailtype.GetProperty(mapping.FieldName).SetValue(item, row[sourcefieldname]);
                    }

                }
                
                this.Insert(item);
               

            }
        }
    }
}



