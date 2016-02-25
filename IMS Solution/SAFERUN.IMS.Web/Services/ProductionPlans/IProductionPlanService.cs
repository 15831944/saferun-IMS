

     
 
 
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
    public interface IProductionPlanService:IService<ProductionPlan>
    {

                  IEnumerable<ProductionPlan> GetBySKUId(int  skuid);
                 IEnumerable<ProductionPlan> GetByProductionProcessId(int  productionprocessid);
                 IEnumerable<ProductionPlan> GetByOrderId(int  orderid);
        
         
 
		void ImportDataTable(DataTable datatable);

        IEnumerable<ProductionTask> GenerateProductionTask(int[] plansid);
	}
}