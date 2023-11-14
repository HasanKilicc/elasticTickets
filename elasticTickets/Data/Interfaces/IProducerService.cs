using elasticTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace elasticTickets.Data.Interfaces
{
    public interface IProducerService
    {
        
        ////Task Get All Documents
        Task<List<Producer>> GetDocumentsProducer(string indexName);

        //// Get by ID document
        Task<Producer> GetSingleDocumentProducer(string indexName, string id);
        
        ////Task Insert Document
        Task AddProducer(string indexName, Producer producer);

        ////Task Update Document
        Task<Producer> UpdateProducer(string indexName, Producer producer, string id);
        
        ////Task Delete Document
        Task DeleteProducer(string indexName, string id);
        
    }
}
