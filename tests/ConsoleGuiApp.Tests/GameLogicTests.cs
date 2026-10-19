using Xunit;
using ConsoleGuiApp;

namespace ConsoleGuiApp.Tests;

public class GameLogicTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(50)]
    [InlineData(100)]
    public void CreateTarget_ReturnsValueWithinInclusiveRange(int maxValue)
    {
        for (int i = 0; i < 100; i++)
        {
            int target = GameLogic.CreateTarget(maxValue, new Random(i));
            Assert.InRange(target, 1, maxValue);
        }
    }

    [Fact]
    public void CreateTarget_RejectsZeroMaximum()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => GameLogic.CreateTarget(0));
    }

    [Theory]
    [InlineData(1, 10, true)]
    [InlineData(10, 10, true)]
    [InlineData(0, 10, false)]
    [InlineData(11, 10, false)]
    [InlineData(1, 0, false)]
    public void IsValidGuess_ValidatesBoundaries(int guess, int maxValue, bool expected)
    {
        Assert.Equal(expected, GameLogic.IsValidGuess(guess, maxValue));
    }

    [Theory]
    [InlineData(4, 5, GuessResult.TooLow)]
    [InlineData(6, 5, GuessResult.TooHigh)]
    [InlineData(5, 5, GuessResult.Correct)]
    public void EvaluateGuess_ReturnsExpectedResult(int guess, int target, GuessResult expected)
    {
        Assert.Equal(expected, GameLogic.EvaluateGuess(guess, target));
    }
}
