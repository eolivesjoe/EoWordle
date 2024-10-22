using EoWordle.Models;
using EoWordle.Util;
using EoWordle.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace EoWordle.Views
{
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

        // setup the game grid
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
                        FontSize = 16,
                        Width = 50,
                        Height = 50,
                        TextAlignment = TextAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center
                    };
                    Grid.SetRow(textBox, row);
                    Grid.SetColumn(textBox, col);

                    _textBoxes[row, col] = textBox;
                    GuessesGrid.Children.Add(textBox);
                }
            }
        }

        // handling for key presses
        private void GuessTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            var viewModel = DataContext as GameViewModel;
            if (e.Key == Key.Enter)
            {
                viewModel?.SubmitGuess();
            }
            if (e.Key == Key.Escape)
            {
                viewModel?.ClearGuess();
            }
        }

        // add the guess result to the grid
        public void AddGuessToGrid(GuessResult result)
        {
            for (int col = 0; col < _gridSize; col++)
            {
                _textBoxes[_currentGuessRow, col].Text = result.Guess[col].ToString();
                _textBoxes[_currentGuessRow, col].Background = result.Colours[col];
            }
            _currentGuessRow++;
        }

        public void ResetGrid()
        {
            _currentGuessRow = 0;
            for (int row = 0; row < _gridSize; row++)
            {
                for (int col = 0; col < _gridSize; col++)
                {
                    _textBoxes[row, col].Text = string.Empty;
                    _textBoxes[row, col].Background = Brushes.White;
                }
            }
        }
    }
}
