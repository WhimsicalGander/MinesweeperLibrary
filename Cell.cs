using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperLibrary
{
    public class Cell
    {
        public int singleNum {  get; set; }
        public bool isVisited { get; set; }
        public bool isBomb { get; set; }
        public bool isFlagged { get; set; }
        public int numOfBombNeighbors { get; set; }
        public bool hasSpecialReward { get; set; }

        //non default constructor
        public Cell( int singleNum, int[]bombPositions) 
        {
            this.singleNum = singleNum;
            this.isVisited = false;
            this.isFlagged = false;
            this.isBomb = false;
            setBomb(bombPositions);


        }

        public Cell()
        {
        }

        //if there is a bomb in the position that the cell is numbered, isBomb is set to true
        public void setBomb(int[]bombPositions)
        {
            for (int i = 0; i < bombPositions.Length; i++)
            { 
                if( bombPositions[i] == singleNum)
                {
                    isBomb = true;
                }
            }


        }
    }
}
