using EoWordle.Services;
using EoWordle.Util;
using System.Windows.Media;

namespace EoWordle.Models;

public class GameModel
{
    private readonly IGameService _gameService;
    private readonly IWordService _wordService;

    private readonly int _wordLength;
    private readonly int _maxGuesses;
    private string _correctWord;
    private int _currentGuessIndex = 0;

    public bool IsGameOver = false;

    public GameModel(IWordService wordService, IGameService gameService)
    {
        _wordLength = GameConfig.WordLength;
        _maxGuesses = GameConfig.MaxGuesses;

        _gameService = gameService;
        _wordService = wordService;

        _correctWord = _wordService.GetWord();
    }

    public GuessResult CheckGuess(string guess)
    {
        if (guess.Length != _wordLength)
        {
            throw new ArgumentException($"Guess must be {_wordLength} letters long");
        }

        if (_currentGuessIndex >= _maxGuesses)
        {
            throw new InvalidOperationException("No more guesses allowed.");
        }

        guess = guess.ToUpper();
        _currentGuessIndex++;
        return _gameService.CheckGuess(guess, _correctWord);
    }
}