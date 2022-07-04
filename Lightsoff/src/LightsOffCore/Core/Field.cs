using System;
using System.Collections.Generic;
using System.Text;
using LightsOff.Core;

namespace LightsOff.Core
{
    [Serializable]
    public class Field
    {
        public Tile[,] tiles;

        private DateTime startTime;

        public int RowCount { get; }

        public int ColumnCount { get; }


        public Field(int rowCount, int columnCount)
        {
            RowCount = rowCount;
            ColumnCount = columnCount;
            tiles = new Tile[rowCount, columnCount];
            Generate();
            Map(4);
            startTime = DateTime.Now;
        }


        public Tile GetTile(int row, int column)
        {
            return tiles[row, column];
        }


        public void Generate()
        {
            for (var row = 0; row < RowCount; row++)
            {
                for (var column = 0; column < ColumnCount; column++)
                {
                    tiles[row, column] = new Tile(false);
                    /*
                    Random r = new Random();
                    int rInt = r.Next(0, 2);
                    if (rInt == 1)
                    {
                        tiles[row, column] = new Tile(false);
                    }
                    else {
                        tiles[row, column] = new Tile(false);
                    }*/
                }
            }
        }
        public void Map(int difficulty) {
            Random r = new Random();
            //for (var i = 0; i < 2; i++)
            //{
            for (var column = 0; column < difficulty; column++)
                {
                int rInt = r.Next(ColumnCount);
                InvertHandler(tiles[column, rInt], column, rInt);
            //    }
            }
        }

        public void Play(int row_s,int column_s ) {
                //int rows = 0;
                //int columns = 0;

                int rows = Convert.ToInt32(row_s);

                int columns = Convert.ToInt32(column_s);


                InvertHandler(tiles[rows, columns], rows, columns);
        }

        public void InvertHandler(object sender, int row, int column)
        {
            InvertButton(tiles[row, column], row, column);

            if (row > 0)
            {
                InvertButton(tiles[row-1, column], row - 1, column);
            }
            if (row < (tiles.GetLength(1) - 1))
            {
                InvertButton(tiles[row + 1, column], row + 1, column); 
            }
            if (column > 0)
            {
                InvertButton(tiles[row, column - 1], row, column - 1); 
            }
            if (column < (tiles.GetLength(1) - 1))
            {
                InvertButton(tiles[row, column + 1], row, column + 1); 
            }

        }

        public void InvertButton(object sender, int row, int column)
        {


            if (tiles[row, column].Value == true)
            {
                tiles[row, column].Value = false;
            }
            else
            {
                tiles[row, column].Value = true;
            }
            
        }

        public bool IsSolved(){
            for (var row = 0; row < RowCount; row++)
            {
                for (var column = 0; column < ColumnCount; column++)
                {
                    if (GetTile(row,column).Value == true)
                    {
                        return false;
                    }
                    
                }
            }
            return true;
        }


        public int GetScore(int moves)
        {
            return 3000 - moves;
        }


    }

}
