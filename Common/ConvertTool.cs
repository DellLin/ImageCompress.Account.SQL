public static class ConvertTool
{
    public static DateTime? ConvertToDatetime(string datetimeString)
    {
        if (string.IsNullOrEmpty(datetimeString))
        {
            return null;
        }
        else
        {
            return Convert.ToDateTime(datetimeString);
        }
    }
    public static Guid? ConvertToGuid(string guidString)
    {
        if (string.IsNullOrEmpty(guidString))
        {
            return null;
        }
        else
        {
            return Guid.Parse(guidString);
        }
    }
}
