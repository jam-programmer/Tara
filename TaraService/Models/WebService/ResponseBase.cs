namespace TaraService.Models.WebService
{
    public record ResponseBase
    {
        public bool IsSuccess { get; set; }  
        public string? Message { get; set; } =string.Empty;
        public object? Data { get; set; }   

        public static ResponseBase Fail (string message=null,object data = null)
        {
            return new ResponseBase()
            {
                IsSuccess = false,
                Message = message,
                Data = data
            };
        }
        public static ResponseBase Success(string message = null, object data = null)
        {
            return new ResponseBase()
            {
                IsSuccess = true,
                Message = message,
                Data = data
            };
        }

    }
}
