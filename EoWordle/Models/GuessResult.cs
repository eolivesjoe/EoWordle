using System.Windows.Media;

namespace EoWordle.Models;

// Stores the guessed word as well as the colours for the squares
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