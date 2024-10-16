namespace Application.Model.Tara.Request;

public sealed record ServiceAmount
{
    public long serviceId { get;set;}  
    public long amount { get;set;}
}
