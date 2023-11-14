using elasticTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace elasticTickets.Data.Base
{
    public class ElasticsearchService : IElasticsearchService
    {
        private readonly IConfiguration _configuration;
        public readonly IElasticClient _client;

        public ElasticsearchService(IConfiguration configuration)
        {
            _configuration = configuration;
            
            string host = _configuration.GetSection("ElasticsearchServer:Host").Value;
            string username = _configuration.GetSection("ElasticsearchServer:Username").Value;
            string password = _configuration.GetSection("ElasticsearchServer:Password").Value;

            var settings = new ConnectionSettings(host, new
                Elasticsearch.Net.BasicAuthenticationCredentials(username, password));
           
            _client = new ElasticClient(settings);
        }

        public IElasticClient GetElasticClient()
        {
            return _client;
        }
        public async Task ChekIndex(string indexName)
        {
            var anyy = await _client.Indices.ExistsAsync(indexName);
            if (anyy.Exists)
                return;

            else if (indexName == "actors-index")
            {
                var response = await _client.Indices.CreateAsync(indexName,
                ci => ci
                    .Index(indexName)
                    .ActorMapping()
                    .Settings(s => s.NumberOfShards(3).NumberOfReplicas(1))
                    );

                return;
            }
            else if (indexName == "movies-index")
            {
                var response = await _client.Indices.CreateAsync(indexName,
                ci => ci
                    .Index(indexName)
                    .MoviesMapping()
                    .Settings(s => s.NumberOfShards(3).NumberOfReplicas(1))
                    );

                return;
            }
            else if (indexName == "producers-index")
            {
                var response = await _client.Indices.CreateAsync(indexName,
                ci => ci
                    .Index(indexName)
                    .ProducerMapping()
                    .Settings(s => s.NumberOfShards(3).NumberOfReplicas(1))
                    );

                return;
            }
            else if (indexName == "cinemas-index")
            {
                var response = await _client.Indices.CreateAsync(indexName,
                ci => ci
                    .Index(indexName)
                    .CinemaMapping()
                    .Settings(s => s.NumberOfShards(3).NumberOfReplicas(1))
                    );

                return;
            }


        }
               
    }
}
