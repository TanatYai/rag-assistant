using Microsoft.AspNetCore.WebUtilities;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Text.Json.Serialization;

namespace RagAssistant.Share.Helpers
{
    public static class RequestHelpers
    {
        public static string ToUrl(string baseUrl, string endpoint = "")
        {
            return ToUrl<object>(baseUrl, endpoint, null);
        }

        public static string ToUrl<T>(string baseUrl, string endpoint = "", T? parameters = default)
        {
            string url = new Uri(new Uri(baseUrl.TrimEnd('/') + "/"), endpoint.TrimStart('/')).ToString();

            if (parameters == null || typeof(T) == typeof(string))
            {
                return url;
            }

            foreach (PropertyInfo propertyInfo in parameters.GetType().GetProperties())
            {
                string propertyName = (propertyInfo.GetCustomAttribute(typeof(JsonPropertyNameAttribute)) as JsonPropertyNameAttribute)?.Name ?? propertyInfo.Name;
                object? propertyValue = propertyInfo.GetValue(parameters);

                if (string.IsNullOrEmpty(propertyName) || propertyValue == null)
                {
                    continue;
                }

                if (propertyValue is Array arrayValue)
                {
                    foreach (object? item in arrayValue)
                    {
                        url = QueryHelpers.AddQueryString(url, propertyName, item?.ToString() ?? string.Empty);
                    }
                }
                else if (propertyValue is IList listValue)
                {
                    foreach (object? item in listValue)
                    {
                        url = QueryHelpers.AddQueryString(url, propertyName, item?.ToString() ?? string.Empty);
                    }
                }
                else
                {
                    url = propertyValue is DateOnly dateOnlyValue
                        ? QueryHelpers.AddQueryString(url, propertyName, dateOnlyValue.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))
                        : propertyValue is DateTime dateTimeValue
                            ? QueryHelpers.AddQueryString(url, propertyName, dateTimeValue.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture))
                            : QueryHelpers.AddQueryString(url, propertyName, propertyValue?.ToString() ?? string.Empty);
                }
            }

            return url;
        }
    }
}
