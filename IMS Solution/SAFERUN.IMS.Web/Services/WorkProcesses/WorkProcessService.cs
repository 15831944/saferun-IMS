
             
           
 

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
    public class WorkProcessService : Service< WorkProcess >, IWorkProcessService
    {

        private readonly IRepositoryAsync<WorkProcess> _repository;
		 private readonly IDataTableImportMappingService _mappingservice;
        public  WorkProcessService(IRepositoryAsync< WorkProcess> repository,IDataTableImportMappingService mappingservice)
            : base(repository)
        {
            _repository=repository;
			_mappingservice = mappingservice;
        }
        
                 public  IEnumerable<WorkProcess> GetByWorkId(int  workid)
         {
            return _repository.GetByWorkId(workid);
         }
                  public  IEnumerable<WorkProcess> GetByOrderId(int  orderid)
         {
            return _repository.GetByOrderId(orderid);
         }
                  public  IEnumerable<WorkProcess> GetBySKUId(int  skuid)
         {
            return _repository.GetBySKUId(skuid);
         }
                  public  IEnumerable<WorkProcess> GetByProductionProcessId(int  productionprocessid)
         {
            return _repository.GetByProductionProcessId(productionprocessid);
         }
                  public  IEnumerable<WorkProcess> GetByWorkDetailId(int  workdetailid)
         {
            return _repository.GetByWorkDetailId(workdetailid);
         }
                  public  IEnumerable<WorkProcess> GetByCustomerId(int  customerid)
         {
            return _repository.GetByCustomerId(customerid);
         }
                          public IEnumerable<WorkProcessDetail>   GetWorkProcessDetailsByWorkProcessId (int workprocessid)
        {
            return _repository.GetWorkProcessDetailsByWorkProcessId(workprocessid);
        }
         
        

		public void ImportDataTable(System.Data.DataTable datatable)
        {
            foreach (DataRow row in datatable.Rows)
            {
                 
                WorkProcess item = new WorkProcess();
                foreach (DataColumn col in datatable.Columns)
                {
                    var sourcefieldname = col.ColumnName;
                    var mapping = _mappingservice.FindMapping("WorkProcess", sourcefieldname);
                    if (mapping != null && row[sourcefieldname] != DBNull.Value)
                    {
                        
                        Type workprocesstype = item.GetType();
						PropertyInfo propertyInfo = workprocesstype.GetProperty(mapping.FieldName);
						propertyInfo.SetValue(item, Convert.ChangeType(row[sourcefieldname], propertyInfo.PropertyType), null);
                        //workprocesstype.GetProperty(mapping.FieldName).SetValue(item, row[sourcefieldname]);
                    }

                }
                
                this.Insert(item);
               

            }
        }
    }
}



