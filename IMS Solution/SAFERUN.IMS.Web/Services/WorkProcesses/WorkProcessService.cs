
             
           
 

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
    public class WorkProcessService : Service< WorkProcess >, IWorkProcessService
    {

        private readonly IRepositoryAsync<WorkProcess> _repository;
		 private readonly IDataTableImportMappingService _mappingservice;
         private readonly IWorkDetailService _workdetailservice;
         private readonly IProcessStepService _setpservice;
         private readonly IWorkProcessDetailService _processdetailservice;
        public  WorkProcessService( IWorkProcessDetailService processdetailservice,IProcessStepService setpservice,IWorkDetailService workdetailservice,IRepositoryAsync< WorkProcess> repository,IDataTableImportMappingService mappingservice)
            : base(repository)
        {
            _repository=repository;
			_mappingservice = mappingservice;
            _workdetailservice = workdetailservice;
            _setpservice = setpservice;
            _processdetailservice = processdetailservice;
        }
        
                 public  IEnumerable<WorkProcess> GetByWorkId(int  workid)
         {
            return _repository.GetByWorkId(workid);
         }
                  public  IEnumerable<WorkProcess> GetByOrderId(int  orderid)
         {
            return _repository.GetByOrderId(orderid);
         }
                  public  IEnumerable<WorkProcess> GetBySKUId(int  skuid)
         {
            return _repository.GetBySKUId(skuid);
         }
                  public  IEnumerable<WorkProcess> GetByProductionProcessId(int  productionprocessid)
         {
            return _repository.GetByProductionProcessId(productionprocessid);
         }
                  public  IEnumerable<WorkProcess> GetByWorkDetailId(int  workdetailid)
         {
            return _repository.GetByWorkDetailId(workdetailid);
         }
                  public  IEnumerable<WorkProcess> GetByCustomerId(int  customerid)
         {
            return _repository.GetByCustomerId(customerid);
         }
                          public IEnumerable<WorkProcessDetail>   GetWorkProcessDetailsByWorkProcessId (int workprocessid)
        {
            return _repository.GetWorkProcessDetailsByWorkProcessId(workprocessid);
        }
         
        

		public void ImportDataTable(System.Data.DataTable datatable)
        {
            foreach (DataRow row in datatable.Rows)
            {
                 
                WorkProcess item = new WorkProcess();
                foreach (DataColumn col in datatable.Columns)
                {
                    var sourcefieldname = col.ColumnName;
                    var mapping = _mappingservice.FindMapping("WorkProcess", sourcefieldname);
                    if (mapping != null && row[sourcefieldname] != DBNull.Value)
                    {
                        
                        Type workprocesstype = item.GetType();
						PropertyInfo propertyInfo = workprocesstype.GetProperty(mapping.FieldName);
						propertyInfo.SetValue(item, Convert.ChangeType(row[sourcefieldname], propertyInfo.PropertyType), null);
                        //workprocesstype.GetProperty(mapping.FieldName).SetValue(item, row[sourcefieldname]);
                    }

                }
                
                this.Insert(item);
               

            }
        }


        public void GenerateWorkProcesses(IEnumerable<WorkDetail> workdetails)
        {
            var list = new List<WorkProcess>();
            foreach (var workdetail in workdetails)
            {
                var detail = _workdetailservice.Queryable().Include(x=>x.Work).Include(x=>x.Work.Order).Where(x=>x.Id==workdetail.Id).First();
                WorkProcess item = new WorkProcess();
                item.CustomerId = detail.Work.Order.CustomerId;
                item.FinishedQty = 0;
                item.GraphSKU = detail.GraphSKU;
                item.Operator = "";
                item.OrderId = detail.Work.OrderId;
                item.OrderKey = detail.Work.OrderKey;
                item.ProjectName = detail.Work.Order.ProjectName;
                //item.ProductionProcessId = 1;
                item.RequirementQty = detail.RequirementQty;
                item.ProductionQty = workdetail.ProductionQty.Value;
                item.SKUId = detail.ComponentSKUId;
                item.WorkDate = DateTime.Now;
                item.WorkId = detail.WorkId;
                item.WorkDetailId = detail.Id;
                item.WorkItems = 0;
                item.WorkNo = detail.WorkNo;
                list.Add(item);

                detail.ProductionQty = workdetail.RequirementQty - workdetail.ProductionQty - workdetail.DoingQty - workdetail.FinishedQty;
                detail.DoingQty = (detail.DoingQty == null ? 0 : detail.DoingQty) + workdetail.ProductionQty.Value;
                if (detail.DoingQty == detail.RequirementQty)
                {
                    detail.Status = 2;
                }
                else if (detail.DoingQty > 0 && detail.DoingQty < detail.RequirementQty)
                {
                    detail.Status = 1;
                }
                else if (detail.FinishedQty == detail.RequirementQty)
                {
                    detail.Status = 3;
                }

                this._workdetailservice.Update(detail);


            }

            this.InsertRange(list);
        }


        public void GenerateWorkProcesses(WorkProcess process)
        {
            var workprocess = this.Queryable().Include(x => x.SKU).Where(x => x.Id == process.Id).First();
            workprocess.Status = 1;
            this.Update(workprocess);
            var steps = _setpservice.Queryable().Where(x => x.ProductionProcessId == process.ProductionProcessId).ToList();
            var list = new List<WorkProcessDetail>();
            foreach (var setp in steps) {
                if (!_processdetailservice.Queryable().Where(x => x.ProcessStepId == setp.Id).Any())
                {
                    WorkProcessDetail item = new WorkProcessDetail();
                    item.WorkProcessId = process.Id;
                    item.Operator = "";
                    item.SKUId = workprocess.SKUId;
                    item.ComponentSKU = workprocess.SKU.Sku;
                    item.GraphSKU = workprocess.GraphSKU;
                    item.ProcessStepId = setp.Id;
                    item.StandardElapsedTime = setp.ElapsedTime;
                    item.StationId = setp.StationId;
                    item.StepName = setp.StepName;
                    item.Status = 0;
                    list.Add(item);
                }
            }
            _processdetailservice.InsertRange(list);
        }


        public void CompletedWork(int id)
        {
            if (!this._processdetailservice.Queryable().Where(x => x.WorkProcessId == id && x.Status != 2).Any())
            {

                var workProcess = this.Find(id);
                workProcess.Status = 3;
                workProcess.FinishedQty = workProcess.ProductionQty;
                this.Update(workProcess);
            }

            if (this._processdetailservice.Queryable().Where(x => x.WorkProcessId == id && (x.Status == 1 || x.Status == 0) ).Any())
            {

                var workProcess = this.Find(id);
                workProcess.Status = 2;
                workProcess.FinishedQty = workProcess.ProductionQty;
                this.Update(workProcess);
            }

        }
    }
}



