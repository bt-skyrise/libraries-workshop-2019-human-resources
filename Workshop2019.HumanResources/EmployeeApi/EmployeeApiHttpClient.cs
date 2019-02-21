using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace Workshop2019.HumanResources.EmployeeApi
{
    public class EmployeeApiHttpClient
    {
        private readonly Uri _apiUrl;
        private readonly string _apiKey;

        public EmployeeApiHttpClient(Uri apiUrl, string apiKey)
        {
            _apiUrl = apiUrl;
            _apiKey = apiKey;
        }

        public async Task<TResponse> Get<TResponse>(string path)
        {
            return await Get<TResponse>(path, new Dictionary<string, string>());
        }

        public async Task<TResponse> Get<TResponse>(string path, Dictionary<string, string> queryParameters)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.SendAsync(new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = FormatRequestUri(path, queryParameters),
                    Headers =
                    {
                        { "ApiKey", _apiKey },
                        { "Accept", "application/json" }
                    }
                });

                var stringResponse = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<TResponse>(stringResponse);
            }
        }

        private Uri FormatRequestUri(string path, Dictionary<string, string> queryParameters)
        {
            var apiUriWithPath = new Uri(_apiUrl, path);

            var uriBuilder = new UriBuilder(apiUriWithPath)
            {
                Query = FormatQuery(queryParameters)
            };

            return new Uri(uriBuilder.ToString());
        }

        private static string FormatQuery(Dictionary<string, string> queryParameters)
        {
            var nameValueCollection = HttpUtility.ParseQueryString("");

            foreach (var queryParameter in queryParameters)
            {
                nameValueCollection[queryParameter.Key] = queryParameter.Value;
            }

            return nameValueCollection.ToString();
        }
    }
}