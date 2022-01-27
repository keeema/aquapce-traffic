using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;

using Microsoft.Extensions.Logging;

namespace keeema.aquapcetraffic
{
    public class CollectTraffic
    {

        [FunctionName("CollectTraffic")]
        public async Task Run(
            [TimerTrigger("0 */15 * * * *"
#if DEBUG
            , RunOnStartup=true
#endif
        )]TimerInfo myTimer,
        [CosmosDB(databaseName: "dbaquapcetraffic", collectionName: "dbaquapcetraffic-container", ConnectionStringSetting = "CosmosDbConnectionString")] IAsyncCollector<dynamic> documentsOut, ILogger log)
        {
            log.LogInformation($"C# CollectTraffic function executed at: {DateTime.Now}");

            var content = HtmlDownloader.Download(log);
            var trafficItems = TrafficParser.Parse(content, log);
            foreach (var trafficItem in trafficItems)
            {
                await documentsOut.AddAsync(new
                {
                    place = trafficItem.Place,
                    count = trafficItem.Count,
                    timestamp = trafficItem.TimeStamp
                });
            }

            var resultMessage = $"C# CollectTraffic function execution finished at: {DateTime.Now}";
            log.LogInformation(resultMessage);
        }
    }
}
