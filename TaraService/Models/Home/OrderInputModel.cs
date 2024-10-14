namespace Tara.Models.Home;

public record OrderInputModel
{
    public string? detail { get; set; }
    public string? dateTime { get; set; }
    public string? fullName { get; set; }
    public string? phoneNumber {  get; set; }
    public string? barcode {  get; set; }
    public string? terminal { set; get; }
}
