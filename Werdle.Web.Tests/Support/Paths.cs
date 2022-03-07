namespace Werdle.Tests.Support;

using System;
using System.IO;

public static class Paths
{
    // IMPORTANT: do not change the order of these lines.
    // /Werdle/Werdle.Web.Tests/bin/Debug/net6.0/
    private static readonly string BaseDir = AppDomain.CurrentDomain.BaseDirectory;
    // /Werdle/Werdle.Web.Tests/
    private static readonly string TestProjectDir = new DirectoryInfo(BaseDir).Parent.Parent.Parent.FullName;
    private static readonly string SupportDir = Path.Join(TestProjectDir, "Support");

    public static readonly string CsvFile = Path.Join(SupportDir, "file.csv");
}
