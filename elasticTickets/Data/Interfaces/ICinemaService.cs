using elasticTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace elasticTickets.Data.Interfaces
{
    public interface ICinemaService
    {

        
        ////Task Get All Documents
        Task<List<Cinema>> GetDocumentsCinema(string indexName);
        
        //// Get by ID document
        Task<Cinema> GetSingleDocumentCinema(string indexName, string id);
        
        ////Task Insert Document
        Task AddCinema(string indexName, Cinema cinema);
        
        ////Task Update Document
        Task<Cinema> UpdateCinema(string indexName, Cinema cinema, string id);
        
        ////Task Delete Document
        Task DeleteCinema(string indexName, string id);
        
    }
}
