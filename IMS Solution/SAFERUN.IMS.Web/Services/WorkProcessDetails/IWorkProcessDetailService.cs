

     
 
 
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
    public interface IWorkProcessDetailService:IService<WorkProcessDetail>
    {

                  IEnumerable<WorkProcessDetail> GetByWorkProcessId(int  workprocessid);
                 IEnumerable<WorkProcessDetail> GetBySKUId(int  skuid);
                 IEnumerable<WorkProcessDetail> GetByProcessStepId(int  processstepid);
                 IEnumerable<WorkProcessDetail> GetByStationId(int  stationid);
        
         
 
		void ImportDataTable(DataTable datatable);

        void Start(int id);
        void Completed(int id);
	}
}