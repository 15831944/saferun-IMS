
             
           
 

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
    public class ProcessNodeService : Service< ProcessNode >, IProcessNodeService
    {

        private readonly IRepositoryAsync<ProcessNode> _repository;
        public  ProcessNodeService(IRepositoryAsync< ProcessNode> repository)
            : base(repository)
        {
            _repository=repository;
        }
        
                  
        
    }
}



