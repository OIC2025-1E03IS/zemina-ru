namespace ConsoleGuiApp;

public enum GuessResult
{
    TooLow,
    TooHigh,
    Correct
}

public static class GameLogic
{
    public static int CreateTarget(int maxValue, Random? random = null)
    {
        if (maxValue < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(maxValue), "最大値は1以上にしてください。");
        }

        return (random ?? Random.Shared).Next(1, maxValue + 1);
    }

    public static bool IsValidGuess(int guess, int maxValue) =>
        maxValue >= 1 && guess >= 1 && guess <= maxValue;

    public static GuessResult EvaluateGuess(int guess, int target)
    {
        if (guess < target) return GuessResult.TooLow;
        if (guess > target) return GuessResult.TooHigh;
        return GuessResult.Correct;
    }
}
