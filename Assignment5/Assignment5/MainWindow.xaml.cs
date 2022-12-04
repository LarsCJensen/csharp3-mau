using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Assignment5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameManager _player1GameManager;
        private GameManager _player2GameManager;
        Grid _player1Grid = new Grid();        
        Grid _player2Grid = new Grid();
        // TODO Future
        //Grid _player1NextGrid = new Grid();
        //Grid _player2NextGrid = new Grid();
        private int _rows = 22;
        private int _columns = 10;
        static private Brush NoBrush = Brushes.Transparent; // For empty labels
        static private Brush SilverBrush = Brushes.Gray; // For borders
        private Label[,] BlockLabelsPlayer1; // The labels for game Board1
        private Label[,] BlockLabelsPlayer2; // The labels for game Board2
        // TODO Future
        //private Label[,] NextBlockPlayer1; // The labels for next block p1
        //private Label[,] NextBlockPlayer2; // The labels for next block p2
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
        private bool _gameOver { get; set; }
        
        public MainWindow()
        {
            InitializeComponent();            
            CreatePlayer1GameGrid(_player1Grid);
            CreatePlayer2GameGrid(_player2Grid);           
        }
        
        // TODO Refactor   
        private void CreatePlayer1GameGrid(Grid grid)
        {
            GridPlayer1.Children.Clear();
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;

            BlockLabelsPlayer1 = new Label[_rows, _columns];
            for (int i = 0; i < _columns; i++)
            {

                for (int j = 0; j < _rows; j++)
                {
                    ColumnDefinition columnDefinition = new ColumnDefinition() { MinWidth = 25, MaxWidth = 25 };
                    grid.ColumnDefinitions.Add(columnDefinition);

                    RowDefinition rowDefinition = new RowDefinition() { MinHeight = 25, MaxHeight = 25 };
                    grid.RowDefinitions.Add(rowDefinition);
                    Label label = new Label();
                    label.Background = NoBrush;
                    label.BorderBrush = SilverBrush;
                    label.MinHeight = 25;
                    label.MaxHeight = 25;
                    label.MinWidth = 25;
                    label.MaxWidth = 25;
                    label.BorderThickness = new Thickness(1, 1, 1, 1);
                    Grid.SetRow(label, j);
                    Grid.SetColumn(label, i);
                    grid.Children.Add(label);
                    BlockLabelsPlayer1[j, i] = label;
                }
            }
            GridPlayer1.Children.Add(grid);            
        }
        private void CreatePlayer2GameGrid(Grid grid)
        {
            GridPlayer2.Children.Clear();
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;

            BlockLabelsPlayer2 = new Label[_rows, _columns];
            for (int i = 0; i < _columns; i++)
            {
                for (int j = 0; j < _rows; j++)
                {
                    ColumnDefinition columnDefinition = new ColumnDefinition() { MinWidth = 25, MaxWidth = 25 };
                    grid.ColumnDefinitions.Add(columnDefinition);

                    RowDefinition rowDefinition = new RowDefinition() { MinHeight = 25, MaxHeight = 25 };
                    grid.RowDefinitions.Add(rowDefinition);
                    Label label = new Label();
                    label.Background = NoBrush;
                    label.BorderBrush = SilverBrush;
                    label.MinHeight = 25;
                    label.MaxHeight = 25;
                    label.MinWidth = 25;
                    label.MaxWidth = 25;
                    label.BorderThickness = new Thickness(1, 1, 1, 1);
                    Grid.SetRow(label, j);
                    Grid.SetColumn(label, i);
                    grid.Children.Add(label);
                    BlockLabelsPlayer2[j, i] = label;
                }
            }
            GridPlayer2.Children.Add(grid);
        }
        // TODO Future
        ///// <summary>
        ///// Helper function to create grid of labels to display next block
        ///// </summary>
        ///// <param name="nextGrid">Grid to create labels for</param>
        //private void CreatePlayer1NextBlockGrid()
        //{
        //    NextBlockPlayer1 = new Label[4, 4];
        //    Player1Next.Children.Clear();
        //    CreateNextGrid(_player1NextGrid, ref NextBlockPlayer1);
        //    Player1Next.Children.Add(_player1NextGrid);
        //}
        ///// <summary>
        ///// Helper function to create grid of labels to display next block
        ///// </summary>
        ///// <param name="nextGrid">Grid to create labels for</param>
        //private void CreatePlayer2NextBlockGrid()
        //{
        //    NextBlockPlayer2 = new Label[4, 4];
        //    Player2Next.Children.Clear();
        //    CreateNextGrid(_player2NextGrid, ref NextBlockPlayer2);
        //    Player2Next.Children.Add(_player2NextGrid);
        //}

        //private void CreateNextGrid(Grid NextGrid, ref Label[,] NextBlockLabels)
        //{
        //    NextGrid.HorizontalAlignment = HorizontalAlignment.Stretch;            
        //    for (int i = 0; i < 4; i++)
        //    {
        //        for (int j = 0; j < 4; j++)
        //        {
        //            ColumnDefinition columnDefinition = new ColumnDefinition() { MinWidth = 15, MaxWidth = 15 };
        //            NextGrid.ColumnDefinitions.Add(columnDefinition);

        //            RowDefinition rowDefinition = new RowDefinition() { MinHeight = 15, MaxHeight = 15 };
        //            NextGrid.RowDefinitions.Add(rowDefinition);

        //            Label label = new Label();
        //            label.Background = NoBrush;
        //            label.BorderBrush = SilverBrush;
        //            label.MinHeight = 15;
        //            label.MaxHeight = 15;
        //            label.MinWidth = 15;
        //            label.MaxWidth = 15;
        //            label.BorderThickness = new Thickness(1, 1, 1, 1);
        //            Grid.SetRow(label, j);
        //            Grid.SetColumn(label, i);
        //            NextGrid.Children.Add(label);
        //            NextBlockLabels[j, i] = label;
        //        }
        //    }
        //}
        /// <summary>
        /// Task for Player1 game loop
        /// </summary>
        /// <returns></returns>
        public async Task AsyncStartGamePlayer1()
        {
            _player1GameManager = new GameManager(BlockLabelsPlayer1, _rows, _columns);            
            await AsyncStartGame(_player1GameManager);
        }
        /// <summary>
        /// Task for Player2 game loop
        /// </summary>
        /// <returns></returns>
        public async Task AsyncStartGamePlayer2()
        {
            _player2GameManager = new GameManager(BlockLabelsPlayer2, _rows, _columns);
            await AsyncStartGame(_player2GameManager);
        }

        public async Task AsyncStartGame(GameManager gameManager)
        {
            Draw(gameManager);
            while (!gameManager.GameOver)
            {
                int delay = Math.Max(75, 1000 - (gameManager.Score * 25));
                await Task.Delay(delay);
                gameManager.MoveBlockDown();                
                Draw(gameManager);
            }
            if(_player1GameManager.Score > _player2GameManager.Score)
            {
                MessageBox.Show("Player 1 wins!");
            }else if (_player1GameManager.Score < _player2GameManager.Score)
            {
                MessageBox.Show("Player 2 wins!");
            } else
            {
                MessageBox.Show("We have a draw!");
            }
        }
        #region Draw function
        public void Draw(GameManager gameManager)
        {
            DrawGrid(gameManager);
            DrawBlock(gameManager);
            int P1Score = 0;
            int P2Score = 0;
            if (_player1GameManager != null)
            {
                P1Score = _player1GameManager.Score;
            }
            if (_player2GameManager != null)
            {
                P2Score = _player2GameManager.Score;
            }
            Player1Score.Content = $"Score: {P1Score}";
            Player2Score.Content = $"Score: {P2Score}";
        }

        /// <summary>
        /// Method to draw grid with blocks
        /// </summary>
        private void DrawGrid(GameManager gameManager)
        {            
            for (int r = 0; r < gameManager.TetrisGrid.Rows; r++)
            {
                for (int c = 0; c < gameManager.TetrisGrid.Columns; c++)
                {
                    int id = gameManager.TetrisGrid[r, c];
                    // For each tile, change background to the corresponding color
                    gameManager.GameGrid[r, c].Background = tileColors[id];
                }
            }
        }
        /// <summary>
        /// Method to draw block
        /// </summary>
        private void DrawBlock(GameManager gameManager)
        {
            // For each position of the form, set the corresponding color
            foreach (Position p in gameManager.CurrentBlock.FormPositions())
            {
                gameManager.GameGrid[p.Row, p.Column].Background = tileColors[gameManager.CurrentBlock.Id];                
            }
        }
        #endregion
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (_player1GameManager.GameOver || _player2GameManager.GameOver)
            {
                return;
            }

            // Keymap - Player1 kör vissa tasks
            // Player2 kör andra

            switch (e.Key)
            {
                #region Player1 Controls
                case Key.Left:
                    Task Player1MoveLeft = Task.Run(() => _player1GameManager.MoveBlockLeft());
                    Player1MoveLeft.Wait();
                    break;
                case Key.Right:
                    Task Player1MoveRight = Task.Run(() => _player1GameManager.MoveBlockRight());
                    Player1MoveRight.Wait();
                    break;
                case Key.Down:
                    Task Player1MoveDown = Task.Run(() => _player1GameManager.MoveBlockDown());
                    Player1MoveDown.Wait();
                    break;
                case Key.Up:
                    Task Player1RotateBlockClockWise = Task.Run(() => _player1GameManager.RotateBlockCW());
                    Player1RotateBlockClockWise.Wait();
                    break;
                case Key.M:
                    Task Player1RotateBlockCounterClockWise = Task.Run(() => _player1GameManager.RotateBlockCCW());
                    Player1RotateBlockCounterClockWise.Wait();
                    break;
                //case Key.Space:
                //    _gameManager.DropBlock();
                //    break;
                #endregion
                #region Player2 controls
                case Key.A:
                    Task Player2MoveLeft = Task.Run(() => _player2GameManager.MoveBlockLeft());
                    Player2MoveLeft.Wait();
                    break;
                case Key.D:
                    Task Player2MoveRight = Task.Run(() => _player2GameManager.MoveBlockRight());
                    Player2MoveRight.Wait();
                    break;
                case Key.S:
                    Task Player2MoveDown = Task.Run(() => _player2GameManager.MoveBlockDown());
                    Player2MoveDown.Wait();
                    break;
                case Key.W:
                    Task Player2RotateBlockClockWise = Task.Run(() => _player2GameManager.RotateBlockCW());
                    Player2RotateBlockClockWise.Wait();
                    break;
                case Key.Z:
                    Task Player2RotateBlockCounterClockWise = Task.Run(() => _player2GameManager.RotateBlockCCW());
                    Player2RotateBlockCounterClockWise.Wait();
                    break;
                #endregion
                // TODO Pause game
                default:
                    return;
            }            
        }

        /// <summary>
        /// Event for click on start
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void start_Click(object sender, RoutedEventArgs e)
        {
            // Create task for player1
            Task player1 = AsyncStartGamePlayer1();
            // Create task for player2
            Task player2 = AsyncStartGamePlayer2();

            // Play music on repeat
            PlayMusic();
            // Await both tasks
            await Task.WhenAll(player1, player2);
        }

        private void quit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Function to play background 
        /// </summary>
        private void PlayMusic()
        {            
            // Play music as background task
            Task PlayMusic = Task.Run(() => {                
                Uri uri = new Uri(@"pack://application:,,,/Assets/gamemusic.wav");
                Stream fileStream = Application.GetResourceStream(uri).Stream;
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(fileStream);
                // Loop music
                player.PlayLooping();                
            });
            PlayMusic.Wait();
        }        
    }
}
