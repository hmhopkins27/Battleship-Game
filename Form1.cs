//---------------------------------------------------
//Hannah Hopkins, Josh Kent, and Todd Gibson
//Project 5
//Battleship
//28 April 2016
//This program implements the game battleship.
//Key:
//0=not been hit, no ship there
//1=hit, no ship there
//2=not hit, destroyer ship there
//3=not hit, submarine ship there
//4=not hit, battleship there
//5=not hit, carrier ship there
//10=hit, ship there
//---------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleship
{
    public partial class Form1 : Form
    {
        private Button[,] grid = new Button[10, 10];
        private Button[,] grid2 = new Button[10, 10];
        private Battleship b1 = new Battleship();
        private Battleship b2 = new Battleship();
        private int x = 0;
        private int y = 0;
        private int phase = 0;
        private int direction = 0;
        private int shipCount = 2;

        public Form1()
        {
            InitializeComponent();
            init();
            setupBoardComputer();
        }

        //------------------------------------
        //This function initializes the board
        //-----------------------------------
        public void init()
        {
            int placement1 = 0;
            int placement2 = 80;
            int placement3 = 600;
            int placement4 = 80;
            char row = 'A';

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    grid[i, j] = new Button();
                    grid2[i, j] = new Button();
                    grid2[i, j].Enabled = false;
                    grid[i, j].Width = 40;
                    grid2[i, j].Width = 40;
                    grid[i, j].Height = 40;
                    grid2[i, j].Height = 40;

                    grid[i, j].Left = placement1;
                    grid2[i, j].Left = placement3;
                    grid[i, j].Top = placement2;
                    grid2[i, j].Top = placement4;
                    placement1 = placement1 + 40;
                    placement3 = placement3 + 40;

                    if (placement1 == 400)
                    {
                        placement1 = 0;
                        placement2 = placement2 + 40;
                    }

                    if (placement3 == 1000)
                    {
                        placement3 = 600;
                        placement4 = placement4 + 40;
                    }

                    grid[i, j].BackColor = Color.WhiteSmoke;
                    grid2[i, j].BackColor = Color.WhiteSmoke;
                    grid[i, j].Tag = 0;
                    grid2[i, j].Tag = 0;
                    this.Controls.Add(grid[i, j]);
                    this.Controls.Add(grid2[i, j]);
                    grid[i, j].Name = row.ToString() + (j);
                    grid2[i, j].Name = row.ToString() + (j);
                    grid[i, j].Click += new EventHandler(buttonClick);
                    grid2[i, j].Click += new EventHandler(buttonClick);
                }

                row++;
            }

            button1.Text = " ";
            button3.Text = " ";
            button4.Text = " ";
            button5.Text = " ";
            button6.Text = " ";

            button1.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            label1.Visible = false;
            label3.Visible = false;
            pictureBox1.Visible = false;
            label4.Visible = false;
            label5.Visible = false;

        }

        public void buttonClick(object sender, EventArgs eArgs)
        {
            Button clickedButton = sender as Button;

            convertToCoord(clickedButton);

            if (phase == 0)
            {
                if (shipCount < 6)
                {
                    if (b2.placeShip(shipCount, direction, x, y) == true)
                    {
                        shipCount++;
                    }

                    switch (shipCount)
                    {
                        case 2:
                            button7.Text = "Place destroyer ship.(2)";
                            break;
                        case 3:
                            button7.Text = "Place submarine.\n(3)";
                            break;
                        case 4:
                            button7.Text = "Place battleship.\n(4)";
                            break;
                        case 5:
                            button7.Text = "Place carrier ship.\n(5)";
                            break;
                    }
                }

                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (b2.returnVal(i, j) != 0)
                        {
                            grid[i, j].BackColor = Color.Red;
                        }
                    }
                }
            }

            if (phase == 1)
            {
                button1.Text = " ";

                if (b1.checkBoard() == false)
                {
                    if (b1.makeMove(x, y) == true)
                    {
                        if (b1.isHit(x, y) == true)
                        {
                            clickedButton.BackColor = Color.Red;
                        }
                        else
                        {
                            clickedButton.BackColor = Color.Green;
                        }

                        button1.Text = " ";

                        if (b1.shipCheck(2) == true)
                        {
                            button3.Text = "Destroyer ship has been destroyed.";
                        }

                        if (b1.shipCheck(3) == true)
                        {
                            button4.Text = "Submarine has been destroyed.";
                        }

                        if (b1.shipCheck(4) == true)
                        {
                            button5.Text = "Battleship has been destroyed.";
                        }

                        if (b1.shipCheck(5) == true)
                        {
                            button6.Text = "Carrier ship has been destroyed.";
                        }

                        makeMoveComp();
                    }

                    else
                    {
                        button1.Text = "Area has already been hit. Please pick another location.";
                    }
                }

                if (b1.checkBoard() == true)
                {
                    pictureBox1.Visible = true;
                    label4.Visible = true;
                }

            }
        }

        //------------------------------------
        //This function makes a move for the computer
        //by using a random x and y value
        //-----------------------------------
        public void makeMoveComp()
        {
            Random ran = new Random();
            int x2 = ran.Next(0, 10);
            int y2 = ran.Next(0, 10);

            while(b2.returnVal(x2, y2) == 1 || b2.returnVal(x2, y2) == 10)
            {
                x2 = ran.Next(0, 10);
                y2 = ran.Next(0, 10);
            }


            if (b2.checkBoard() == false)
            {
                if (b2.makeMove(x2, y2) == true)
                {
                    if (b2.isHit(x2, y2) == true)
                    {
                        grid2[x2, y2].BackColor = Color.Black;
                    }
                    else
                    {
                        grid2[x2, y2].BackColor = Color.LightGreen;
                    }
                }

                if (b2.shipCheck(2) == true)
                {
                    button1.Text = "Your destroyer ship has been destroyed!";
                }

                if (b2.shipCheck(3) == true)
                {
                    button1.Text = "Your submarine has been destroyed!";
                }

                if (b2.shipCheck(4) == true)
                {
                    button1.Text = "Your battleship has been destroyed!";
                }

                if (b2.shipCheck(5) == true)
                {
                    button1.Text = "Your carrier ship has been destroyed!";
                }
           }

           else
           {
                pictureBox1.Visible = true;
                label5.Visible = true;
           }
    }

        //------------------------------------
        //This function converts the name of 
        //the button to an x and y coordinate
        //-----------------------------------
        public void convertToCoord(Button buttonNum)
        {
            string first = " ";

            first = buttonNum.Name;

            switch (first[0])
            {
                case 'A':
                    x = 0;
                    break;
                case 'B':
                    x = 1;
                    break;
                case 'C':
                    x = 2;
                    break;
                case 'D':
                    x = 3;
                    break;
                case 'E':
                    x = 4;
                    break;
                case 'F':
                    x = 5;
                    break;
                case 'G':
                    x = 6;
                    break;
                case 'H':
                    x = 7;
                    break;
                case 'I':
                    x = 8;
                    break;
                case 'J':
                    x = 9;
                    break;
                default:
                    break;
            }

            if (first.Length == 3)
            {
                y = 10;
            }
            else
            {
                int.TryParse(first[1].ToString(), out y);
            }
        }

        //------------------------------------
        //This function places the ships randomly
        //for the computer.
        //-----------------------------------
        private void setupBoardComputer()
        {
            Random random = new Random();

            int direction = 0;
            int count = 2; //starts with ship 2 and ends with ship 5
            while (count < 6)
            {
                direction = random.Next(0, 2); //this will randomly decide what direction the ship will face

                switch (direction)
                {
                    //direction 0 = vertical
                    case 0:
                        if(b1.placeShip(count, 0, random.Next(0, (10 - count)), random.Next(0, 10))== true)
                        {
                            count++;
                        }                       
                        break;
                    //direction 1 = horizontal
                    case 1:
                        if(b1.placeShip(count, 1, random.Next(0, 10), random.Next(0, (10 - count))) == true)
                        {
                            count++;
                        }                       
                        break;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        //changes the direction to vertical
        private void button8_Click(object sender, EventArgs e)
        {
            direction = 0;
        }

        //changes the direction to horizontal
        private void button9_Click(object sender, EventArgs e)
        {
            direction = 1;
        }

        //the finish placing ships button
        private void button10_Click(object sender, EventArgs e)
        {
            phase = 1;

            label1.Visible = true;
            label2.Visible = false;
            label3.Visible = true;

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (b2.returnVal(i, j) != 0)
                    {
                        grid2[i, j].BackColor = Color.Red;
                    }
                }
            }

            for (int i=0; i<10; i++)
            {
                for(int j=0; j<10; j++)
                {
                    grid[i, j].BackColor = Color.WhiteSmoke;
                }
            }

            button1.Visible = true;
            button3.Visible = true;
            button4.Visible = true;
            button5.Visible = true;
            button6.Visible = true;

            button7.Visible = false;
            button8.Visible = false;
            button9.Visible = false;
            button10.Visible = false;

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
