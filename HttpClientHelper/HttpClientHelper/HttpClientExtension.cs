﻿using System.Web;

namespace HttpClientHelper
{
    public static class HttpClientExtension
    {
        public static Uri AddParameters(this Uri uri, IDictionary<string, string> parameters)
        {
            var uriBuilder = new UriBuilder(uri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            foreach (var param in parameters.ToDictionary())
            {
                query[param.Key] = param.Value;
            }
            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }

        public static HttpRequestMessage WithHeaders(this HttpRequestMessage request, IDictionary<string, string> headers)
        {
            if (headers != null && headers.Any())
            {
                foreach (var header in headers.ToDictionary())
                {
                    request.Headers.TryAddWithoutValidation(header.Key, header.Value);
                } 
            }
            return request;
        }
    }
}
