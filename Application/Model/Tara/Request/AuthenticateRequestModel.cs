namespace Application.Model.Tara.Request;

public sealed record AuthenticateRequestModel
{
    public string username {  get; set; }
    public string password {  get; set; }
}
