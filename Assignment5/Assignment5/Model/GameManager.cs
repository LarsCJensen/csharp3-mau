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
        private readonly Brush[] tileColors = new Brush[]
        {
            Brushes.Transparent,
            Brushes.Yellow,
            Brushes.Green,
        };
        public TetrisGrid TetrisGrid { get; set; }
        public bool GameOver { get; set; }
        public Block CurrentBlock { get; private set; }
        public int Score { get; set; }
        private Label[,] _gameGrid;
        // TODO        
        // Rotate
        // Move
        // Is in ok position
        public GameManager(Label[,] labels, int rows, int colums)
        {
            TetrisGrid = new TetrisGrid(rows, colums);
            _gameGrid = labels;
            GameOver = false;
            CurrentBlock = GetRandomBlock();
        }
        public async Task StartGame()
        {
            Draw();
            while (!GameOver)
            {
                Draw();
                int delay = Math.Max(75, 1000 - (Score * 25));
                await Task.Delay(delay);
                MoveBlockDown();
            }
        }

        public void Draw()
        {
            DrawGrid();
            DrawBlock();
        }

        // TODO Is this to be used?
        private void DrawGrid()
        {
            for (int r = 0; r < TetrisGrid.Rows; r++)
            {
                for (int c = 0; c < TetrisGrid.Columns; c++)
                {
                    int id = TetrisGrid[r, c];
                    //Label lbl = GameGrid.Children.Cast<Label>()
                    //    .First(e => Grid.GetRow(e) == r && Grid.GetColumn(e) == c);
                    //lbl.Background = tileColors[id];                    
                    _gameGrid[r, c].Background = tileColors[id];
                }
            }
        }
        private void DrawBlock()
        {
            foreach (Position p in CurrentBlock.FormPositions())
            {
                
                //Label lbl = GameGrid.Children.Cast<Label>()
                //    .First(e_gameGrid[r, c].Background => Grid.GetRow(e) == p.Row && Grid.GetColumn(e) == p.Column);
                _gameGrid[p.Row, p.Column].Background = tileColors[CurrentBlock.Id];
                
            }
        }
        public void MoveBlockDown()
        {
            CurrentBlock.Move(1, 0);            
        }

        private Block GetRandomBlock()
        {
            return new IBlock();
        }

            
    }
}
