using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace Assignment5.Model
{
    public class TetrisGrid
    {
        /// <summary>
        /// Class which holds the game grid 
        /// </summary>
        
        // Two-dimensional array
        private readonly int[,] testrisGrid;
        public int Rows { get;  }
        public int Columns { get; }
        static private Brush NoBrush = Brushes.Transparent; // For empty labels
        static private Brush SilverBrush = Brushes.Gray; // For borders
        // Indexer to let the consumer more easily get/set values from the grid class
        public int this[int row, int column]
        {
            get => testrisGrid[row, column];
            set => testrisGrid[row, column] = value;
        }
        // TODO Use in MainViewModel
        public TetrisGrid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            testrisGrid = new int[Rows, Columns];
            // TODO REMOVE
            //BlockControls = new Label[Columns, Rows];
            //for (int i = 0; i < Columns; i++)
            //{
            //    for (int j = 0; j < Rows; j++)
            //    {
            //        BlockControls[i, j] = new Label();
            //        BlockControls[i, j].Background = NoBrush;
            //        BlockControls[i, j].BorderBrush = SilverBrush;
            //        BlockControls[i, j].BorderThickness = new Thickness(1, 1, 1, 1);
            //        Grid.SetRow(BlockControls[i, j], j);
            //        Grid.SetColumn(BlockControls[i, j], i);
            //        TetrisGrid.Children.Add(BlockControls[i, j]);
            //    }
            //}
        }
        // HELPER METHODS NEEDED
    }
}
