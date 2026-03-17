using System.Text.Json;
using SubsequenceFinder;

namespace SubsequenceFinder.Unit.Tests;

public class SubsequenceFinderTests
{
    private readonly ISubsequenceFinderService _finder = new SubsequenceFinderService();

    [Theory]
    [InlineData("6 1 5 9 2", "1 5 9")]          // given test case
    [InlineData("1 2 3 4", "1 2 3 4")]           // fully increasing
    [InlineData("4 3 2 1", "4")]                 // fully decreasing → first element
    [InlineData("5", "5")]                       // single element
    [InlineData("1 3 2 4", "1 3")]               // tie → earliest wins
    [InlineData("3 3 3", "3")]                   // all equal (not strictly increasing)
    [InlineData("1 2 1 2 3", "1 2 3")]           // second run longer
    public void LongestIncreasingRun_ReturnsExpected(string input, string expected)
    {
        string result = _finder.LongestIncreasingRun(input);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void LongestIncreasingRun_NullOrWhitespace_Throws(string? input)
    {
        Assert.Throws<ArgumentException>(() => _finder.LongestIncreasingRun(input!));
    }

    [Theory]
    [InlineData("1 two 3")]
    [InlineData("abc")]
    [InlineData("1.5 2.5")]
    public void LongestIncreasingRun_NonInteger_Throws(string input)
    {
        Assert.Throws<ArgumentException>(() => _finder.LongestIncreasingRun(input));
    }

    public static TheoryData<string, string, string> FileTestCases()
    {
        string file = Path.Combine(AppContext.BaseDirectory, "TestData", "testcases.json");
        var cases = JsonSerializer.Deserialize<JsonElement[]>(File.ReadAllText(file))!;
        var data = new TheoryData<string, string, string>();
        int i = 1;
        foreach (var c in cases)
        {
            string input       = c.GetProperty("input").GetString()!;
            string expected    = c.GetProperty("expected").GetString()!;
            string description = c.TryGetProperty("description", out var d) ? d.GetString()! : $"testcases[{i}]";
            data.Add(input, expected, description);
            i++;
        }
        return data;
    }

    [Theory]
    [MemberData(nameof(FileTestCases))]
    public void LongestIncreasingRun_FileCase(string input, string expected, string caseName)
    {
        string result = _finder.LongestIncreasingRun(input);
        Assert.True(result == expected, $"[{caseName}] Expected: {expected}  Got: {result}");
    }
}
