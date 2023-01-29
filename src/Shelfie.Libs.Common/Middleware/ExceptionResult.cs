namespace Shelfie.Api.Users.Middleware;

internal sealed class ExceptionResult
{
    public string Title { get; init; }

    public int Code { get; init; }

    public string? Source { get; init; }

    public string Message { get; init; }

    public string? StackTrace { get; init; }
}
