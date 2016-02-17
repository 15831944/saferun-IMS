

     
 
 
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
    public interface IDefectTrackingService:IService<DefectTracking>
    {

                  IEnumerable<DefectTracking> GetByOrderId(int  orderid);
                 IEnumerable<DefectTracking> GetBySKUId(int  skuid);
                 IEnumerable<DefectTracking> GetByDefectTypeId(int  defecttypeid);
                 IEnumerable<DefectTracking> GetByDefectId(int  defectid);
        
         
 
		void ImportDataTable(DataTable datatable);
	}
}