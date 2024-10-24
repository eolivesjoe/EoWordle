# EoWordle

EoWordle is a Wordle-inspired game built using C# and WPF with a customisable word feature and dynamic game logic.

The solution contains 2 projects, one for the game and one for the tests.

How to play:

## Default Launch Instructions:
Open a terminal, navigate to the `EoWordle` folder, then run the following commands:
- `dotnet build`
- `dotnet run`

## Custom Word Launch Instructions:

To run EoWordle with a specific word as the correct answer, use command line arguments as follows:
- `dotnet build`
- `dotnet run -- <correct-word>`

The following restrictions apply to `<correct-word>`:
- must exist in words list (`EoWordle\Util\words.txt`)
- must be of the length specified in `EoWordle\Util\GameConfig.cs`

If a restriction is violated, EoWordle will ignore the input and select a random word from the words list which is of the specified GameConfig word length (ie: same as default launch).

## Play Instructions:
- follow launch instructions
- wait for app to launch
- type guess into the text box at the bottom of the screen
- submit guess by pressing the `Enter` key.
- when correct word guessed or all guesses have been used, a dialogue popup will appear
- when the dialogue popup is dismissed, a new game will start automatically with a random word (same as default launch)
- if multiple custom inputs are needed, the app will need to be quit and relaunched each time using `dotnet run -- <correct-word>`


## Assumptions:
- `words.txt` exists in `EoWordle\Util\` and contains one word per line. it cannot be empty.
- words in the list can only contain the letters a-z and A-Z.
- app is running on Windows (no other OS tested)
- the tests are based on the assumption that the GameConfig is set to classic Wordle rules (i.e 5 letters per word, and 6 guesses)

The word list used can be found here `http://gwicks.net/dictionaries.htm`

Please reach out if any questions arise or if you need anything at all.
Have fun!