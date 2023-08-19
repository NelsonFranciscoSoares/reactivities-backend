namespace Application.Core
{
    public class ApplicationException
    {
        public ApplicationException(int statusCode, string message, string details = null)
        {
            this.Details = details;
            this.Message = message;
            this.StatusCode = statusCode;
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}