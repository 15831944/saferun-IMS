
             
           
 

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
    public class ProductionScheduleService : Service< ProductionSchedule >, IProductionScheduleService
    {

        private readonly IRepositoryAsync<ProductionSchedule> _repository;
		 private readonly IDataTableImportMappingService _mappingservice;
        public  ProductionScheduleService(IRepositoryAsync< ProductionSchedule> repository,IDataTableImportMappingService mappingservice)
            : base(repository)
        {
            _repository=repository;
			_mappingservice = mappingservice;
        }
        
                 public  IEnumerable<ProductionSchedule> GetByWorkId(int  workid)
         {
            return _repository.GetByWorkId(workid);
         }
                  public  IEnumerable<ProductionSchedule> GetByCustomerId(int  customerid)
         {
            return _repository.GetByCustomerId(customerid);
         }
                          public IEnumerable<ScheduleDetail>   GetScheduleDetailsByProductionScheduleId (int productionscheduleid)
        {
            return _repository.GetScheduleDetailsByProductionScheduleId(productionscheduleid);
        }
         
        

		public void ImportDataTable(System.Data.DataTable datatable)
        {
            foreach (DataRow row in datatable.Rows)
            {
                 
                ProductionSchedule item = new ProductionSchedule();
                foreach (DataColumn col in datatable.Columns)
                {
                    var sourcefieldname = col.ColumnName;
                    var mapping = _mappingservice.FindMapping("ProductionSchedule", sourcefieldname);
                    if (mapping != null && row[sourcefieldname] != DBNull.Value)
                    {
                        
                        Type productionscheduletype = item.GetType();
						PropertyInfo propertyInfo = productionscheduletype.GetProperty(mapping.FieldName);
						propertyInfo.SetValue(item, Convert.ChangeType(row[sourcefieldname], propertyInfo.PropertyType), null);
                        //productionscheduletype.GetProperty(mapping.FieldName).SetValue(item, row[sourcefieldname]);
                    }

                }
                
                this.Insert(item);
               

            }
        }
    }
}



