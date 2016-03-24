

     
 
 
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
    public interface IWorkProcessService:IService<WorkProcess>
    {

                  IEnumerable<WorkProcess> GetByWorkId(int  workid);
                 IEnumerable<WorkProcess> GetByOrderId(int  orderid);
                 IEnumerable<WorkProcess> GetBySKUId(int  skuid);
                 IEnumerable<WorkProcess> GetByProductionProcessId(int  productionprocessid);
                 IEnumerable<WorkProcess> GetByWorkDetailId(int  workdetailid);
                 IEnumerable<WorkProcess> GetByCustomerId(int  customerid);
        
                 IEnumerable<WorkProcessDetail>   GetWorkProcessDetailsByWorkProcessId (int workprocessid);
         
         
 
		void ImportDataTable(DataTable datatable);

        void GenerateWorkProcesses(IEnumerable<WorkDetail> workdetails);
        void GenerateWorkProcesses(WorkProcess process);
        void CompletedWork(int id);
	}
}