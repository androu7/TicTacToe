using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region private members
        /// <summary>
        /// Holds the current results of cells in the active game
        /// </summary>
        private MarkType[] mResults;

        /// <summary>
        /// True if it is player 1's turn(X) or player 2's turn (O)
        /// </summary>
        private bool mPlayer1Turn;

        /// <summary>
        /// True if the game has ended
        /// </summary>
        private bool mGameEnded;
        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }
        #endregion


        #region Start New Game
        private void NewGame()
        {
            //Create a new blank array of free cells
            mResults = new MarkType[9];

            for (var i = 0; i < mResults.Length; i++)
                mResults[i] = MarkType.Free;

            //Make sure Player 1 starts the game
            mPlayer1Turn = true;

            //Interate every button on the grid..
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                //Change backround, foreground and content to default values
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            //Make sure the game hasn't finished
            mGameEnded = false;
        }

        #endregion

        #region Click Button
        /// <summary>
        /// handles a button click event
        /// </summary>
        /// <param name="sender"></param> the button that was clicked
        /// <param name="e"></param>  the events of the clicked
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mGameEnded)
            {
                NewGame();
                return;
            }
            //Cast the sender to a button
            var button = (Button)sender;
            //Find the buttons position in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);
            //don't do anythind if the cell already has a value
            if (mResults[index] != MarkType.Free)
            {
                return;
            }
            //set the cell value based on which players turn it is
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;

            //set button text to the result
            button.Content = mPlayer1Turn ? "X" : "O";

            //Change noughts to green
            if (!mPlayer1Turn)
                button.Foreground = Brushes.Red;

            //toggle the players turns
            mPlayer1Turn ^= true;

            //Checks for a winner
            CheckForWinner();

        }
        #endregion


        /// <summary>
        /// checks if there is a winner of a 3 line straight
        /// </summary>
        #region Check for winner
        private void CheckForWinner()
        {
            //Chek for horizontal wins

            ///ROW -0
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                //Game ends
                mGameEnded = true;
                // Highlight winning cells in Green
                Button_0_0.Background = Button_1_0.Background = Button_2_0.Background = Brushes.Green;
            }
            ///ROW -1
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                //Game ends
                mGameEnded = true;
                // Highlight winning cells in Green
                Button_0_1.Background = Button_1_1.Background = Button_2_1.Background = Brushes.Green;
            }
            ///ROW -2
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                //Game ends
                mGameEnded = true;
                // Highlight winning cells in Green
                Button_0_2.Background = Button_1_2.Background = Button_2_2.Background = Brushes.Green;
            }


            //Chek for vertical wins

            ///COLOUMN -0
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                //Game ends
                mGameEnded = true;
                // Highlight winning cells in Green
                Button_0_0.Background = Button_0_1.Background = Button_0_2.Background = Brushes.Green;
            }

            ///COLOUMN -1
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                //Game ends
                mGameEnded = true;
                // Highlight winning cells in Green
                Button_1_0.Background = Button_1_1.Background = Button_1_2.Background = Brushes.Green;
            }

            ///COLOUMN -2
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                //Game ends
                mGameEnded = true;
                // Highlight winning cells in Green
                Button_2_0.Background = Button_2_1.Background = Button_2_2.Background = Brushes.Green;
            }

            //Chek for diagonial wins

            ///Left to Right
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                //Game ends
                mGameEnded = true;
                // Highlight winning cells in Green
                Button_0_0.Background = Button_1_1.Background = Button_2_2.Background = Brushes.Green;
            }

            ///Right to Left
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                //Game ends
                mGameEnded = true;
                // Highlight winning cells in Green
                Button_0_2.Background = Button_1_1.Background = Button_2_0.Background = Brushes.Green;
            }


            //check for no winner and full board
            if (!mResults.Any(f => f == MarkType.Free))
            {
                //Game ended
                mGameEnded = true;

                // Turn all cells orange
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Foreground = Brushes.Orange;
                });
            }

            #endregion
        }
    }
}