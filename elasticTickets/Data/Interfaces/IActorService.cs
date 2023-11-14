using elasticTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace elasticTickets.Data.Interfaces
{
    public interface IActorService
    {

        
        ////Task Get All Documents
        Task<List<Actor>> GetDocumentsActor(string indexName);
        
        //// Get by ID document
        Task<Actor> GetSingleDocumentActor(string indexName, string id);
        
        ////Task Insert Document
        Task AddActor(string indexName, Actor actor);
        
        ////Task Update Document
        Task<Actor> UpdateActor(string indexName, Actor newActor, string id);
        
        ////Task Delete Document
        Task DeleteActor(string indexName, string id);
        
    }
}
