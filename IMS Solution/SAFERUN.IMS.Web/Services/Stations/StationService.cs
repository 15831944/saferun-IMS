
             
           
 

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
    public class StationService : Service< Station >, IStationService
    {

        private readonly IRepositoryAsync<Station> _repository;
        public  StationService(IRepositoryAsync< Station> repository)
            : base(repository)
        {
            _repository=repository;
        }
        
                  
        
    }
}



