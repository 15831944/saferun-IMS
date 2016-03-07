
             
           
 

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
    public class WorkDetailService : Service< WorkDetail >, IWorkDetailService
    {

        private readonly IRepositoryAsync<WorkDetail> _repository;
		 private readonly IDataTableImportMappingService _mappingservice;
        public  WorkDetailService(IRepositoryAsync< WorkDetail> repository,IDataTableImportMappingService mappingservice)
            : base(repository)
        {
            _repository=repository;
			_mappingservice = mappingservice;
        }
        
                 public  IEnumerable<WorkDetail> GetByWorkId(int  workid)
         {
            return _repository.GetByWorkId(workid);
         }
                  public  IEnumerable<WorkDetail> GetByParentSKUId(int  parentskuid)
         {
            return _repository.GetByParentSKUId(parentskuid);
         }
                  public  IEnumerable<WorkDetail> GetByComponentSKUId(int  componentskuid)
         {
            return _repository.GetByComponentSKUId(componentskuid);
         }
                   
        

		public void ImportDataTable(System.Data.DataTable datatable)
        {
            foreach (DataRow row in datatable.Rows)
            {
                 
                WorkDetail item = new WorkDetail();
                foreach (DataColumn col in datatable.Columns)
                {
                    var sourcefieldname = col.ColumnName;
                    var mapping = _mappingservice.FindMapping("WorkDetail", sourcefieldname);
                    if (mapping != null && row[sourcefieldname] != DBNull.Value)
                    {
                        
                        Type workdetailtype = item.GetType();
						PropertyInfo propertyInfo = workdetailtype.GetProperty(mapping.FieldName);
						propertyInfo.SetValue(item, Convert.ChangeType(row[sourcefieldname], propertyInfo.PropertyType), null);
                        //workdetailtype.GetProperty(mapping.FieldName).SetValue(item, row[sourcefieldname]);
                    }

                }
                
                this.Insert(item);
               

            }
        }
    }
}



