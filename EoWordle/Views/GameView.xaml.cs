using EoWordle.Models;
using EoWordle.Util;
using EoWordle.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace EoWordle.Views;

public partial class GameView : UserControl
{
    private int _gridSize;
    private int _currentGuessRow = 0;
    private TextBox[,] _textBoxes;

    public GameView()
    {
        _gridSize = GameConfig.WordLength;
        _textBoxes = new TextBox[_gridSize, _gridSize];

        InitializeComponent();
        CreateGrid();
    }

    private void CreateGrid()
    {
        for (int i = 0; i != _gridSize; ++i)
        {
            GuessesGrid.RowDefinitions.Add(new RowDefinition());
            GuessesGrid.ColumnDefinitions.Add(new ColumnDefinition());
        }

        for (int row = 0; row != _gridSize; ++row)
        {
            for (int col = 0; col != _gridSize; ++col)
            {
                TextBox textBox = new TextBox
                {
                    IsReadOnly = true,
                    Background = Brushes.White,
                    Margin = new Thickness(5),
                    FontSize = 16
                };
                Grid.SetRow(textBox, row);
                Grid.SetColumn(textBox, col);

                _textBoxes[row, col] = textBox;
                GuessesGrid.Children.Add(textBox);
            }
        }
    }

    private void GuessTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            var textBox = sender as TextBox;
            string guess = textBox.Text.ToUpper(System.Globalization.CultureInfo.InvariantCulture);

            if (guess?.Length == _gridSize)
            {
                var viewModel = DataContext as GameViewModel;
                var result = viewModel?.SubmitGuess(guess);

                AddGuessToGrid(result);
                _currentGuessRow++;

                textBox.Text = string.Empty;

                if (_currentGuessRow == _gridSize)
                {
                    MessageBox.Show("No more guesses left!");
                    textBox.IsEnabled = false;
                }
            }
            else 
            {
                MessageBox.Show("Do the correct length of guess you fucking moron");
            }
        }
    }

    private void AddGuessToGrid(GuessResult result)
    {
        for (int col = 0; col < _gridSize; col++)
        {
            _textBoxes[_currentGuessRow, col].Text = result.Guess[col].ToString();
            _textBoxes[_currentGuessRow, col].Background = result.Colours[col];

        }
    }
}