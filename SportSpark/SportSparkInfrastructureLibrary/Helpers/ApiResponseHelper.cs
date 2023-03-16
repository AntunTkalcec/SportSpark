using Newtonsoft.Json;

namespace SportSparkInfrastructureLibrary.Helpers
{
    public class ApiResponseHelper
    {
        public int Status { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; }

        public ApiResponseHelper(int status, string message = null)
        {
            Status = status;
            Message = message ?? GetDefaultMessageForStatusCode(status);
        }

        private static string GetDefaultMessageForStatusCode(int status)
        {
            return status switch
            {
                401 => "That action is unauthorized.",
                404 => "Resource could not be found.",
                500 => "A server error has occurred.",
                _ => null,
            };
        }
    }
}
