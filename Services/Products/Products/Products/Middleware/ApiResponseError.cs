namespace Products.Middleware
{
    public class ApiResponseError
    {
        public int? StatusCode { get; set; }
        public string? ErrorMessage { get; set; }
        public string? StackTrace { get; set; }

        public ApiResponseError( int? statusCode, string? errorMessage, string? stackTrace)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
            StackTrace = stackTrace;
        }
    }
}
