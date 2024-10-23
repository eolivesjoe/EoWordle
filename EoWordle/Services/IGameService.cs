using EoWordle.Models;

namespace EoWordle.Services;

public interface IGameService
{
    public GuessResult CheckGuess(string guess, string correctWord);
}