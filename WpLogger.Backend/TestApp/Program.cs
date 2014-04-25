using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BackendWebSite.Models;
using Newtonsoft.Json;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            RunAsync();
            Console.ReadLine();
        }

        private static async void RunAsync()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            var logEntry = new LogEntry();
            logEntry.Content = "Test content";
            logEntry.LogLevel = "DEBUG";
            logEntry.Tag = "UPNEXT";
            logEntry.TimeStamp = DateTime.Now;

            var content = JsonConvert.SerializeObject(logEntry);

            //var url = string.Format("http://upnext-hackathon.azurewebsites.net/api/device/{0}/app/{1}/log", "Device1", "App1");
            var url = string.Format("http://localhost:2385/api/device/{0}/app/{1}/log", "Device1", "App1");

            using (var request = new HttpRequestMessage(HttpMethod.Post, url))
            {
                request.Content = new StringContent(content, Encoding.UTF8, "application/json");
                var response = await httpClient.SendAsync(request);
                Console.WriteLine(response.StatusCode);
            }
        }
    }
}
