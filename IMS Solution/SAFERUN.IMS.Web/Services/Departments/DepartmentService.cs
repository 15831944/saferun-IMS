
             
           
 

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
    public class DepartmentService : Service< Department >, IDepartmentService
    {

        private readonly IRepositoryAsync<Department> _repository;
        public  DepartmentService(IRepositoryAsync< Department> repository)
            : base(repository)
        {
            _repository=repository;
        }
        
                  
        
    }
}



