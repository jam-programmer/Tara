
using Application.Common;

namespace Infrastructure.WebService
{
    public class ApiService(ILogger<ApiService> logger) : IApiService
    {
        private ILogger<ApiService> _logger = logger;


        public async Task<TResult> GetAsync<TResult>(ApiOption config, CancellationToken cancellationToken = default)
        {
            HttpClient httpClient = new HttpClient();
            SetTimeout(config.TimeOut, httpClient);
            AddHeaders(config.HeaderParameters, httpClient);
            Authorization(config.BearerToken, httpClient);
            string requestUrl = BuildRequestUrl(config.BaseUrl, config.QueryParameters);

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl,cancellationToken);
                return await HandleResponse<TResult>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,Message.WebServiceException+$" - {config.BaseUrl}");
                return default;
            }
        }

        public async Task<TResult> PostAsync<TResult>(ApiOption config, CancellationToken cancellationToken = default)
        {
            HttpClient httpClient = new HttpClient();

            SetTimeout(config.TimeOut, httpClient);
            AddHeaders(config.HeaderParameters, httpClient);
            Authorization(config.BearerToken, httpClient);
            HttpContent content = CreateContent(config.DataBody);

            try
            {
                HttpResponseMessage response = await httpClient.PostAsync(config.BaseUrl, content,cancellationToken);
                return await HandleResponse<TResult>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Message.WebServiceException + $" - {config.BaseUrl}");
                return default;
            }
        }
        private void Authorization(string token, in HttpClient httpClient)
        {
            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }


        private void SetTimeout(TimeSpan? timeout, in HttpClient httpClient)
        {
            if (timeout.HasValue && timeout.Value.TotalMinutes > 0)
            {
                httpClient.Timeout = timeout.Value;
            }
        }

        private void AddHeaders(Dictionary<string, string> headers, in HttpClient httpClient)
        {
            if (headers != null)
            {
                foreach (var parameter in headers)
                {
                    httpClient.DefaultRequestHeaders.Add(parameter.Key, parameter.Value);
                }
            }
        }

        private string BuildRequestUrl(string baseUrl, Dictionary<string, string> queryParameters)
        {
            if (queryParameters == null || queryParameters.Count == 0)
            {
                return baseUrl;
            }

            string query = "?" + string.Join("&",
                queryParameters.Select(p => $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value)}"));
            return baseUrl + query;
        }

        private HttpContent CreateContent(object dataBody)
        {
            if (dataBody == null)
            {
                return null;
            }

            string jsonData = JsonConvert.SerializeObject(dataBody);
            return new StringContent(jsonData, Encoding.UTF8, "application/json");
        }

        private async Task<TResult> HandleResponse<TResult>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResult>(responseContent)!;
            }

            string responseContentError = await response.Content.ReadAsStringAsync();
            _logger.LogError( Message.WebServiceExceptionDetail + $" - {responseContentError}");

            return default!;
        }

        public async Task PostWithOutResponseAsync(ApiOption config, CancellationToken cancellationToken)
        {
            HttpClient httpClient = new HttpClient();
            SetTimeout(config.TimeOut, httpClient);
            AddHeaders(config.HeaderParameters, httpClient);
            Authorization(config.BearerToken, httpClient);
            MultipartFormDataContent formContent= SetFormData(config.Data);
            try
            {
                await httpClient.PostAsync(config.BaseUrl, formContent, cancellationToken);
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Message.WebServiceException + $" - {config.BaseUrl}");
               
            }
        }
        private MultipartFormDataContent? SetFormData(Dictionary<string, string> data)
        {
            MultipartFormDataContent formData = new MultipartFormDataContent();

            if(data == null)
            {
                return null;
            }
            foreach (var item in data)
            {
               
                formData.Add(new StringContent(item.Key), item.Value);
              
            }

            return formData;
        }
    }
}
