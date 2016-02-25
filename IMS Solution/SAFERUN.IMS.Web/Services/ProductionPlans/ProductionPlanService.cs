




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
    public class ProductionPlanService : Service<ProductionPlan>, IProductionPlanService
    {

        private readonly IRepositoryAsync<ProductionPlan> _repository;
        private readonly IDataTableImportMappingService _mappingservice;
        private readonly IProductionTaskService _taskservice;
        private readonly IProcessStepService _stepservice;
        public ProductionPlanService(IProcessStepService stepservice,IProductionTaskService taskservice,IRepositoryAsync<ProductionPlan> repository, IDataTableImportMappingService mappingservice)
            : base(repository)
        {
            _repository = repository;
            _mappingservice = mappingservice;
            _taskservice = taskservice;
            _stepservice = stepservice;
        }

        public IEnumerable<ProductionPlan> GetBySKUId(int skuid)
        {
            return _repository.GetBySKUId(skuid);
        }
        public IEnumerable<ProductionPlan> GetByProductionProcessId(int productionprocessid)
        {
            return _repository.GetByProductionProcessId(productionprocessid);
        }
        public IEnumerable<ProductionPlan> GetByOrderId(int orderid)
        {
            return _repository.GetByOrderId(orderid);
        }



        public void ImportDataTable(System.Data.DataTable datatable)
        {
            foreach (DataRow row in datatable.Rows)
            {

                ProductionPlan item = new ProductionPlan();
                foreach (DataColumn col in datatable.Columns)
                {
                    var sourcefieldname = col.ColumnName;
                    var mapping = _mappingservice.FindMapping("ProductionPlan", sourcefieldname);
                    if (mapping != null && row[sourcefieldname] != DBNull.Value)
                    {

                        Type productionplantype = item.GetType();
                        PropertyInfo propertyInfo = productionplantype.GetProperty(mapping.FieldName);
                        propertyInfo.SetValue(item, Convert.ChangeType(row[sourcefieldname], propertyInfo.PropertyType), null);
                        //productionplantype.GetProperty(mapping.FieldName).SetValue(item, row[sourcefieldname]);
                    }

                }

                this.Insert(item);


            }
        }


        public IEnumerable<ProductionTask> GenerateProductionTask(int[] plansid)
        {
            
            var planlist = this.Queryable().Include(x=>x.ProductionProcess).Where(x => plansid.Contains(x.Id)).ToList();
            var newitems = new List<ProductionTask>();
          
            foreach (var planitem in planlist)
            {
                var setps=_stepservice.Queryable().Include(x=>x.ProductionProcess).Where(x=>x.ProductionProcessId== planitem.ProductionProcessId && planitem.ProductionProcessId>0).ToList();
                
                foreach(var setp in setps){
                    var isexist = _taskservice.Queryable().Where(x => x.OrderId == planitem.OrderId && x.ProcessName == setp.ProductionProcess.Name && x.ProcessOrder == setp.Order).Any();
                    if (!isexist)
                    {
                        ProductionTask item = new ProductionTask();
                        item.OrderId = planitem.OrderId;
                        item.OrderKey = planitem.OrderKey;
                        item.OrderPlanDate = planitem.OrderPlanDate;
                        item.ParentBomComponentId = planitem.ParentBomComponentId;
                        item.BomComponentId = planitem.BomComponentId;
                        item.ComponentSKU = planitem.ComponentSKU;
                        item.ProcessSetp = setp.StepName;
                        item.ProductionLine = setp.ProductionProcess.ProductionLine;
                        item.ProcessOrder = setp.Order;
                        item.ProductionQty = planitem.RequirementQty;
                        item.SKUId = planitem.SKUId;
                        item.GraphSKU = planitem.GraphSKU;
                        item.ALTSku = planitem.ALTSku;
                        item.AltElapsedTime = planitem.RequirementQty * setp.ElapsedTime;
                        item.Equipment = setp.Equipment;
                        item.DesignName = planitem.DesignName;
                        item.OrderPlanDate = planitem.OrderPlanDate;
                        item.PlanStartDateTime = planitem.PlanedStartDate;
                        item.PlanCompletedDateTime = planitem.PlanedStartDate.Value.AddHours((double)item.AltElapsedTime);
                        item.ProcessName = setp.ProductionProcess.Name;
                        item.Owner = "";
                        newitems.Add(item);
                    }
                }
            }
            _taskservice.InsertRange(newitems);
            return newitems;
        }
    }
}



