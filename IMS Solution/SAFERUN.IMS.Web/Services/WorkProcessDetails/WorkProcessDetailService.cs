




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
    public class WorkProcessDetailService : Service<WorkProcessDetail>, IWorkProcessDetailService
    {

        private readonly IRepositoryAsync<WorkProcessDetail> _repository;
        private readonly IDataTableImportMappingService _mappingservice;
      
        public WorkProcessDetailService(IRepositoryAsync<WorkProcessDetail> repository, IDataTableImportMappingService mappingservice)
            : base(repository)
        {
            _repository = repository;
            _mappingservice = mappingservice;
        
        }

        public IEnumerable<WorkProcessDetail> GetByWorkProcessId(int workprocessid)
        {
            return _repository.GetByWorkProcessId(workprocessid);
        }
        public IEnumerable<WorkProcessDetail> GetBySKUId(int skuid)
        {
            return _repository.GetBySKUId(skuid);
        }
        public IEnumerable<WorkProcessDetail> GetByProcessStepId(int processstepid)
        {
            return _repository.GetByProcessStepId(processstepid);
        }
        public IEnumerable<WorkProcessDetail> GetByStationId(int stationid)
        {
            return _repository.GetByStationId(stationid);
        }



        public void ImportDataTable(System.Data.DataTable datatable)
        {
            foreach (DataRow row in datatable.Rows)
            {

                WorkProcessDetail item = new WorkProcessDetail();
                foreach (DataColumn col in datatable.Columns)
                {
                    var sourcefieldname = col.ColumnName;
                    var mapping = _mappingservice.FindMapping("WorkProcessDetail", sourcefieldname);
                    if (mapping != null && row[sourcefieldname] != DBNull.Value)
                    {

                        Type workprocessdetailtype = item.GetType();
                        PropertyInfo propertyInfo = workprocessdetailtype.GetProperty(mapping.FieldName);
                        propertyInfo.SetValue(item, Convert.ChangeType(row[sourcefieldname], propertyInfo.PropertyType), null);
                        //workprocessdetailtype.GetProperty(mapping.FieldName).SetValue(item, row[sourcefieldname]);
                    }

                }

                this.Insert(item);


            }
        }


        public void Start(int id)
        {
            var detail = this.Queryable().Where(x => x.Id == id).First();
            detail.StartingDateTime = DateTime.Now;
            detail.Status = 1;
            detail.Operator = System.Threading.Thread.CurrentPrincipal.Identity.Name;
            this.Update(detail);

        }

        public void Completed(int id)
        {
            var detail = this.Queryable().Where(x => x.Id == id).First();
            detail.CompletedDateTime = DateTime.Now;
            var hours = detail.CompletedDateTime.Value - detail.StartingDateTime.Value;
            detail.ElapsedTime = hours.Hours;
            detail.Status = 2;
            detail.Operator = System.Threading.Thread.CurrentPrincipal.Identity.Name;

            this.Update(detail);
        }

        public override void Update(WorkProcessDetail entity)
        {
            if (entity.StartingDateTime.HasValue && entity.CompletedDateTime.HasValue)
            {
                entity.Status = 2;
            }
            if (entity.StartingDateTime.HasValue && !entity.CompletedDateTime.HasValue)
            {
                entity.Status = 1;
            }
            if (!entity.StartingDateTime.HasValue && !entity.CompletedDateTime.HasValue)
            {
                entity.Status = 0;
            }
            base.Update(entity);

            //if (!this.Queryable().Where(x => x.Status != 2 && x.WorkProcessId == entity.WorkProcessId).Any())
            //{
            //    var workprocess = this._repository.GetRepository<WorkProcess>();
            //    var workitem = workprocess.Find(entity.WorkProcessId);
            //    workitem.Status = 3;
            //    workprocess.Update(workitem);
            //    //this._workprocessService.CompletedWork(entity.WorkProcessId);
            //}
        }
        public override void Insert(SAFERUN.IMS.Web.Models.WorkProcessDetail entity)
        {
            if (entity.StartingDateTime.HasValue && entity.CompletedDateTime.HasValue)
            {
                entity.Status = 2;
            }
            if (entity.StartingDateTime.HasValue && !entity.CompletedDateTime.HasValue)
            {
                entity.Status = 1;
            }
            if (!entity.StartingDateTime.HasValue && !entity.CompletedDateTime.HasValue)
            {
                entity.Status = 0;
            }
            base.Insert(entity);
            //if (!this.Queryable().Where(x => x.Status != 2 && x.WorkProcessId == entity.WorkProcessId).Any())
            //{
            //    var workprocess = this._repository.GetRepository<WorkProcess>();
            //    var workitem = workprocess.Find(entity.WorkProcessId);
            //    workitem.Status = 3;
            //    workprocess.Update(workitem);
            //}
        }
    }
}



