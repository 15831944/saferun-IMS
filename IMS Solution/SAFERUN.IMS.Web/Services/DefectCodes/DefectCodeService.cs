
             
           
 

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
    public class DefectCodeService : Service< DefectCode >, IDefectCodeService
    {

        private readonly IRepositoryAsync<DefectCode> _repository;
        public  DefectCodeService(IRepositoryAsync< DefectCode> repository)
            : base(repository)
        {
            _repository=repository;
        }
        
                 public  IEnumerable<DefectCode> GetByDefectTypeId(int  defecttypeid)
         {
            return _repository.GetByDefectTypeId(defecttypeid);
         }
                   
        
    }
}



