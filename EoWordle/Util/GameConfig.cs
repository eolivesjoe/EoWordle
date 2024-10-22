// Store the word length and max guesses to make it easy to update in the future if you want to make it easier or more difficult

namespace EoWordle.Util
{
    public static class GameConfig
    {
        public static int WordLength = 5;
        public static int MaxGuesses = WordLength;
    }
}
