using System.Text.Json.Serialization;

namespace PRN232.Lab2.CoffeeStore.API.Models
{
    public class ApiResponse
    {

        [JsonPropertyName("isSuccess")]
        public bool IsSuccess { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("errors")]
        public List<ApiError>? Errors { get; set; }
    }

    public class ApiResponse<T> : ApiResponse
    {
        [JsonPropertyName("data")]
        public T? Data { get; set; }
    }

    public class ApiError
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
    }
}
