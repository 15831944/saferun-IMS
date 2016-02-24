
             
           
 

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
    public class ProductionTaskService : Service< ProductionTask >, IProductionTaskService
    {

        private readonly IRepositoryAsync<ProductionTask> _repository;
		 private readonly IDataTableImportMappingService _mappingservice;
        public  ProductionTaskService(IRepositoryAsync< ProductionTask> repository,IDataTableImportMappingService mappingservice)
            : base(repository)
        {
            _repository=repository;
			_mappingservice = mappingservice;
        }
        
                 public  IEnumerable<ProductionTask> GetBySKUId(int  skuid)
         {
            return _repository.GetBySKUId(skuid);
         }
                  public  IEnumerable<ProductionTask> GetByOrderId(int  orderid)
         {
            return _repository.GetByOrderId(orderid);
         }
                   
        

		public void ImportDataTable(System.Data.DataTable datatable)
        {
            foreach (DataRow row in datatable.Rows)
            {
                 
                ProductionTask item = new ProductionTask();
                foreach (DataColumn col in datatable.Columns)
                {
                    var sourcefieldname = col.ColumnName;
                    var mapping = _mappingservice.FindMapping("ProductionTask", sourcefieldname);
                    if (mapping != null && row[sourcefieldname] != DBNull.Value)
                    {
                        
                        Type productiontasktype = item.GetType();
						PropertyInfo propertyInfo = productiontasktype.GetProperty(mapping.FieldName);
						propertyInfo.SetValue(item, Convert.ChangeType(row[sourcefieldname], propertyInfo.PropertyType), null);
                        //productiontasktype.GetProperty(mapping.FieldName).SetValue(item, row[sourcefieldname]);
                    }

                }
                
                this.Insert(item);
               

            }
        }
    }
}



