
namespace Application.Model.Tara.Response;

public record TrackingCodeResponseModel
{
    public string? traceNumber { set; get; }
    public bool success { set; get; }
    public string? mobile { set; get; }
    public string? doTime { set; get; }
    public string? message {  set; get; }   
    public long code { set; get; }
}

