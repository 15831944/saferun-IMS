
             
           
 

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
    public class SKUService : Service< SKU >, ISKUService
    {

        private readonly IRepositoryAsync<SKU> _repository;
        private readonly IDataTableImportMappingService _mappingservice;
        public SKUService(IRepositoryAsync<SKU> repository, IDataTableImportMappingService mappingservice)
            : base(repository)
        {
            _repository=repository;
            _mappingservice = mappingservice;
        }




        public void ImportDataTable(System.Data.DataTable datatable)
        {
            foreach (DataRow row in datatable.Rows)
            {
                
                SKU item = new SKU();
                foreach (DataColumn col in datatable.Columns)
                {
                    var sourcefieldname = col.ColumnName;
                    var mapping = _mappingservice.FindMapping("SKU", sourcefieldname);
                    if (mapping != null && row[sourcefieldname] != DBNull.Value)
                    {
                        
                        Type skutype = item.GetType();
                        skutype.GetProperty(mapping.FieldName).SetValue(item, row[sourcefieldname]);
                    }

                }
                if (Find(item.Sku) == null)
                {
                    this.Insert(item);
                }

            }
        }


        public SKU Find(string sku)
        {
            return this.Queryable().Where(x => x.Sku == sku).FirstOrDefault();
        }
    }
}



