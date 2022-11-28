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

namespace Assignment5.Model
{
    public class GameManager
    {
        // Array of colors to map the blocks to
        private Brush[] tileColors = new Brush[]
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

        private Random random = new Random();
        // Array of blocks to get a random block for
        private Block[] blocks = new Block[]
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
        public int Score { get; set; }
        private Label[,] _gameGrid;
        
        public GameManager(Label[,] labels, int rows, int colums)
        {
            // Array of the game area
            TetrisGrid = new TetrisGrid(rows, colums);
            // Labels corresponding to the tetris grid
            _gameGrid = labels;
            GameOver = false;
            CurrentBlock = GetRandomBlock();
        }
        public async Task AsyncStartGame()
        {
            Draw();
            while (!GameOver)
            {
                int delay = Math.Max(75, 1000 - (Score * 25));
                await Task.Delay(delay);
                MoveBlockDown();
                Draw();
            }
        }

        public void Draw()
        {
            DrawGrid();
            DrawBlock();
        }

        /// <summary>
        /// Method to draw grid with blocks
        /// </summary>
        private void DrawGrid()
        {            
            for (int r = 0; r < TetrisGrid.Rows; r++)
            {
                for (int c = 0; c < TetrisGrid.Columns; c++)
                {
                    int id = TetrisGrid[r, c];
                    // For each tile, change background to the corresponding color
                    _gameGrid[r, c].Background = tileColors[id];
                }
            }
        }
        /// <summary>
        /// Method to draw block
        /// </summary>
        private void DrawBlock()
        {
            foreach (Position p in CurrentBlock.FormPositions())
            {               
                _gameGrid[p.Row, p.Column].Background = tileColors[CurrentBlock.Id];                
            }
        }
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
        public void MoveBlockLeft()
        {
            CurrentBlock.Move(0, -1);

            if (!BlockFits())
            {
                CurrentBlock.Move(0, 1);
            }
        }

        public void MoveBlockRight()
        {
            CurrentBlock.Move(0, 1);

            if (!BlockFits())
            {
                CurrentBlock.Move(0, -1);
            }
        }

        public void RotateBlockCW()
        {
            CurrentBlock.RotateClockWise();

            if (!BlockFits())
            {
                CurrentBlock.RotateCounterClockWise();
            }
        }

        public void RotateBlockCCW()
        {
            CurrentBlock.RotateCounterClockWise();

            if (!BlockFits())
            {
                CurrentBlock.RotateClockWise();
            }
        }
        #endregion

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
                CurrentBlock = GetRandomBlock();
            }
        }


    }
}
