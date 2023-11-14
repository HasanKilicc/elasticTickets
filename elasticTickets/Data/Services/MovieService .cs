using elasticTickets.Models;
using elasticTickets.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elasticTickets.Data.Base;

namespace elasticTickets.Data.Services
{
    public class MovieService : IMovieService
    {
       
        private readonly IElasticClient _client;

        public MovieService(ElasticsearchService client)
        {
            _client = client.GetElasticClient();
        }

        

        public async Task<List<Movie>> GetDocumentsMovie(string indexName)
            
        {
            var response = await _client.SearchAsync<Movie>(q => q
             .Index(indexName).Scroll("5m")
             );
            return response.Documents.ToList();


        }

        public async Task AddMovie(string indexName, Movie movie)
        {

            var searchResponse = await _client.SearchAsync<Movie>(s => s.Index(indexName)
                                                                        .Sort(sort => sort.Descending("id")) // veya başka bir alan üzerinde sıralama yapabilirsiniz
                                                                        .Size(1) // Sadece en üstteki belgeyi al
            );
            int Idnew=1;
            if (searchResponse.IsValid && searchResponse.Documents.Any())
            {
                var lastMovie = searchResponse.Documents.First();
                Idnew= int.Parse(lastMovie.id)+1;
                
            }
            movie.id = Idnew.ToString();

            var response = await _client.CreateAsync(movie, q => q.Index(indexName).Id(Idnew));

            if (!response.IsValid)
            {
                // Elasticsearch'e eklenirken bir hata oluştu
                throw new Exception($"Elasticsearch belge ekleme hatası: {response.ServerError?.Error}");
            }

        }

        public async Task<Movie> GetSingleDocumentMovie(string indexName, string id)
        {
            var singleResponse = await _client.GetAsync<Movie>(id, s => s
                .Index(indexName));

            return singleResponse.Source;
        }

        public async Task<Movie> UpdateMovie(string indexName, Movie newMovie, string id)
        {

            await _client.UpdateAsync<Movie>(id, u => u.Index(indexName).Doc(newMovie));

            return newMovie;

        }

        public async Task DeleteMovie(string indexName, string id)
        {
            var singleResponse = await _client.DeleteAsync<Movie>(id, s => s
                .Index(indexName));

        }
    }
}
