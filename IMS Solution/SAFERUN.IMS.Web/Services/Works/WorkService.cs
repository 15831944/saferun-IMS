




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
    public class WorkService : Service<Work>, IWorkService
    {

        private readonly IRepositoryAsync<Work> _repository;
        private readonly IDataTableImportMappingService _mappingservice;
        private readonly IBOMComponentService _bomservice;
        private readonly IOrderDetailService _orderdetailservice;
        private readonly IWorkDetailService _workdetailservice;
        public WorkService(IWorkDetailService workdetailservice,
            IOrderDetailService orderdetailservice, 
            IBOMComponentService bomservice, 
            IRepositoryAsync<Work> repository, 
            IDataTableImportMappingService mappingservice)
            : base(repository)
        {
            _repository = repository;
            _mappingservice = mappingservice;
            _bomservice = bomservice;
            _orderdetailservice = orderdetailservice;
            _workdetailservice = workdetailservice;
        }

        public IEnumerable<Work> GetByWorkTypeId(int worktypeid)
        {
            return _repository.GetByWorkTypeId(worktypeid);
        }
        public IEnumerable<Work> GetByOrderId(int orderid)
        {
            return _repository.GetByOrderId(orderid);
        }
        public IEnumerable<WorkDetail> GetWorkDetailsByWorkId(int workid)
        {
            return _repository.GetWorkDetailsByWorkId(workid);
        }



        public void ImportDataTable(System.Data.DataTable datatable)
        {
            foreach (DataRow row in datatable.Rows)
            {

                Work item = new Work();
                foreach (DataColumn col in datatable.Columns)
                {
                    var sourcefieldname = col.ColumnName;
                    var mapping = _mappingservice.FindMapping("Work", sourcefieldname);
                    if (mapping != null && row[sourcefieldname] != DBNull.Value)
                    {

                        Type worktype = item.GetType();
                        PropertyInfo propertyInfo = worktype.GetProperty(mapping.FieldName);
                        propertyInfo.SetValue(item, Convert.ChangeType(row[sourcefieldname], propertyInfo.PropertyType), null);
                        //worktype.GetProperty(mapping.FieldName).SetValue(item, row[sourcefieldname]);
                    }

                }

                this.Insert(item);


            }
        }


        public void GenerateWorkDetail(WorkGeneratorViewModel viewmodel)
        {
            var orderdetail = this._orderdetailservice.Queryable().Include(x=>x.Order).Where(x => x.Id == viewmodel.OrderDetailId).First();
            var work = this.Queryable().Where(x => x.Id == viewmodel.WorkId).First();
            var bomcomponents = this._bomservice.Queryable().Include(x=>x.ParentComponent).Include(x => x.SKU).Where(x => viewmodel.WorkingBomComponents.Contains(x.Id)).ToList();
            var list = new List<WorkDetail>();
            foreach (var bom in bomcomponents) {
                WorkDetail item = new WorkDetail();
                item.WorkId = work.Id;
                item.WorkNo = work.WorkNo;
                item.StockQty = 0;
                item.AltOrderProdctionDate = orderdetail.Order.OrderDate;
                item.AltProdctionDate1 = orderdetail.Order.OrderDate;
                item.AltProdctionDate2 = orderdetail.Order.OrderDate;
                item.AltProdctionDate3 = orderdetail.Order.OrderDate;
                item.AltProdctionDate4 = orderdetail.Order.OrderDate;
                item.AltProdctionDate5 = orderdetail.Order.OrderDate;
                item.BomComponentId = bom.Id;
                item.ParentBomComponentId = bom.ParentComponentId;
                item.ComponentSKUId = bom.SKUId;
                item.ConsumeQty = bom.ConsumeQty;
                item.GraphSKU = bom.GraphSKU;
                item.GraphVer = "";
                item.OrderKey = orderdetail.OrderKey;
                item.ParentSKUId = bom.ParentComponent.SKUId;
                item.Remark1 = bom.Remark1;
                item.Remark2 = bom.Remark2;
                item.RequirementQty = orderdetail.Qty * bom.ConsumeQty;
                item.StockQty = 0;

                var exists = _workdetailservice.Queryable().Where(x => x.WorkId == work.Id && x.ComponentSKUId == bom.SKUId && x.ParentSKUId == bom.ParentComponent.SKUId).Any();
                if (!exists)
                {
                    list.Add(item);
                }
            }
            this._workdetailservice.InsertRange(list);
        }
    }
}



