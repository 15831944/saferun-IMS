
             
           
 

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
    public class DefectTrackingService : Service< DefectTracking >, IDefectTrackingService
    {

        private readonly IRepositoryAsync<DefectTracking> _repository;
		 private readonly IDataTableImportMappingService _mappingservice;
        public  DefectTrackingService(IRepositoryAsync< DefectTracking> repository,IDataTableImportMappingService mappingservice)
            : base(repository)
        {
            _repository=repository;
			_mappingservice = mappingservice;
        }
        
                 public  IEnumerable<DefectTracking> GetByOrderId(int  orderid)
         {
            return _repository.GetByOrderId(orderid);
         }
                  public  IEnumerable<DefectTracking> GetBySKUId(int  skuid)
         {
            return _repository.GetBySKUId(skuid);
         }
                  public  IEnumerable<DefectTracking> GetByDefectTypeId(int  defecttypeid)
         {
            return _repository.GetByDefectTypeId(defecttypeid);
         }
                  public  IEnumerable<DefectTracking> GetByDefectId(int  defectid)
         {
            return _repository.GetByDefectId(defectid);
         }
                   
        

		public void ImportDataTable(System.Data.DataTable datatable)
        {
            foreach (DataRow row in datatable.Rows)
            {
                 
                DefectTracking item = new DefectTracking();
                foreach (DataColumn col in datatable.Columns)
                {
                    var sourcefieldname = col.ColumnName;
                    var mapping = _mappingservice.FindMapping("DefectTracking", sourcefieldname);
                    if (mapping != null && row[sourcefieldname] != DBNull.Value)
                    {
                        
                        Type defecttrackingtype = item.GetType();
                        defecttrackingtype.GetProperty(mapping.FieldName).SetValue(item, row[sourcefieldname]);
                    }

                }
                
                this.Insert(item);
               

            }
        }
    }
}



