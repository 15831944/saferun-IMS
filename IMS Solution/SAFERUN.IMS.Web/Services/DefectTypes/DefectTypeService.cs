
             
           
 

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Repository.Pattern.Repositories;
using Service.Pattern;

using SAFERUN.IMS.Web.Models;
using SAFERUN.IMS.Web.Repositories;
namespace SAFERUN.IMS.Web.Services
{
    public class DefectTypeService : Service< DefectType >, IDefectTypeService
    {

        private readonly IRepositoryAsync<DefectType> _repository;
        public  DefectTypeService(IRepositoryAsync< DefectType> repository)
            : base(repository)
        {
            _repository=repository;
        }
        
                         public IEnumerable<DefectCode>   GetDefectCodesByDefectTypeId (int defecttypeid)
        {
            return _repository.GetDefectCodesByDefectTypeId(defecttypeid);
        }
         
        
    }
}



