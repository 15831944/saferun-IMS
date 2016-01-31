
     
 
 
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
    public interface IMenuItemService : IService<MenuItem>
    {

        IEnumerable<MenuItem> GetByParentId(int parentid);

        IEnumerable<MenuItem> GetSubMenusByParentId(int parentid);


        IEnumerable<MenuItem> AllMenus();





    }
}