
using EoWordle.Models;
using EoWordle.Util;
using System.Windows.Media;

namespace EoWordle.Services;

// Game logic
public class GameService : IGameService
{
    public GuessResult CheckGuess(string guess, string correctWord)
    {
        SolidColorBrush[] colours = new SolidColorBrush[GameConfig.WordLength];
        var result = new GuessResult(guess, colours);
        int[] charExists = new int[26];


        // First loop checks every char for a perfect match between the guess and the target word, paint green if it matches.
        // If not we make note of the char in the target word to be used for comparison to the guess later.
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

        // Second loop we check if any char in the guess match with the target word, if it does we paint it yellow and remove it from the array
        // If not we paint it gray
        for (int i = 0;i != guess.Length; ++i)
        {
            if (charExists[guess[i] - 'A'] > 0)
            {
                colours[i] = Brushes.Yellow;
                charExists[guess[i] - 'A']--;
            }
            else
            {
                if (colours[i] != Brushes.Green)
                {
                    colours[i] = Brushes.Gray;
                }
            }
        }
        return result;
    }
}

