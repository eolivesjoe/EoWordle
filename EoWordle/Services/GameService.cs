
using EoWordle.Models;
using EoWordle.Util;
using System.Windows.Media;

namespace EoWordle.Services;

public class GameService : IGameService
{
    public GuessResult CheckGuess(string guess, string correctWord)
    {
        SolidColorBrush[] colours = new SolidColorBrush[GameConfig.WordLength];
        var result = new GuessResult(guess, colours);
        int[] charExists = new int[26];

        for (int i = 0; i != guess.Length; ++i)
        {
            if (guess[i] == correctWord[i])
            {
                colours[i] = Brushes.Green;
            }
            else
            {
                charExists[correctWord[i] - 'A']++;
            }
        }

        for (int i = 0;i != guess.Length; ++i)
        {
            if (charExists[guess[i] - 'A'] > 0)
            {
                colours[i] = Brushes.Yellow;
                charExists[guess[i] - 'A']--;
            }
            else
            {
                colours[i] = colours[i] == Brushes.Green 
                    ? Brushes.Green 
                    : Brushes.Gray;
            }
        }

        return result;
    }
}

