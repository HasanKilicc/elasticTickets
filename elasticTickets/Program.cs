using Elastic.Apm.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using Serilog.AspNetCore;
using Serilog.Sinks.Elasticsearch;
using Elasticsearch.Net;
using NLog;
using NLog.Web;
using Elastic.Apm.NLog;

namespace elasticTickets
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("init main");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception exception)
            {
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            //.UseSerilog((hostingContext, loggerConfiguration) =>
            //{
            //    loggerConfiguration
            //     .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("https://c40ff5805a704f969705d802e1c1a698.us-central1.gcp.cloud.es.io/"))
            //      {
            //        IndexFormat = "elastic-ticket-logs-{0:yyyy.MM.dd}",
            //        AutoRegisterTemplate = true,
            //        NumberOfShards = 2,
            //        NumberOfReplicas = 1,
            //        ModifyConnectionSettings = connectionConfiguration => connectionConfiguration.BasicAuthentication("elastic", "6PHXWbFFai2RFex6e5aDfTWI")
            //     })
            //    .Enrich.FromLogContext()
            //    .WriteTo.Console();
            //})
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            })
                .UseNLog();

    }
}
