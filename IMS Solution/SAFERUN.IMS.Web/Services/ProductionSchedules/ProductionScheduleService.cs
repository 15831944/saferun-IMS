
             
           
 

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
    public class ProductionScheduleService : Service< ProductionSchedule >, IProductionScheduleService
    {

        private readonly IRepositoryAsync<ProductionSchedule> _repository;
		 private readonly IDataTableImportMappingService _mappingservice;
         private readonly IWorkDetailService _workdetailservice;
         private readonly IScheduleDetailService _scheduledetailservice;
        public  ProductionScheduleService(IScheduleDetailService scheduledetailservice,IWorkDetailService workdetailservice,IRepositoryAsync< ProductionSchedule> repository,IDataTableImportMappingService mappingservice)
            : base(repository)
        {
            _repository=repository;
			_mappingservice = mappingservice;
            _workdetailservice = workdetailservice;
            _scheduledetailservice = scheduledetailservice;
        }
        
                 public  IEnumerable<ProductionSchedule> GetByWorkId(int  workid)
         {
            return _repository.GetByWorkId(workid);
         }
                  public  IEnumerable<ProductionSchedule> GetByCustomerId(int  customerid)
         {
            return _repository.GetByCustomerId(customerid);
         }
                          public IEnumerable<ScheduleDetail>   GetScheduleDetailsByProductionScheduleId (int productionscheduleid)
        {
            return _repository.GetScheduleDetailsByProductionScheduleId(productionscheduleid);
        }
         
        

		public void ImportDataTable(System.Data.DataTable datatable)
        {
            foreach (DataRow row in datatable.Rows)
            {
                 
                ProductionSchedule item = new ProductionSchedule();
                foreach (DataColumn col in datatable.Columns)
                {
                    var sourcefieldname = col.ColumnName;
                    var mapping = _mappingservice.FindMapping("ProductionSchedule", sourcefieldname);
                    if (mapping != null && row[sourcefieldname] != DBNull.Value)
                    {
                        
                        Type productionscheduletype = item.GetType();
						PropertyInfo propertyInfo = productionscheduletype.GetProperty(mapping.FieldName);
						propertyInfo.SetValue(item, Convert.ChangeType(row[sourcefieldname], propertyInfo.PropertyType), null);
                        //productionscheduletype.GetProperty(mapping.FieldName).SetValue(item, row[sourcefieldname]);
                    }

                }
                
                this.Insert(item);
               

            }
        }


        public void GenerateSchedulekDetails(ScheduleGeneratorViewModel viewmodel)
        {
            var productionschedule = this.Queryable().Where(x => x.Id == viewmodel.ProductionScheduleId).First();
            var workdetails = this._workdetailservice.Queryable().Where(x => viewmodel.WorkDetails.Contains(x.Id)).ToList();
            var list = new List<ScheduleDetail>();
            foreach (var workdetail in workdetails) {
                ScheduleDetail item = new ScheduleDetail();
                item.ActualConsumeTime = 0;
                item.ActualProductionQty = 0;
                item.AltConsumeTime = 0;
                item.AltProdctionDate1 = workdetail.AltProdctionDate1;
                item.ActualProdctionDate1 = workdetail.ActualProdctionDate1;
                item.AltProdctionDate2 = workdetail.AltProdctionDate1;
                item.BomComponentId = workdetail.BomComponentId;
                item.ParentBomComponentId = workdetail.ParentBomComponentId;
                item.ComponentSKUId = workdetail.ComponentSKUId;
                item.ConsumeQty = workdetail.ConsumeQty;
                item.GraphSKU = workdetail.GraphSKU;
                item.GraphVer = workdetail.GraphVer;
                item.OrderKey = workdetail.OrderKey;
                item.ParentSKUId = workdetail.ParentSKUId;
                item.ProductionScheduleId = productionschedule.Id;
                item.Remark1 = workdetail.Remark1;
                item.Remark2 = workdetail.Remark2;
                item.RequirementQty = workdetail.RequirementQty;
                item.ScheduleNo = productionschedule.ScheduleNo;
                item.ScheduleProductionQty = workdetail.RequirementQty;
                item.Status = 0;
                item.WorkDetailId = workdetail.Id;
                item.WorkNo = workdetail.WorkNo;
                list.Add(item);
            }
            _scheduledetailservice.InsertRange(list);
        }
    }
}



