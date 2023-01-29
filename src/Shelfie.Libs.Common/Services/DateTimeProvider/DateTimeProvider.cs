namespace Shelfie.Libs.Common.Services.DateTimeProvider;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
