namespace Talabat.API.Errors
{
    public class ApiValidationErrorResponse :ApiResponse
    {
        public string[]  Errors  { get; set; }
        public ApiValidationErrorResponse(string[] errors):base(400)
        {
            Errors = errors;    
        }


    }
}
