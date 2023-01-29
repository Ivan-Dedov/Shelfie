namespace Shelfie.Libs.Common.Services.DateTimeProvider;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
