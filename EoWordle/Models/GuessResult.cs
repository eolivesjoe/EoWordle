using System.Windows.Media;

namespace EoWordle.Models;

public class GuessResult
{
    public string Guess { get; set; } = string.Empty;
    public SolidColorBrush[] Colours;

    public GuessResult(string guess, SolidColorBrush[] colours)
    {
        Guess = guess;
        Colours = colours;
    }
}