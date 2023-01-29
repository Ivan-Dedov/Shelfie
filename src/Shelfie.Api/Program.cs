using Shelfie.Libs.Common.Extensions;

namespace Shelfie.Api;

public sealed class Program
{
    public static Task Main()
        => Host
            .CreateDefaultBuilder()
            .ConfigureShelfie<Startup>()
            .Build()
            .RunAsync()
            ;
}
