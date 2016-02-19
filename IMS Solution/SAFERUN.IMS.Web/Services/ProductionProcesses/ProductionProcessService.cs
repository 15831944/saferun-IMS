
             
           
 

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

namespace SAFERUN.IMS.Web.Services
{
    public class ProductionProcessService : Service< ProductionProcess >, IProductionProcessService
    {

        private readonly IRepositoryAsync<ProductionProcess> _repository;
		 private readonly IDataTableImportMappingService _mappingservice;
        public  ProductionProcessService(IRepositoryAsync< ProductionProcess> repository,IDataTableImportMappingService mappingservice)
            : base(repository)
        {
            _repository=repository;
			_mappingservice = mappingservice;
        }
        
                  
        

		public void ImportDataTable(System.Data.DataTable datatable)
        {
            foreach (DataRow row in datatable.Rows)
            {
                 
                ProductionProcess item = new ProductionProcess();
                foreach (DataColumn col in datatable.Columns)
                {
                    var sourcefieldname = col.ColumnName;
                    var mapping = _mappingservice.FindMapping("ProductionProcess", sourcefieldname);
                    if (mapping != null && row[sourcefieldname] != DBNull.Value)
                    {
                        
                        Type productionprocesstype = item.GetType();
                        productionprocesstype.GetProperty(mapping.FieldName).SetValue(item, row[sourcefieldname]);
                    }

                }
                
                this.Insert(item);
               

            }
        }
    }
}



