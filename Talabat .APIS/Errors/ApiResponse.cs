
namespace Talabat_.APIS.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageStatusCode(statusCode);
        }

        private string? GetDefaultMessageStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "a bad request, you have made",
                401 => "Authorized, you are not",
                404 => "Not Found",
                500 => "Server Error",
                _ => null

            };
        }
    }
}
