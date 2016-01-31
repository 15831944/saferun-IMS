             
           
 

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
    public class BaseCodeService : Service< BaseCode >, IBaseCodeService
    {

        private readonly IRepositoryAsync<BaseCode> _repository;
        public  BaseCodeService(IRepositoryAsync< BaseCode> repository)
            : base(repository)
        {
            _repository=repository;
        }
        
        public IEnumerable<CodeItem>   GetCodeItemsByBaseCodeId (int basecodeid)
        {
            return _repository.GetCodeItemsByBaseCodeId(basecodeid);
        }




       public IEnumerable<CodeItem> GetCodeItemsByCodeType(string codeType)
                         {
                             return _repository.GetCodeItemsByCodeType(codeType);
                         }
    }
}



