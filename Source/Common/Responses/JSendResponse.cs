using System.Net;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonConverter = Newtonsoft.Json.JsonConverter;

namespace Common.Responses
{
    public class JSendResponse
    {
        /// <summary>Gets the status of this response.</summary>
        [JsonProperty("status")]
        [JsonPropertyName("status")]
        public string Status { get; set; }

        /// <summary>Gets the wrapper for any data to be returned; null if no data is to be returned.</summary>
        [JsonProperty("data")]
        [JsonPropertyName("data")]
        public JToken Data { get; set; }

        /// <summary>Gets the error message explaining what went wrong.</summary>
        [JsonProperty("message")]
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>Gets the numeric error code corresponding to the error.</summary>
        [JsonProperty("code")]
        [JsonPropertyName("code")]
        public int? Code { get; set; }

        public T Deserialize<T>()
        {
            var settings = JsonConvert.DefaultSettings();
            settings.DateParseHandling = DateParseHandling.DateTime;

            return Data.ToObject<T>(JsonSerializer.CreateDefault(settings));
        }

        public T Deserialize<T>(params JsonConverter[] converters)
        {
            var settings = JsonConvert.DefaultSettings();
            settings.DateParseHandling = DateParseHandling.DateTime;

            foreach (var item in converters)
                settings.Converters.Add(item);

            return Data.ToObject<T>(JsonSerializer.CreateDefault(settings));
        }

        public bool IsSuccess() => Status == "success";

        public bool HasFailed() => Status == "fail";

        public bool HasError() => Status == "error";

        public bool HasFailedValidation() => HasFailed() && Message == "Validation Failed";

        public bool IsRateLimited()
        {
            return Code == (int)HttpStatusCode.TooManyRequests;
        }

        public uint RetryAfter()
        {
            if (!IsRateLimited())
                return 0;

            var response = Deserialize<ApiLimit>();
            return uint.TryParse(response.RetryAfter, out var result) ? result : 0;
        }

        public class ApiLimit
        {
            public string EndPoint { get; set; }

            public string Limit { get; set; }

            public string Period { get; set; }

            public string RetryAfter { get; set; }
        }
    }
}
