

     
 
 
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
    public interface IScheduleDetailService:IService<ScheduleDetail>
    {

                  IEnumerable<ScheduleDetail> GetByParentSKUId(int  parentskuid);
                 IEnumerable<ScheduleDetail> GetByComponentSKUId(int  componentskuid);
                 IEnumerable<ScheduleDetail> GetByStationId(int  stationid);
                 IEnumerable<ScheduleDetail> GetByShiftId(int  shiftid);
                 IEnumerable<ScheduleDetail> GetByProductionScheduleId(int  productionscheduleid);
        
         
 
		void ImportDataTable(DataTable datatable);
	}
}