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
    public class ProducerService : IProducerService
    {
        private readonly IElasticClient _client;

        public ProducerService(ElasticsearchService client)
        {
            _client = client.GetElasticClient();
        }

        

        public async Task<List<Producer>> GetDocumentsProducer(string indexName)
            
        {
            var response = await _client.SearchAsync<Producer>(q => q
             .Index(indexName).Scroll("5m")
             );
            return response.Documents.ToList();


        }

        

        public async Task AddProducer(string indexName, Producer producer)
        {

            var searchResponse = await _client.SearchAsync<Producer>(s => s.Index(indexName)
                                                                        .Sort(sort => sort.Descending("id")) // veya başka bir alan üzerinde sıralama yapabilirsiniz
                                                                        .Size(1) // Sadece en üstteki belgeyi al
            );
            int Idnew=1;
            if (searchResponse.IsValid && searchResponse.Documents.Any())
            {
                var lastProducer = searchResponse.Documents.First();
                Idnew= int.Parse(lastProducer.id)+1;
                
            }
            producer.id = Idnew.ToString();

            var response = await _client.CreateAsync(producer, q => q.Index(indexName).Id(Idnew));

            if (!response.IsValid)
            {
                // Elasticsearch'e eklenirken bir hata oluştu
                throw new Exception($"Elasticsearch belge ekleme hatası: {response.ServerError?.Error}");
            }

        }

        public async Task<Producer> GetSingleDocumentProducer(string indexName, string id)
        {
            var singleResponse = await _client.GetAsync<Producer>(id, s => s
                .Index(indexName));

            return singleResponse.Source;
        }

        public async Task<Producer> UpdateProducer(string indexName, Producer newProducer, string id)
        {

            await _client.UpdateAsync<Producer>(id, u => u.Index(indexName).Doc(newProducer));

            return newProducer;

        }

        public async Task DeleteProducer(string indexName, string id)
        {
            var singleResponse = await _client.DeleteAsync<Producer>(id, s => s
                .Index(indexName));

        }
    }
}
