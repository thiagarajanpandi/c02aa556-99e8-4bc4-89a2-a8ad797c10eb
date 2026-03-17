using SubsequenceFinder;

ISubsequenceFinderService finder = new SubsequenceFinderService();

while (true)
{
    Console.Write("Enter integers separated by spaces (or 'exit' to quit): ");
    string input = Console.ReadLine() ?? string.Empty;

    if (string.Equals(input.Trim(), "exit", StringComparison.OrdinalIgnoreCase))
    {
        break;
    }

    try
    {
        Console.WriteLine(finder.LongestIncreasingRun(input));
    }
    catch (ArgumentException ex)
    {
        Console.Error.WriteLine($"Invalid input: {ex.Message}");
    }
}
