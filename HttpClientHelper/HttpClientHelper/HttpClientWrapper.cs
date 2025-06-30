namespace HttpClientHelper
{
    public class HttpClientWrapper
    {
        private HttpClient _client;

        public HttpClientWrapper(HttpClient httpClient)
        {
            _client = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        /// <summary>
        /// Sends an HTTP request message and returns the response message asynchronously.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return await SendAsync(request, CancellationToken.None);
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken, HttpCompletionOption completionOption = HttpCompletionOption.ResponseHeadersRead)
        {
            return await _client.SendAsync(request, completionOption, cancellationToken);
        }

        /// <summary>
        /// Creates an HTTP request message with the specified method, relative URI, and optional parameters.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="relativeUri"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
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
