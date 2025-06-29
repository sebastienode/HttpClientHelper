namespace HttpClientHelper
{
    public class HttpClientWrapper
    {
        private HttpClient _client;

        public HttpClientWrapper(HttpClient httpClient)
        {
            _client = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return await _client.SendAsync(request);
        }

        public HttpRequestMessage CreateRequest(HttpMethod method, string relativeUri, IToDictionnary? parameters)
        {
            if (_client.BaseAddress == null)
            {
                throw new InvalidOperationException("BaseAddress cannot be null.");
            }

            var uri = new Uri(_client.BaseAddress, relativeUri);

            if (parameters != null)
            {
                var dicoParam = parameters.ToDictionary();
                if (dicoParam != null && dicoParam.Any())
                {
                    uri = uri.AddParameters(dicoParam);
                }
            }

            return new HttpRequestMessage(method, uri);
        }

    }
}
