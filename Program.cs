using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
    .AddEnvironmentVariables()
    .Build();

config.GetSection("Section1").Bind(MyConfiguration.Section1);

Console.WriteLine($"Value1: {MyConfiguration.Section1.Value1}");
Console.WriteLine($"SubSection1: [{MyConfiguration.Section1.SubSection1.A}, {MyConfiguration.Section1.SubSection1.B}]");
Console.WriteLine($"SubSection2: [{MyConfiguration.Section1.SubSection2?.A.ToString() ?? "null"}, {MyConfiguration.Section1.SubSection2?.B.ToString() ?? "null"}]");

public static class MyConfiguration
{
    public static Section Section1 { get; set; } = new();
}

public sealed class Section
{
    public int Value1 { get; set; } = 1;

    public SubSection SubSection1 { get; set; } = new(1, 2);

    public SubSection? SubSection2 { get; set; } = null;
}

public class SubSection
{
    public int A { get; }

    public int B { get; }

    public SubSection(int a, int b)
    {
        A = a;
        B = b;
    }
}

[JsonSourceGenerationOptions(
    GenerationMode = JsonSourceGenerationMode.Default, WriteIndented = true)] // https://github.com/dotnet/runtime/issues/78602#issuecomment-1322004254
[JsonSerializable(typeof(Section))]
[JsonSerializable(typeof(SubSection))]
internal partial class SectionJsonSerializerContext : JsonSerializerContext
{
}

[JsonSourceGenerationOptions(
    GenerationMode = JsonSourceGenerationMode.Default, WriteIndented = true)] // https://github.com/dotnet/runtime/issues/78602#issuecomment-1322004254
[JsonSerializable(typeof(SubSection))]
internal partial class SubSectionJsonSerializerContext : JsonSerializerContext
{
}
