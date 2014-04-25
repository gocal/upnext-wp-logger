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

        public readonly string _apiUrl;
        private readonly HttpClient httpClient;
        private string _appId;
        private string _deviceId;

        #endregion

        #region Constructors and Destructors

        public WpLoggerService()
        {
            _apiUrl = "http://upnext-hackathon.azurewebsites.net/api/";
            _appId = "app_id_1";
            _deviceId = "device_id_1";

            httpClient = new HttpClient();
        }

        public void setApp(string appId, string deviceId)
        {
           // _appId = appId;
           // _deviceId = deviceId;
        }

        #endregion

        #region Public Methods and Operators

        private async Task<HttpResponseMessage> Post(Uri address, string data)
        {

            {
                using (var request = new HttpRequestMessage(HttpMethod.Post, address))
                {
                    request.Content = new StringContent(data, Encoding.UTF8, "application/json");
                    return await httpClient.SendAsync(request);
                }
            }
        }

        public async Task<HttpResponseMessage> Get(Uri address)
        {
            return await httpClient.GetAsync(address);
        }

        public async void SendLog(string level, string tag, string content)
        {
            var logEntry = new LogEntry()
            {
                LogLevel = level,
                Content = content,
                Tag = tag,
                TimeStamp = DateTime.Now
            };

            var url = new Uri(_apiUrl + "device/" + _deviceId + "/app/" + _appId + "/log");
            var data = JsonConvert.SerializeObject(logEntry);
            var response = await Post(url, data);
        }

        public async Task<List<LogEntry>> GetLogs(DateTime from, DateTime to)
        {

            var url = new Uri(_apiUrl + "device/" + _deviceId + "/app/" + _appId + "/log?from=" + from.ToUniversalTime().ToString("O") + "&to=" + to.ToUniversalTime().ToString("O"));
     
            

            var response = await Get(url);

            var reader = new StreamReader(await response.Content.ReadAsStreamAsync());
            var stringContent = reader.ReadToEnd();


            var list = JsonConvert.DeserializeObject<List<LogEntry>>(stringContent, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return list;
        }


        public async Task<List<String>> GetApps(string deviceId)
        {
            var url = new Uri(_apiUrl + "device/" + deviceId + "/app/");

            var response = await Get(url);

            var reader = new StreamReader(await response.Content.ReadAsStreamAsync());
            var stringContent = reader.ReadToEnd();

            var list = JsonConvert.DeserializeObject<List<String>>(stringContent, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return list;
        }

        public async Task<List<String>> GetDevices()
        {

            var url = new Uri(_apiUrl + "device/");

            var response = await Get(url);

            var reader = new StreamReader(await response.Content.ReadAsStreamAsync());
            var stringContent = reader.ReadToEnd();


            var list = JsonConvert.DeserializeObject<List<String>>(stringContent, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return list;
        }

        #endregion
    }
}