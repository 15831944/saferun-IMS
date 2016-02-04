
             
           
 

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
    public class ProjectNodeService : Service< ProjectNode >, IProjectNodeService
    {

        private readonly IRepositoryAsync<ProjectNode> _repository;
        public  ProjectNodeService(IRepositoryAsync< ProjectNode> repository)
            : base(repository)
        {
            _repository=repository;
        }
        
                 public  IEnumerable<ProjectNode> GetByDepartmentId(int  departmentid)
         {
            return _repository.GetByDepartmentId(departmentid);
         }
                  public  IEnumerable<ProjectNode> GetByProjectTypeId(int  projecttypeid)
         {
            return _repository.GetByProjectTypeId(projecttypeid);
         }
                   
        
    }
}



