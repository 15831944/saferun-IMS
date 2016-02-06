
             
           
 

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
    public class BOMComponentService : Service< BOMComponent >, IBOMComponentService
    {

        private readonly IRepositoryAsync<BOMComponent> _repository;
        public  BOMComponentService(IRepositoryAsync< BOMComponent> repository)
            : base(repository)
        {
            _repository=repository;
        }
        
                 public  IEnumerable<BOMComponent> GetBySKUId(int  skuid)
         {
            return _repository.GetBySKUId(skuid);
         }
                  public  IEnumerable<BOMComponent> GetByParentComponentId(int  parentcomponentid)
         {
            return _repository.GetByParentComponentId(parentcomponentid);
         }
                          public IEnumerable<BOMComponent>   GetComponentsByParentComponentId (int parentcomponentid)
        {
            return _repository.GetComponentsByParentComponentId(parentcomponentid);
        }
         
        
    }
}



