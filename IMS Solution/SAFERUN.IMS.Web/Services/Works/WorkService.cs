
             
           
 

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
    public class WorkService : Service< Work >, IWorkService
    {

        private readonly IRepositoryAsync<Work> _repository;
		 private readonly IDataTableImportMappingService _mappingservice;
        public  WorkService(IRepositoryAsync< Work> repository,IDataTableImportMappingService mappingservice)
            : base(repository)
        {
            _repository=repository;
			_mappingservice = mappingservice;
        }
        
                 public  IEnumerable<Work> GetByWorkTypeId(int  worktypeid)
         {
            return _repository.GetByWorkTypeId(worktypeid);
         }
                  public  IEnumerable<Work> GetByOrderId(int  orderid)
         {
            return _repository.GetByOrderId(orderid);
         }
                          public IEnumerable<WorkDetail>   GetWorkDetailsByWorkId (int workid)
        {
            return _repository.GetWorkDetailsByWorkId(workid);
        }
         
        

		public void ImportDataTable(System.Data.DataTable datatable)
        {
            foreach (DataRow row in datatable.Rows)
            {
                 
                Work item = new Work();
                foreach (DataColumn col in datatable.Columns)
                {
                    var sourcefieldname = col.ColumnName;
                    var mapping = _mappingservice.FindMapping("Work", sourcefieldname);
                    if (mapping != null && row[sourcefieldname] != DBNull.Value)
                    {
                        
                        Type worktype = item.GetType();
						PropertyInfo propertyInfo = worktype.GetProperty(mapping.FieldName);
						propertyInfo.SetValue(item, Convert.ChangeType(row[sourcefieldname], propertyInfo.PropertyType), null);
                        //worktype.GetProperty(mapping.FieldName).SetValue(item, row[sourcefieldname]);
                    }

                }
                
                this.Insert(item);
               

            }
        }
    }
}



