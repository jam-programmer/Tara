
namespace Application.Common.Extensions;
public static class Extension
{
    public static string GetEnumDescription(this Enum value)
    {
        FieldInfo field = value.GetType().GetField(value.ToString());
        DescriptionAttribute attribute =
            (DescriptionAttribute)Attribute.GetCustomAttribute(field!, typeof(DescriptionAttribute));
        return attribute != null ? attribute.Description : value.ToString();
    }

    public static string PersainDateTime(this DateTime dateTime)
    {
        PersianCalendar persianCalendar = new();
        int year=persianCalendar.GetYear(dateTime);
        int month=persianCalendar.GetMonth(dateTime);
        int day=persianCalendar.GetDayOfMonth(dateTime);
        int hour=persianCalendar.GetHour(dateTime);
        int minute=persianCalendar.GetMinute(dateTime);
        int second=persianCalendar.GetSecond(dateTime);

        return $"{year}/{month}/{day} - {hour} : {minute} : {second}";
    }
}