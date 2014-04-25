using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WpLogger.DataContract.Model;

namespace WpLogger.Wp8TestApp.Services
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
            _appId = "1";
            _deviceId = "1";

            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/xml");
        }

        #endregion

        #region Public Methods and Operators

        public async Task<HttpResponseMessage> Post(Uri address, string data)
        {

            {
                using (var request = new HttpRequestMessage(HttpMethod.Post, address))
                {
                    request.Content = new StringContent(data, Encoding.UTF8, "application/x-www-form-urlencoded");
                    return await httpClient.SendAsync(request);
                }
            }
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

        #endregion
    }
}