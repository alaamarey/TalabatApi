namespace Talabat.API.Errors
{
    public class ApiExceptionResponse : ApiResponse
    {
        public string? Details { get; set; }

        public ApiExceptionResponse(string? details , string? message , int code):base(code , message)
        {
            Details = details;
        }


      
    }
}
