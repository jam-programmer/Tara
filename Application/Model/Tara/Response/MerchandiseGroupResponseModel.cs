namespace Application.Model.Tara.Response;

public record MerchandiseGroupResponseModel
{
    public string? tag {  get; set; }
    public long id { get; set; }
    public string? title {  get; set; }
    public string? name { get; set; }
}
