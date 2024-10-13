namespace Application.Model.Tara.Request;

public record AuthenticateRequestModel
{
    public string username {  get; set; }
    public string password {  get; set; }
}
