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

namespace elasticTickets.Data
{
    public class ActorService : IActorService

    {
        private readonly IElasticClient _client;

        public ActorService(ElasticsearchService client)
        {
            _client = client.GetElasticClient();
        }

        
        public async Task<List<Actor>> GetDocumentsActor(string indexName)
            
        {
            var response = await _client.SearchAsync<Actor>(q => q
             .Index(indexName).Scroll("5m")
             );
            return response.Documents.ToList();


        }

        public async Task AddActor(string indexName, Actor actor)
        {

            var searchResponse = await _client.SearchAsync<Actor>(s => s.Index(indexName)
                                                                        .Sort(sort => sort.Descending("id"))
                                                                        .Size(1)
            );
            int Idnew=1;
            if (searchResponse.IsValid && searchResponse.Documents.Any())
            {
                var lastActor = searchResponse.Documents.First();
                Idnew= int.Parse(lastActor.id)+1;
                
            }
            actor.id = Idnew.ToString();

            var response = await _client.CreateAsync(actor, q => q.Index(indexName).Id(Idnew));

            if (!response.IsValid)
            {
                // Elasticsearch'e eklenirken bir hata oluştu
                throw new Exception($"Elasticsearch belge ekleme hatası: {response.ServerError?.Error}");
            }

        }

        public async Task<Actor> GetSingleDocumentActor(string indexName, string id)
        {
            var singleResponse = await _client.GetAsync<Actor>(id, s => s
                .Index(indexName));

            return singleResponse.Source;
        }

        public async Task<Actor> UpdateActor(string indexName, Actor newActor, string id)
        {

            await _client.UpdateAsync<Actor>(id, u => u.Index(indexName).Doc(newActor));

            return newActor;

        }

        public async Task DeleteActor(string indexName, string id)
        {
            var singleResponse = await _client.DeleteAsync<Actor>(id, s => s
                .Index(indexName));

        }
    }
}
