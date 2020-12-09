using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tic_Tac_Toe_Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members
        /// <summary>
        /// Holds the current results of cells in the active game.
        /// </summary>
        private MarkType[] results;

        /// <summary>
        /// True if it is player 1's turn (X) or false is player 2's turn (O).
        /// </summary>
        private bool player1Turn;

        /// <summary>
        /// True if the game has ended.
        /// </summary>
        private bool gameEnded;
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }
        #endregion

        #region Game Mechanics
        /// <summary>
        /// Starts a new game and clears all values back to the start.
        /// </summary>
        private void NewGame()
        {
            // Create a new blank array of free cells.
            results = new MarkType[9];

            for (var i = 0; i < results.Length; i++)
                results[i] = MarkType.Free;

            // Sets the turn to player 1.
            player1Turn = true;

            // Iterate every button on the grid.
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                // Change background, foreground and content to default values.
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            // Make sure the game hasn't finished.
            gameEnded = false;
        }

        /// <summary>
        /// Handles a button click event.
        /// </summary>
        /// <param name="sender">The button that was clicked.</param>
        /// <param name="e">The events of the click</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Start a new game on the click after it finished.
            if(gameEnded)
            {
                NewGame();
                return;
            }

            // Cast the sender to a button.
            var button = (Button)sender;

            // Find the buttons poisition in the array.
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            // Don't do anyhting if the cell already has a value in it.
            if (results[index] != MarkType.Free)
                return;

            // Set the cell value based on which players turn it is.
            results[index] = player1Turn ? MarkType.Cross : MarkType.Nought;

            // Set button text to the result.
            button.Content = player1Turn ? "X" : "O";

            //Change noughts to green
            if (!player1Turn)
                button.Foreground = Brushes.Red;

            // Toggle the players turn
            player1Turn ^= true;

            // Check for a winner
            CheckForWinner();
        }

        /// <summary>
        /// Checks if there is a winner of a 3 line straight.
        /// </summary>
        private void CheckForWinner()
        {
            #region Horizontal Wins
            // Check for horizontal winner.
            //
            //  - Row 0
            //
            if (results[0] != MarkType.Free && (results[0] & results[1] & results[2]) == results[0])
            {
                // Game ends.
                gameEnded = true;

                // Highlight winning cells in green.
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }
            //
            //  - Row 1
            //
            if (results[3] != MarkType.Free && (results[3] & results[4] & results[5]) == results[3])
            {
                // Game ends.
                gameEnded = true;

                // Highlight winning cells in green.
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }
            //
            //  - Row 2
            //
            if (results[6] != MarkType.Free && (results[6] & results[7] & results[8]) == results[6])
            {
                // Game ends.
                gameEnded = true;

                // Highlight winning cells in green.
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion

            #region Vertical Wins
            // Check for vertical winner.
            //
            //  - Column 0
            //
            if (results[0] != MarkType.Free && (results[0] & results[3] & results[6]) == results[0])
            {
                // Game ends.
                gameEnded = true;

                // Highlight winning cells in green.
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }
            //
            //  - Column 1
            //
            if (results[1] != MarkType.Free && (results[1] & results[4] & results[7]) == results[1])
            {
                // Game ends.
                gameEnded = true;

                // Highlight winning cells in green.
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }
            //
            //  - Column 2
            //
            if (results[2] != MarkType.Free && (results[2] & results[5] & results[8]) == results[2])
            {
                // Game ends.
                gameEnded = true;

                // Highlight winning cells in green.
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion

            #region Diagonal Wins
            // Check for diagonal winner.
            //
            //  - Top Left Bottom Right
            //
            if (results[0] != MarkType.Free && (results[0] & results[4] & results[8]) == results[0])
            {
                // Game ends.
                gameEnded = true;

                // Highlight winning cells in green.
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }
            //
            //  - Top Right Bottom Left
            //
            if (results[2] != MarkType.Free && (results[2] & results[4] & results[6]) == results[2])
            {
                // Game ends.
                gameEnded = true;

                // Highlight winning cells in green.
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }
            #endregion

            #region No Winner
            //Check for no winner and full board.
            if (!results.Any(result => result == MarkType.Free))
            {
                // Game ends.
                gameEnded = true;

                // Turn all cells orange.
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });
            }
            #endregion
        }
    }

    #endregion
}
