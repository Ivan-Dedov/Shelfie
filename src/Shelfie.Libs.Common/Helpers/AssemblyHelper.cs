using System.Reflection;

namespace Shelfie.Libs.Common.Helpers;

internal static class AssemblyHelper
{
    internal static AssemblyName? EntryAssembly => Assembly.GetEntryAssembly()?.GetName();

    internal static string EntryAssemblyName => EntryAssembly?.Name ?? "Shelfie";

    internal static string EntryAssemblyVersion => EntryAssembly?.Version?.ToString() ?? "v1";
}
