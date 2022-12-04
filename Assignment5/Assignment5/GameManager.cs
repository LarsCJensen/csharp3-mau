using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Assignment5
{
    public class GameManager
    {
        // Array of colors to map the blocks to
        private readonly Brush[] tileColors = new Brush[]
        {
            Brushes.Transparent,
            Brushes.AliceBlue,
            Brushes.Blue,
            Brushes.Orange,
            Brushes.Yellow,
            Brushes.Green,
            Brushes.Purple,
            Brushes.Red,
        };

        private readonly Random random = new Random();
        // Array of blocks to get a random block for
        private readonly Block[] blocks = new Block[]
        {
            new IBlock(),
            new JBlock(),
            new LBlock(),
            new OBlock(),
            new SBlock(),
            new TBlock(),
            new ZBlock(),
        };
        public TetrisGrid TetrisGrid { get; set; }
        public bool GameOver { get; set; }
        private Block _currentBlock;
        public Block CurrentBlock {
            get => _currentBlock;
            private set
            {
                _currentBlock = value;
                _currentBlock.Reset();

                for (int i = 0; i < 2; i++)
                {
                    _currentBlock.Move(1, 0);

                    if (!BlockFits())
                    {
                        _currentBlock.Move(-1, 0);
                    }
                }
            }
        }
        public Block NextBlock { get; private set; }
        public int Score { get; set; }
        public Label[,] GameGrid { get; private set; }
        public Label[,] NextGrid { get; private set; }

        public GameManager(Label[,] labels, int rows, int colums)
        {
            // Array of the game area
            TetrisGrid = new TetrisGrid(rows, colums);
            // Labels corresponding to the tetris grid
            GameGrid = labels;
            GameOver = false;
            NextBlock = GetRandomBlock();
            CurrentBlock = GetBlock();
            Score = 0;
        }        
        private Block GetBlock()
        {
            Block block = NextBlock;
            do
            {
                NextBlock = GetRandomBlock();
            } while (block.Id == NextBlock.Id); // Don't return same block as current
            return block;            
        }

        /// <summary>
        /// Gets a random block
        /// </summary>
        /// <returns></returns>
        private Block GetRandomBlock()
        {
            return blocks[random.Next(blocks.Length)];            
            
        }
        /// <summary>
        /// Helper method to return if a block fits
        /// </summary>
        /// <returns></returns>
        private bool BlockFits()
        {
            foreach (Position p in CurrentBlock.FormPositions())
            {   
                // If the current position of the form is not empty then return false
                if (!TetrisGrid.IsEmpty(p.Row, p.Column))
                {
                    return false;
                }
            }

            return true;
        }
        /// <summary>
        /// Helper method for game over
        /// </summary>
        /// <returns></returns>
        private bool IsGameOver()
        {
            // If not the two top rows are empty then game is not over
            return !(TetrisGrid.IsRowEmpty(0) && TetrisGrid.IsRowEmpty(1));
        }       
        #region Block movement
        /// <summary>
        /// Helper method to move block down
        /// </summary>
        public void MoveBlockDown()
        {
            CurrentBlock.Move(1, 0);
            if (!BlockFits())
            {
                CurrentBlock.Move(-1, 0);
                PlaceBlock();
            }
        }
        /// <summary>
        /// Helper method to move block left
        /// </summary>
        public void MoveBlockLeft()
        {
            CurrentBlock.Move(0, -1);

            if (!BlockFits())
            {
                CurrentBlock.Move(0, 1);
            }
        }
        /// <summary>
        /// Helper method to move block right
        /// </summary>
        public void MoveBlockRight()
        {
            CurrentBlock.Move(0, 1);

            if (!BlockFits())
            {
                CurrentBlock.Move(0, -1);
            }
        }
        /// <summary>
        /// Helper method to rotate block
        /// </summary>
        public void RotateBlockCW()
        {
            CurrentBlock.RotateClockWise();

            if (!BlockFits())
            {
                CurrentBlock.RotateCounterClockWise();
            }
        }
        /// <summary>
        /// Helper method to rotate block
        /// </summary>
        public void RotateBlockCCW()
        {
            CurrentBlock.RotateCounterClockWise();

            if (!BlockFits())
            {
                CurrentBlock.RotateClockWise();
            }
        }
        #endregion
        /// <summary>
        /// Helper method to place block when it has reached a stop
        /// </summary>
        private void PlaceBlock()
        {
            foreach (Position p in CurrentBlock.FormPositions())
            {
                TetrisGrid[p.Row, p.Column] = CurrentBlock.Id;
            }

            Score += TetrisGrid.ClearFullRows();

            if (IsGameOver())
            {
                GameOver = true;
            }
            else
            {
                CurrentBlock = GetBlock();
            }
        }
    }
}
