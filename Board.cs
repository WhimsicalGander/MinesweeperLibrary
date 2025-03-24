using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperLibrary
{
    public class Board
    {

        public int size { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public Cell[,] cells { get; set; }
        public int difficulty { get; set; }
        public int rewardsHeld { get; set; }

        public int[] bombPositions { get; set; }
        public enum gameProgress { won, lost, inProgress }

        Random rand = new Random();

        public Board()
        {
        }

        public Board(int size, DateTime start, int difficulty)
        {
            this.size = size;
            this.start = start;
            this.cells = new Cell[size,size];
            this.difficulty = difficulty;
            this.rewardsHeld = 0;
            gameProgress status = gameProgress.inProgress;
            InitializeBoard();
        }

        //sets up whole board
        public void InitializeBoard()
        {

            setupBombs();
            setupCells(bombPositions);
            numOfBombNeighbors();
        }

        //sets up bombs into a double array
        public void setupBombs()
        {
            int bombs = difficulty * 10;

            bombPositions = new int[bombs];

            for (int i = 0; i < bombs; i++)
            {
                bombPositions[i] = randomNumber(bombPositions);
            }

        }

        //creates random numbers that do not repeat
        public int randomNumber(int[] bombPositions)
        {
            int tempRand = rand.Next(0, (size * size) - 1);

            
            for (int i = 0;i < bombPositions.Length; i++)
            {
                if (tempRand == bombPositions[i])
                {
                    tempRand = randomNumber(bombPositions);
                }
            }
            

            return tempRand;
        }

        /// <summary>
        /// initializes all cells
        /// </summary>
        /// <param name="bombPositions"></param>
        public void setupCells(int[] bombPositions)
        {
            int counter = 0;
            for (int i = 0; i< size;i++) 
            {
                 for(int j = 0; j < size; j++)
                    {
                        cells[i,j] = new Cell(counter, bombPositions);
                        counter++;
                    }
            }
        }


        //returns number of bomb neighbors of the cell
        public void numOfBombNeighbors()
        {
            //double loop iterates through all cells
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {


                    //if current cell is bomb make the count nine
                    if (cells[i, j].isBomb)
                    {
                        cells[i, j].numOfBombNeighbors = 9;
                    }
                    else
                    {
                        for (int x = -1; x < 2; x++)
                        {
                            for (int y = -1; y < 2; y++)
                            {
                                if (((i + x) >= 0) && ((i + x) < size) && ((j + y) >= 0) && ((j + y) < size))
                                {

                                    if (cells[i + x, j + y].isBomb)
                                    {
                                        cells[i, j].numOfBombNeighbors++;
                                    }



                                }
                            }
                        }
                    }
                }
            }
        }
        //concatenates and formats the cell numbers and Bs into a string array
        public string[] makePrintable()
        {
            string[] printableBoard = new string[size];

            for (int i = 0;i < size; i++)
            {
                int c = i + 1;
                string tempString = "" + c;

                for (int j = 0;j < size; j++)
                {
                    if ((cells[i, j].numOfBombNeighbors > 0) && (cells[i, j].numOfBombNeighbors < 9))
                    {
                        
                        tempString += " | " + cells[i, j].numOfBombNeighbors;
                    }
                    if (cells[i, j].numOfBombNeighbors == 9)
                    {
                       
                        tempString += " | B";
                    }
                    if (cells[i, j].numOfBombNeighbors == 0)
                    {
                       
                        tempString += " | .";
                    }
                }
                tempString += " | ";

                printableBoard[i] = tempString;
            }


            return printableBoard;
        }





}
}
