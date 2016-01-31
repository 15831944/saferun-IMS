
             
           
 

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
    public class EmployeeService : Service< Employee >, IEmployeeService
    {

        private readonly IRepositoryAsync<Employee> _repository;
        public  EmployeeService(IRepositoryAsync< Employee> repository)
            : base(repository)
        {
            _repository=repository;
        }
        
                 public  IEnumerable<Employee> GetByManagerID(int  managerid)
         {
            return _repository.GetByManagerID(managerid);
         }
                   
        
    }
}



