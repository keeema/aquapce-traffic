using System;
using System.Linq;
using System.Collections.Generic;

using HtmlAgilityPack;
using Microsoft.Extensions.Logging;

namespace keeema.aquapcetraffic
{
    class TrafficParser
    {
        public static IEnumerable<TrafficItem> Parse(string htmlContent, ILogger log)
        {
            log.LogInformation($"Parsing");
            var result = new List<TrafficItem>();
            HtmlDocument htmlSnippet = new HtmlDocument();
            htmlSnippet.LoadHtml(htmlContent);
            var nodes = htmlSnippet.DocumentNode.SelectNodes("//div[@class='fast-info']//li");
            log.LogInformation($"Found {nodes.Count()} records:");

            var timestamp = DateTime.UtcNow;
            foreach (var node in nodes)
            {
                var record = node.InnerText.ToString();
                log.LogInformation(record);
                var parts = record.Trim().Split(":");
                result.Add(new TrafficItem
                {
                    Place = parts[0],
                    Count = int.Parse(parts[1]),
                    TimeStamp = timestamp
                });
            }

            log.LogInformation($"Parsed traffic:");

            foreach (var traffic in result)
            {
                log.LogInformation(traffic.ToString());
            }

            return result;
        }
    }
}