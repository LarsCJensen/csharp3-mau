using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace Assignment5
{
    public class TetrisGrid
    {
        /// <summary>
        /// Class which holds the game grid 
        /// </summary>
        
        // Two-dimensional array of ints
        private readonly int[,] tetrisGrid;
        public int Rows { get;  }
        public int Columns { get; }
        // Indexer to let the consumer more easily get/set values from the grid class
        public int this[int row, int column]
        {
            get => tetrisGrid[row, column];
            set => tetrisGrid[row, column] = value;
        }
        public TetrisGrid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            tetrisGrid = new int[Rows, Columns];
        }
        /// <summary>
        /// Check if block is inside the tetrisGrid
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns>True/False</returns>
        public bool IsInside(int row, int column)
        {
            return row >= 0 && row < Rows && column >= 0 && column < Columns;
        }
        /// <summary>
        /// Check if square is empty
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public bool IsEmpty(int row, int column)
        {
            return IsInside(row, column) && tetrisGrid[row, column] == 0;
        }
        /// <summary>
        /// Helper method to check if row is empty
        /// </summary>
        /// <param name="row">Row to check</param>
        /// <returns></returns>
        public bool IsRowEmpty(int row)
        {
            for (int col = 0; col < Columns; col++)
            {
                if (tetrisGrid[row, col] != 0)
                {
                    return false;
                }
            }

            return true;
        }
        /// <summary>
        /// Check if row is full
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public bool IsRowFull(int row)
        {
            for (int col = 0; col < Columns; col++)
            {
                if (tetrisGrid[row, col] == 0)
                {
                    return false;
                }
            }

            return true;
        }
        /// <summary>
        /// Clear row
        /// </summary>
        /// <param name="row">Which row to empty</param>
        private void ClearRow(int row)
        {
            for (int col = 0; col < Columns; col++)
            {
                tetrisGrid[row, col] = 0;
            }
        }
        /// <summary>
        /// Move row down number of rows that are cleared
        /// </summary>
        /// <param name="row">Row</param>
        /// <param name="numRows">How many to remove</param>
        private void MoveRowDown(int row, int numRows)
        {
            for (int col = 0; col < Columns; col++)
            {
                tetrisGrid[row + numRows, col] = tetrisGrid[row, col];
                tetrisGrid[row, col] = 0;
            }
        }
        /// <summary>
        /// If row is full, clear it and then move other row down
        /// </summary>
        /// <returns></returns>
        public int ClearFullRows()
        {
            int cleared = 0;

            for (int row = Rows - 1; row >= 0; row--)
            {
                if (IsRowFull(row))
                {
                    ClearRow(row);
                    cleared++;
                }
                else if (cleared > 0)
                {
                    MoveRowDown(row, cleared);
                }
            }

            return cleared;
        }        
    }
}
