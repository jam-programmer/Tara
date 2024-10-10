namespace Application.Model.Tara.Response
{
    public record TemporaryTokenResponseModel
    {
        public bool success { set; get; }
        public string? doTime { set; get; }
        public string? message { set; get; }
        public int code {  set; get; }
        public string? accessCode { set; get; }
        public long expiryDuration {  set; get; }
    }
}
