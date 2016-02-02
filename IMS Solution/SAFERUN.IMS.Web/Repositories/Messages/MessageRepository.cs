
                    
      
    
 
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Repository.Pattern.Repositories;

using SAFERUN.IMS.Web.Models;

namespace SAFERUN.IMS.Web.Repositories
{
  public static class MessageRepository  
    {
 
                 public static IEnumerable<Message> GetByMessageId(this IRepositoryAsync<Message> repository, int messageid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.MessageId==messageid);
             return query;

         }
             
        
         
	}
}



