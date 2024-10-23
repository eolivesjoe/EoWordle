# EoWordle

EoWordle is a Wordle-inspired game built using C# and WPF with a customisable word feature and dynamic game logic.
The project demonstrates the use of the MVVM pattern, dependency injection, and custom game configuration.


## Overview

EoWordle allows players to guess a hidden word within a limited number of attempts. It includes the following features:

- Random word generation from a predefined list
- Custom word support via command-line arguments
- Color-coded feedback based on the correctness of the guess

## Features

- **Random Word Selection**: Automatically selects a random word from a provided word list (`words.txt`).
- **Custom Words**: Users can pass a custom word to the game at launch.
- **Custom word length and difficulty**: Users can select word length and number of guesses by changing the game config.
- **Visual Feedback**: Colors indicate correct and misplaced letters during gameplay.

## Files

- `App.xaml.cs`: Sets up the WPF application, handles dependency injection, and processes startup logic.
- `GameViewModel.cs`: Handles the game logic, manages user guesses, and updates the UI.
- `GameView.xaml.cs`: Manages the visual components of the game, such as the grid for guesses.
- `GameService.cs`: Contains the logic for checking user guesses and determining the result.
- `WordService.cs`: Handles loading the word list from a file and selecting a random word for gameplay.
- `GameModel.cs`: Stores the game state, including the correct word, guesses, and other core data.
- `GameConfig.cs`: Stores constants for word length and the maximum number of guesses.
- `words.txt`: A text file containing a list of words for the game.

## Installation

1. Clone the repository

2. Open the project in Visual Studio or your preferred C# IDE.

3. Restore the NuGet packages required by the project.

4. Build and run the project.