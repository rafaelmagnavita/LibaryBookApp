using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LibraryHttpClient
{
    public class Client
    {
        public Client(string apiUrl)
        {
            _apiUrl = apiUrl;
        }
        private HttpClient httpClientWithAuth;
        private string _apiUrl;
        private string _userEmail;
        private Dictionary<string, object> parameters;
        private string GetParams(string requestUrl)
        {
            var queryString = new StringBuilder("?");
            foreach (var param in parameters)
            {
                if (param.Value != null)
                {
                    queryString.Append($"{Uri.EscapeDataString(param.Key)}={Uri.EscapeDataString(param.Value.ToString())}&");
                }
            }
            if (queryString.Length > 1)
            {
                queryString.Length -= 1;
            }
            return requestUrl + queryString.ToString();
        }

        public async Task<T> GetApiData<T>(string endpoint, Dictionary<string, object> param = null)
        {
            try
            {
                var requestUrl = _apiUrl + endpoint;
                parameters = param != null ? param : new Dictionary<string, object>();
                string apiUrlWithParams = param != null ? GetParams(requestUrl) : requestUrl;

                HttpResponseMessage response = await httpClientWithAuth.GetAsync(apiUrlWithParams);

                if (response.IsSuccessStatusCode)
                {
                    string jsonData = await response.Content.ReadAsStringAsync();
                    var jsonObject = JsonConvert.DeserializeObject<T>(jsonData);
                    return jsonObject;
                }
                else
                {
                    return default(T);
                }
            }
            catch (Exception ex)
            {
                return default(T);
            }

        }

        public async Task<int> PostApiData<T>(string endpoint, T entity, Dictionary<string, object> param = null)
        {
            try
            {
                    var requestUrl = _apiUrl + endpoint;
                    Dictionary<string, object> parameters = param != null ? param : new Dictionary<string, object>();
                    string apiUrlWithParams = param != null ? GetParams(requestUrl) : requestUrl;
                    HttpContent body = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await httpClientWithAuth.PostAsync(apiUrlWithParams, body);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonData = await response.Content.ReadAsStringAsync();
                        var jsonObject = JsonConvert.DeserializeObject<T>(jsonData);
                        return (int)response.StatusCode;
                    }
                    else
                    {
                        return (int)response.StatusCode;
                    }
            }
            catch (Exception ex)
            {
                return (int)HttpStatusCode.ServiceUnavailable;
            }
        }
    }

}
