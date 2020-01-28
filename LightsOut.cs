using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightsOut
{
    public partial class LightsOut : Form
    {
        // Variables that are needed for the grid, 2d array, buttons, colors, and the count for the amount of lights that are turned on.
        private LightsOutButton[,] LightButtons = new LightsOutButton[row,col];
        private static int row = 5;
        private static int col = 5;
        Color[] lightColor = new Color[2] { Color.White, Color.Blue };
        Random colors = new Random();
        private int LightsOn = 0;
        public LightsOut()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
        }
        /// <summary>
        /// Method that is used to instantiate the buttons and start the game.
        /// </summary>
        private void StartGame()
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    // Adding buttons to a 2d array.
                    LightButtons[i, j] = new LightsOutButton
                    {
                        // Setting the size and color of the buttons.
                        BackColor = lightColor[colors.Next(2)],
                        Size = new Size(40, 40)
                    };
                    LightButtons[i, j].Enabled = true;
                    // Adding on to the counter to indicate how many lights are turned on.
                    if (LightButtons[i, j].BackColor == Color.Blue) LightsOn++;
                    // setting the location for the buttons with spaces inbetween.
                    LightButtons[i, j].Location = new Point(20 + (40 * i), 20 + (40 * j));
                    LightButtons[i, j].xLoc = i;
                    LightButtons[i, j].yLoc = j;
                    LightButtons[i, j].TabIndex = i + j + 1;
                    LightButtons[i, j].Click += Check_Lights;
                    flowLayoutPanel1.Controls.Add(LightButtons[i, j]);
                }
            }
            // Setting the text to show how many lights are on and refreshing it to be displayed.
            OnLights.Text = "Lights On: " + LightsOn;
            OnLights.Refresh();
        }
        /// <summary>
        /// Method used to change the lights color.
        /// </summary>
        /// <param name="button"></param>
        private void ChooseColor(LightsOutButton button)
        {
            if (button.BackColor == Color.Blue)
            { 
                button.BackColor = Color.White;
            }
            else
            {
                button.BackColor = Color.Blue;
            }
        }
        /// <summary>
        /// Method used to call the StartGame() Method and prevent certain buttons being clicked.
        /// </summary>
        private void NewGame_Click(object sender, EventArgs e)
        {
            StartGame();
            NewGame.Enabled = false;
            Restart.Enabled = true;
        }
        /// <summary>
        /// Method used to swap the colors of the lights, and check to see how many lights are left to turn off.
        /// </summary>
        private void Check_Lights(object sender, EventArgs e)
        {
            LightsOutButton buttonclicked = (LightsOutButton)sender;
            ChooseColor(buttonclicked);
            int ButtonXloc = (buttonclicked.xLoc);
            int ButtonYloc = (buttonclicked.yLoc);
            //Checks and changes each button around the clicked button. (left side, right side, on top and below)
            if (ButtonXloc > 0)
            {
                ChooseColor(LightButtons[ButtonXloc - 1, ButtonYloc]);
            }
            if (ButtonXloc < (row - 1))
            {
                ChooseColor(LightButtons[ButtonXloc + 1, ButtonYloc]);
            }
            if (ButtonYloc > 0)
            {
                ChooseColor(LightButtons[ButtonXloc, ButtonYloc - 1]);
            }
            if (ButtonYloc < (col - 1))
            {
                ChooseColor(LightButtons[ButtonXloc, ButtonYloc + 1]);
            }
            // Disables the buttons from being clicked when game is finished and displays all lights have been turned off.
            if (GameFinished())
            {
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < row; j++)
                    {
                        LightButtons[i, j].Enabled = false;
                    }
                }
                MessageBox.Show("All Light Have Been Turned Off");
                NewGame.Enabled = false;
                Restart.Enabled = true;
            }
            // checks all the colors to give a new amount for the LightsOn counter.
            LightsOn = 1;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    if (LightButtons[i, j].BackColor == Color.Blue) LightsOn++;
                }
            }
            LightsOn--;
            OnLights.Text = "Lights On: " + LightsOn;
            OnLights.Refresh();
        }
        /// <summary>
        /// Exits the application.
        /// </summary>
        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        /// <summary>
        /// Boolean method to check if all the lights are turned off.
        /// </summary>
        /// <returns></returns>
        private bool GameFinished()
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    if (LightButtons[i, j].BackColor == Color.Blue)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// Method to restart the game and reset all the lights.
        /// </summary>
        private void Restart_Click(object sender, EventArgs e)
        {
            LightsOn = 0;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    // Enables the buttons to be clicked again.
                    LightButtons[i, j].Enabled = true;
                    LightButtons[i, j].BackColor = lightColor[colors.Next(2)];
                    if (LightButtons[i, j].BackColor == Color.Blue) LightsOn++;
                    OnLights.Text = "Lights On: " + LightsOn;
                    OnLights.Refresh();
                }
            }
            
        }
    }
}
