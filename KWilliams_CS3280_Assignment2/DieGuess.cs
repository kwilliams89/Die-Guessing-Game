using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace KWilliams_CS3280_Assignment2
{
    public partial class DieGuess : Form
    {

        #region Variables

        private Random rnd;   // random Object for random number generator
        private int guess;      // store user guess
        private int dieRolled;  // store simulated die value
        private int tPlayed;    // store times played
        private int tWon;       // store times won
        private int tLost;      // store times lost
        private int[] guessCounter;     //array to hold guess counter for each value 1-6
        private int[] rollCounter;      //array to hold roll counter for each value 1-6
        string[] arr;  // declare and initialize arr for adding items to listview
        ListViewItem item;


        #endregion

        #region Guess Methods
        /// <summary>
        /// Constructor initalizes form and sets default values.
        /// </summary>
        public DieGuess()
        {
            InitializeComponent();
            rnd = new Random();  // initialize random object
            guessCounter = new int[6] { 0, 0, 0, 0, 0, 0 };
            rollCounter = new int[6] { 0, 0, 0, 0, 0, 0 };
            arr = new string[4];

            //initialize statistics and displays for statistics to initial values
            tPlayed = 0;
            tWon = 0;
            tLost = 0;
            label4.Text = tPlayed.ToString();
            label5.Text = tWon.ToString();
            label6.Text = tLost.ToString();

            listView1.View = View.Details;
            listView1.FullRowSelect = true;

            listView1.Columns.Add("FACE", 50);
            listView1.Columns.Add("FREQUENCY", 100);
            listView1.Columns.Add("PERCENT", 100);            
            listView1.Columns.Add("NUMBER OF TIMES GUESSED", 195);

            for (int i = 0; i < 6; i++)             //for loop to add/update items in listview       
            {
                arr[0] = (i+1).ToString();
                arr[1] = 0.ToString();
                arr[2] = 0.ToString();
                arr[3] = 0.ToString();
                item = new ListViewItem(arr);
                item.Name = (i + 1).ToString();
                listView1.Items.Add(item);

            }

        }
        
        /// <summary>
        /// Method simulates die roll.
        /// </summary>
        private void simulateRoll()
        {
            // simulate die roll
            for (int i = 1; i < 7; i++) 
            {
                dieRolled = rnd.Next(1, 7); // generate random die number
                pbImage.Image = Image.FromFile("die" + dieRolled.ToString() + ".gif");  //display die image
                pbImage.Refresh();
                Thread.Sleep(300);
            }
            rollCounter[dieRolled - 1]++;

        }
        /// <summary>
        /// Retrieves user guess from textbox. Idea adapted from answer on stackoverflow from T.Rob. Link to answer - http://stackoverflow.com/a/8199613
        /// </summary>
        private void retrieveGuess()
        {

            int number;
            bool isNumber;
            isNumber = Int32.TryParse(txtGuess.Text, out number);

            if (!isNumber)
            {
                guess = 0; // set guess to out of bounds value to force new input when input was invalid
            }
            else
            {
                guess = Convert.ToInt32(txtGuess.Text); // converts textbox value to int and sets guess equal to the value
            }

        }
        /// <summary>
        /// Compares user guess to simulate die value. Updates game counters accordingly.
        /// </summary>
        /// <param name="roll"></param>
        /// <param name="guess"></param>
        private void compareGuess(int roll, int guess)
        {
            tPlayed++;  // increment tPlayed for valid guess
            guessCounter[guess - 1]++;
            if (guess == dieRolled)  // compare user guess to die value.
            {
                tWon++;  // increment tWon for correct guess
                lblMessage.Text = "You guessed correctly!";
            }
            else
            {
                tLost++;  // increment tLost for incorrect guess
                lblMessage.Text = "You guessed wrong!";
            }
        }
        /// <summary>
        /// Method updates game statistics.
        /// </summary>
        private void updateStats()
        {
            label4.Text = tPlayed.ToString();  // Display updated times played.
            label5.Text = tWon.ToString();     // Display updated times won.
            label6.Text = tLost.ToString();     //Display updated times lost.


            for (int i = 0; i < 6; i++)             //for loop to add/update items in listview       
            {
                listView1.Items[i].SubItems[1].Text = rollCounter[i].ToString();   //update frequency column
                double percentage = (double)rollCounter[i] / (double)tPlayed * 100;               //calculate frequency percentage
                percentage = Math.Round(percentage, 2); //round to two decimal places
                listView1.Items[i].SubItems[2].Text = percentage.ToString() + "%";        //update percentage column
                listView1.Items[i].SubItems[3].Text = guessCounter[i].ToString();   //update guess column
            }
            txtGuess.Text = "";
        }

        #endregion

        #region Form Methods
        /// <summary>
        /// Method initiates individual game round via roll button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRoll_Click(object sender, EventArgs e)
        {
            retrieveGuess();

            if (guess > 0 && guess < 7)
            {
                simulateRoll();
                compareGuess(dieRolled, guess);
                updateStats();
            }
            else
            {
                lblMessage.Text = "Invalid Input. Please enter a value between 1-6.";
            }

        }

        /// <summary>
        /// Method resets game board. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            Application.Restart(); // refresh game board
        }

        #endregion
    }
}
