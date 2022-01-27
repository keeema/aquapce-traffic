using System;
using System.Net;
using Microsoft.Extensions.Logging;

namespace keeema.aquapcetraffic
{
    class HtmlDownloader
    {
        public static string Download(ILogger log)
        {
            var url = "https://www.aquapce.cz/oteviraci-doba/";
            log.LogInformation($"Downloading {url}");
            using (WebClient client = new WebClient())
            {
                return client.DownloadString(url);
            }
        }
    }
}