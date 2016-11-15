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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Battleship
    {
        protected int[,] board = new int[10, 10];
        protected int turn = 1;

        public Battleship()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    board[i, j] = 0;
                }
            }
        }

        //This function clears the board
        public void clearBoard()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    board[i, j] = 0;
                }
            }
        }

        //---------------------------------------------------
        //This function makes a move
        //It takes an x and y coordinate as parameter
        //It returns true if the move was made successfully
        //and false if it was not.
        //---------------------------------------------------
        public bool makeMove(int x, int y)
        {
            //if the spot has not been hit
            if (board[x, y] != 1 && board[x, y] != 10)
            {
                if (isHit(x, y) == true)
                {
                    board[x, y] = 10;
                }
                else
                    board[x, y] = 1;

                return true;
            }
            else
                return false;
        }

        //---------------------------------------------------
        //This function determines if the move that was made
        //hit a ship.
        //It takes an x and y coordinate as a parameter
        //It returns true if a ship was hit and false
        //if a ship was not hit. 
        //---------------------------------------------------
        public bool isHit(int x, int y)
        {
            if(board[x, y] >= 2 && board[x, y] <= 5)
            {
                return true;
            }

            if(board[x, y] == 10)
            {
                return true;
            }

            return false;
        }

        //---------------------------------------------------
        //This function places the ships on the grid. 
        //It takes in the size of the ship, the direction of
        //the ship (vertical or horizontal), and an x and y
        //coordinate as parameters.
        //It returns true if the ship was placed successfully
        //and false if it was not.
        //---------------------------------------------------
        public bool placeShip(int shipSize, int direction, int x, int y)
        {
            int tempX = x;
            int tempY = y;

            //places the ship vertically
            if (direction == 0)
            {
                if (tempX > (10 - shipSize))
                {
                    return false;
                }

                for (int i=0; i< shipSize; i++)
                {
                    if(board[tempX, y] != 0)
                    {
                        return false;
                    }
                    tempX++;
                }

                tempX = x;

                for (int i = 0; i < shipSize; i++)
                {
                    board[tempX, y] = shipSize;
                    tempX++;
                }

                return true;
            }

            //places the ship horizontally
            if (direction == 1)
            {
                if (tempY > (10 - shipSize))
                {
                    return false;
                }

                for (int i = 0; i < shipSize; i++)
                {
                    if (board[x, tempY] != 0)
                    {
                        return false;
                    }
                    tempY++;
                }

                tempY = y;

                for (int i = 0; i < shipSize; i++)
                {
                    board[x, tempY] = shipSize;
                    tempY++;
                }

                return true;
            }

            return false;

        }

        //---------------------------------------------------
        //This function checks to see if a particular ship
        //has been destroyed.
        //It takes in which ship it is as a parameter.
        //It returns true if the ship is destroyed and false
        //if it is not.
        //---------------------------------------------------
        public bool shipCheck(int ship)
        {
            int count = 0; //counts the amount of time a 2 (which represents the destroyer ship) appears on the board. 

            for(int i=0; i<10; i++)
            {
                for(int j=0; j<10; j++)
                {
                    if(board[i, j] == ship)
                    {
                        count++;
                    }
                }
            }

            if(count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //---------------------------------------------------
        //This function returns the value that is at a certain
        //spot on the grid.
        //It takes in an x and y coordinate as parameters.
        //It returns an integer that is the value of what is
        //at that point in the grid.
        //---------------------------------------------------
        public int returnVal(int x, int y)
        {
            return board[x, y];
        }

        //---------------------------------------------------
        //This function checks to see if someone won.
        //No parameters.
        //It returns true if there is a winner and false if 
        //there is not a winner.
        //---------------------------------------------------
        public bool checkBoard()
        {
            int count = 0;

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (board[i, j] == 10)
                    {
                        count++;
                    }
                }
            }

            if (count == 14)
            {
                return true;
            }
            else
                return false;
        }

    } // end Battleship class

}

