namespace SubsequenceFinder;

public class SubsequenceFinderService : ISubsequenceFinderService
{
    /// <summary>
    /// Finds the longest increasing subsequence from the input numbers separated by space.
    ///
    /// Algorithm (one loop, O(n)):
    ///   1. Split the input by space into parts and read each part as a number.
    ///   2. Go through the numbers one by one, keep going while each number is
    ///      bigger than the one before it.  When it is not bigger, move the current
    ///      start to this position.
    ///   3. After each step, check if the current length is longer than the best
    ///      length found so far.  If yes, save the current start and length as best.
    ///   4. Give back the slice at the best start with the best length as a string.
    /// </summary>
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
