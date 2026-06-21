namespace Talabat.API.Errors
{
    public class ApiResponse
    {

        public string? Message { get; set; }

        public int StatusCode { get; set; }
        public ApiResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = string.IsNullOrEmpty(message) ? GetMessageBaseOnStatusCode() : message;
        }


        private string? GetMessageBaseOnStatusCode()
        {
            return StatusCode switch
            {
                400 => "A bad Request You have made",
                404 => "Resourse was not found",
                401 => "Autherized , you are not",
                500 => "Errors are the path to Dark side , Errors lead to anger , anger lead to hate , hate lead to carear shift",
                _ => null,
            };


        }
    }
}
