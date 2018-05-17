using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;


namespace AGLWebServiceTest.Environment
{
    public class HttpClient
    {
        private static HttpClient instance = null;
        private HttpResponseMessage _lastResponse;
        private string _lastResponseContent;

        private HttpClient()
        {
        }

        /// <summary>
        /// Singleton instance of Http client
        /// </summary>
        public static HttpClient Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HttpClient();
                }
                return instance;
            }
        }


        private static JsonMediaTypeFormatter MediaFormatter = new JsonMediaTypeFormatter
        {
            SerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            }
        };

        /// <summary>
        /// Get/Set Last response from the web service
        /// </summary>
        public HttpResponseMessage LastResponse
        {
            get
            {
                return _lastResponse;
            }

            set
            {
                _lastResponse = value;

                if (_lastResponse != null)
                {
                    _lastResponse.Content.LoadIntoBufferAsync().Wait();
                    _lastResponseContent = _lastResponse.Content.ReadAsStringAsync().Result;
                }
                else
                    _lastResponse = null;
            }
        }

        /// <summary>
        /// Get last response contents
        /// </summary>
        public string LastResponseContent
        {
            get
            {
                return _lastResponseContent;
            }
        }

        /// <summary>
        /// Get System.Net.Http.HttpClient instance
        /// </summary>
        /// <returns>System.Net.Http.HttpClient instance</returns>
        protected System.Net.Http.HttpClient GetHttpClient()
        {
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
            return httpClient;
        }

        /// <summary>
        /// HTTP GET
        /// </summary>
        /// <param name="request">Http request</param>
        /// <returns>Last Http Response</returns>
        public HttpResponseMessage Get(string request)
        {
            var httpClient = GetHttpClient();

            LastResponse = httpClient.GetAsync(request).Result;

            return LastResponse;
        }

        /// <summary>
        /// Deserialize JSON Object to get contents of response
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="formatter"></param>
        /// <returns>Deserialized Object</returns>
        public T LastResponseAs<T>(string url, MediaTypeFormatter formatter = null)
        {
            Get(url);
            EnsureSuccess();
            return JsonConvert.DeserializeObject<T>(LastResponseContent);
        }

        /// <summary>
        /// Validate status code from the HTTP Response
        /// </summary>
        public void EnsureSuccess()
        {
            if ((int)LastResponse.StatusCode >= 400)
            {
                throw new WebException($"Web request failed. StatusCode:{LastResponse.StatusCode} ReasonPhrase:{LastResponse.ReasonPhrase} ResponseContent:{LastResponseContent}");
            }
        }
    }
}
