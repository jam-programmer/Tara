namespace Application.Model.Tara.Response;

public class AuthenticateResponseModel
{
    public string? accessToken { set; get; }
    public string? result {  set; get; } 
    public string? description { set; get; }
    public string? doTime { set; get; }
    public long? expireTime { set; get; }
}
