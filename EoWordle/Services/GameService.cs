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

        // Second loop we check anything in the guess that hasn't been painted green, if it still exists in the target word, we paint it yellow, and remove it from the target word
        // if no match exists we paint it gray
        for (int i = 0; i != guess.Length; ++i)
        {
            if (colours[i] != Brushes.Green)
            {
                if (charExists[guess[i] - 'A'] > 0)
                {
                    colours[i] = Brushes.Yellow;
                    charExists[guess[i] - 'A']--;
                }
                else
                {
                    colours[i] = Brushes.Gray;
                }
            }
        }
        return result;
    }
}