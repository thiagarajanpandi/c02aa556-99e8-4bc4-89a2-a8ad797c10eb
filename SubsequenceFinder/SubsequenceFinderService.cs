namespace SubsequenceFinder;

public class SubsequenceFinderService : ISubsequenceFinderService
{
    public string LongestIncreasingRun(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException("Input must not be empty.");
        }

        string[] parts = input.Trim().Split(' ');

        int previousValue = 0;
        int currentStart = 0;
        int bestStart = 0;
        int bestLength = 1;

        for (int i = 0; i < parts.Length; i++)
        {
            if (!int.TryParse(parts[i], out int currentValue))
            {
                throw new ArgumentException("Input must contain only integers separated by single spaces.");
            }

            if (i > 0)
            {
                if (currentValue > previousValue)
                {
                    int currentLength = i - currentStart + 1;
                    if (currentLength > bestLength)
                    {
                        bestStart = currentStart;
                        bestLength = currentLength;
                    }
                }
                else
                {
                    currentStart = i;
                }
            }

            previousValue = currentValue;
        }

        return string.Join(' ', parts[bestStart..(bestStart + bestLength)]);
    }
}
