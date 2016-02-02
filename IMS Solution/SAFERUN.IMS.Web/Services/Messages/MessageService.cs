
             
           
 

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
    public class MessageService : Service< Message >, IMessageService
    {

        private readonly IRepositoryAsync<Message> _repository;
        public  MessageService(IRepositoryAsync< Message> repository)
            : base(repository)
        {
            _repository=repository;
        }
        
                 public  IEnumerable<Message> GetByMessageId(int  messageid)
         {
            return _repository.GetByMessageId(messageid);
         }
                   
        
    }
}



