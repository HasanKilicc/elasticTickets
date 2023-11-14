using elasticTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace elasticTickets.Data.Interfaces
{
    public interface IMovieService
    {
        
        ////Task Get All Documents
        Task<List<Movie>> GetDocumentsMovie(string indexName);
        
        //// Get by ID document
        Task<Movie> GetSingleDocumentMovie(string indexName, string id);

        ////Task Insert Document
        Task AddMovie(string indexName, Movie movie);

        ////Task Update Document
        Task<Movie> UpdateMovie(string indexName, Movie movie, string id);

        ////Task Delete Document
        Task DeleteMovie(string indexName, string id);

    }
}
