using EoWordle.Services;
using EoWordle.Util;

namespace EoWordle.Models;

public class GameModel
{
    private readonly IGameService _gameService;
    private readonly IWordService _wordService;

    private readonly int _wordLength;
    private readonly int _maxGuesses;
    private string _correctWord = string.Empty;
    private int _currentGuessIndex = 0;

    public GameModel(IWordService wordService, IGameService gameService)
    {
        _wordLength = GameConfig.WordLength;
        _maxGuesses = GameConfig.MaxGuesses;

        _gameService = gameService;
        _wordService = wordService;

        SetCorrectWord(_wordService.GetRandomWord());
    }

    public GuessResult CheckGuess(string guess)
    {
        _currentGuessIndex++;
        return _gameService.CheckGuess(guess, _correctWord);
    }

    public bool DoesWordExistInList(string guess)
    {
        var wordList = _wordService.GetWordList();
        return wordList.Contains(guess);
    }

    public string GetCorrectWord()
    {
        return _correctWord;
    }

    public void SetCorrectWord(string correctWord)
    {
        _correctWord = correctWord;
    }

    public bool WonGame(string guess)
    {
        return guess == _correctWord;
    }

    public bool GameOver()
    {
        return _currentGuessIndex >= _maxGuesses;
    }

    public void ResetGame()
    {
        _currentGuessIndex = 0;
        _correctWord = _wordService.GetRandomWord();
    }
}