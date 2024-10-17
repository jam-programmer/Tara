namespace Application.Model.Tara.Common;

public sealed record ServiceAmount
{
    public long serviceId { get; set; }
    public long amount { get; set; }
}
