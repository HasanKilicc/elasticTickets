using elasticTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace elasticTickets.Data.Base
{
    public interface IElasticsearchService
    {
        
        Task ChekIndex(string indexName);
          
          

    }
}
