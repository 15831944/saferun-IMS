
             
           
 

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
    public class RepairJobService : Service< RepairJob >, IRepairJobService
    {

        private readonly IRepositoryAsync<RepairJob> _repository;
		 private readonly IDataTableImportMappingService _mappingservice;
        public  RepairJobService(IRepositoryAsync< RepairJob> repository,IDataTableImportMappingService mappingservice)
            : base(repository)
        {
            _repository=repository;
			_mappingservice = mappingservice;
        }
        
                 public  IEnumerable<RepairJob> GetBySKUId(int  skuid)
         {
            return _repository.GetBySKUId(skuid);
         }
                  public  IEnumerable<RepairJob> GetByOrderId(int  orderid)
         {
            return _repository.GetByOrderId(orderid);
         }
                   
        

		public void ImportDataTable(System.Data.DataTable datatable)
        {
            foreach (DataRow row in datatable.Rows)
            {
                 
                RepairJob item = new RepairJob();
                foreach (DataColumn col in datatable.Columns)
                {
                    var sourcefieldname = col.ColumnName;
                    var mapping = _mappingservice.FindMapping("RepairJob", sourcefieldname);
                    if (mapping != null && row[sourcefieldname] != DBNull.Value)
                    {
                        
                        Type repairjobtype = item.GetType();
						PropertyInfo propertyInfo = repairjobtype.GetProperty(mapping.FieldName);
						propertyInfo.SetValue(item, Convert.ChangeType(row[sourcefieldname], propertyInfo.PropertyType), null);
                        //repairjobtype.GetProperty(mapping.FieldName).SetValue(item, row[sourcefieldname]);
                    }

                }
                
                this.Insert(item);
               

            }
        }
    }
}



