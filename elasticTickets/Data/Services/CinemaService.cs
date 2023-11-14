using elasticTickets.Data.Base;
using elasticTickets.Data.Interfaces;
using elasticTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace elasticTickets.Data.Services

{
    public class CinemaService : ICinemaService
    {
        
        private readonly IElasticClient _client;

        public CinemaService(ElasticsearchService client)
        {
            _client = client.GetElasticClient();
        }

        

        public async Task<List<Cinema>> GetDocumentsCinema(string indexName)
            
        {
            var response = await _client.SearchAsync<Cinema>(q => q
             .Index(indexName).Scroll("5m")
             );
            return response.Documents.ToList();


        }


        public async Task AddCinema(string indexName, Cinema cinema)
        {

            var searchResponse = await _client.SearchAsync<Cinema>(s => s.Index(indexName)
                                                                        .Sort(sort => sort.Descending("id")) 
                                                                        .Size(1)
            );
            int Idnew=1;
            if (searchResponse.IsValid && searchResponse.Documents.Any())
            {
                var lastCinema = searchResponse.Documents.First();
                Idnew= int.Parse(lastCinema.id)+1;
                
            }
            cinema.id = Idnew.ToString();

            var response = await _client.CreateAsync(cinema, q => q.Index(indexName).Id(Idnew));

            if (!response.IsValid)
            {
                // Elasticsearch'e eklenirken bir hata oluştu
                throw new Exception($"Elasticsearch belge ekleme hatası: {response.ServerError?.Error}");
            }

        }

        public async Task<Cinema> GetSingleDocumentCinema(string indexName, string id)
        {
            var singleResponse = await _client.GetAsync<Cinema>(id, s => s
                .Index(indexName));

            return singleResponse.Source;
        }

        public async Task<Cinema> UpdateCinema(string indexName, Cinema newCinema, string id)
        {

            await _client.UpdateAsync<Cinema>(id, u => u.Index(indexName).Doc(newCinema));

            return newCinema;

        }

        public async Task DeleteCinema(string indexName, string id)
        {
            var singleResponse = await _client.DeleteAsync<Cinema>(id, s => s
                .Index(indexName));

        }
    }
}
