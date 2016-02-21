
             
           
 

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
    public class DepartmentService : Service< Department >, IDepartmentService
    {

        private readonly IRepositoryAsync<Department> _repository;
		 private readonly IDataTableImportMappingService _mappingservice;
        public  DepartmentService(IRepositoryAsync< Department> repository,IDataTableImportMappingService mappingservice)
            : base(repository)
        {
            _repository=repository;
			_mappingservice = mappingservice;
        }
        
                 public  IEnumerable<Department> GetByParentDepartmentId(int  parentdepartmentid)
         {
            return _repository.GetByParentDepartmentId(parentdepartmentid);
         }
                          public IEnumerable<Department>   GetDepartmentsByParentDepartmentId (int parentdepartmentid)
        {
            return _repository.GetDepartmentsByParentDepartmentId(parentdepartmentid);
        }
         
        

		public void ImportDataTable(System.Data.DataTable datatable)
        {
            foreach (DataRow row in datatable.Rows)
            {
                 
                Department item = new Department();
                foreach (DataColumn col in datatable.Columns)
                {
                    var sourcefieldname = col.ColumnName;
                    var mapping = _mappingservice.FindMapping("Department", sourcefieldname);
                    if (mapping != null && row[sourcefieldname] != DBNull.Value)
                    {
                        
                        Type departmenttype = item.GetType();
						PropertyInfo propertyInfo = departmenttype.GetProperty(mapping.FieldName);
						propertyInfo.SetValue(item, Convert.ChangeType(row[sourcefieldname], propertyInfo.PropertyType), null);
                        //departmenttype.GetProperty(mapping.FieldName).SetValue(item, row[sourcefieldname]);
                    }

                }
                
                this.Insert(item);
               

            }
        }
    }
}



