

namespace Application.Common;

public enum FailType
{
    [Description("عملیات لغو شد.")]
    Cancellation = 0,
    [Description("موجودیت پیدا نشد.")]
    NotFound = 1,
    [Description("عملیات ثبت ناموفق بود.")]
    InsertFail = 2,
    [Description("عملیات بروزرسانی ناموفق بود.")]
    UpdateFail = 3,
    [Description("صندوق پذیرنده غیرفعال است.")]
    NotValidTerminal =4,
    [Description("محتوایی یافت نشد.")]
    NoContent =5

}