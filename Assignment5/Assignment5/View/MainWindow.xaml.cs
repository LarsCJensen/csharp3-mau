using Assignment5.Model;
using Assignment5.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Formats.Asn1.AsnWriter;
using Block = Assignment5.Model.Block;

namespace Assignment5.View
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
        private int _rows = 22;
        private int _columns = 10;
        private Label[,] BlockLabelsPlayer1; // The labels for game Board1
        private Label[,] BlockLabelsPlayer2; // The labels for game Board2
        static private Brush NoBrush = Brushes.Transparent; // For empty labels
        static private Brush SilverBrush = Brushes.Gray; // For borders
        public bool GameOver { get; set; }
        public Block CurrentBlock { get; private set; }
        public int Score { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            createPlayer1GameGrid(_player1Grid);
            createPlayer2GameGrid(_player2Grid);
            //MainViewModel vm = new MainViewModel();
            //this.DataContext = vm;
            //vm.OnClose += delegate { this.Close(); };

        }
        // TODO Refactor   
        private void createPlayer1GameGrid(Grid grid)
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
        private void createPlayer2GameGrid(Grid grid)
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
        /// <summary>
        /// Task for Player1 game loop
        /// </summary>
        /// <returns></returns>
        public async Task AsyncStartGamePlayer1()
        {
            _player1GameManager = new GameManager(BlockLabelsPlayer1, _rows, _columns);
            await _player1GameManager.AsyncStartGame();
        }
        /// <summary>
        /// Task for Player2 game loop
        /// </summary>
        /// <returns></returns>
        public async Task AsyncStartGamePlayer2()
        {
            _player2GameManager = new GameManager(BlockLabelsPlayer2, _rows, _columns);
            await _player2GameManager.AsyncStartGame();
        }

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
