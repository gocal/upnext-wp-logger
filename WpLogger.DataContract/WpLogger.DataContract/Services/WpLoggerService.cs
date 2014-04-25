using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WpLogger.DataContract.Model;

namespace WpLogger.DataContract.Services
{
    public class WpLoggerService
    {
        #region Fields

        private readonly string _apiUrl;
        private readonly HttpClient httpClient;
        private readonly string _appId;
        private readonly string _deviceId;

        #endregion

        #region Constructors and Destructors

        public WpLoggerService()
        {
            _apiUrl = "http://upnext-hackathon.azurewebsites.net/api/";
            _appId = "app_id_1";
            _deviceId = "device_id_1";

            httpClient = new HttpClient();
        }

        #endregion

        #region Public Methods and Operators

        private async Task<HttpResponseMessage> Post(Uri address, string data)
        {

            {
                using (var request = new HttpRequestMessage(HttpMethod.Post, address))
                {
                    request.Content = new StringContent(data, Encoding.UTF8, "application/x-www-form-urlencoded");
                    return await httpClient.SendAsync(request);
                }
            }
        }

        public async Task<HttpResponseMessage> Get(Uri address)
        {
            return await httpClient.GetAsync(address);
        }

        public async void SendLog(string tag, string content)
        {
            var logEntry = new LogEntry()
            {
                Content = content,
                Tag = tag,
                TimeStamp = DateTime.Now
            };

            var url = new Uri(_apiUrl + _deviceId + "/" + _appId);
            var data = JsonConvert.SerializeObject(logEntry);
            var response = await Post(url, data);
        }

        public async Task<List<LogEntry>> GetLogs(DateTime from, DateTime to)
        {

            var url = new Uri(_apiUrl +"device/" + _deviceId + "/app/" + _appId+"/log");
     
            var response = await Get(url);

            var reader = new StreamReader(await response.Content.ReadAsStreamAsync());
            var stringContent = reader.ReadToEnd();


            var list = JsonConvert.DeserializeObject<List<LogEntry>>(stringContent, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return list;
        }

        #endregion
    }
}