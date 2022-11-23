using Assignment5.Model;
using Assignment5.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private Label[,] BlockControls; // The labels for game Board
        static private Brush NoBrush = Brushes.Transparent; // For empty labels
        static private Brush SilverBrush = Brushes.Gray; // For borders
        public bool GameOver { get; set; }
        public Block CurrentBlock { get; private set; }
        public int Score { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            createGameGrid(_player1Grid);
            createGameGrid(_player2Grid);
            //MainViewModel vm = new MainViewModel();
            //this.DataContext = vm;
            //vm.OnClose += delegate { this.Close(); };

        }
        // TODO Refactor, player1 + 2        
        private void createGameGrid(Grid grid)
        {
            GridPlayer1.Children.Clear();
            //GridPlayer2.Children.Clear();
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;
            
            BlockControls = new Label[_rows, _columns];
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
                    BlockControls[j, i] = label;
                }
            }
            GridPlayer1.Children.Add(grid);
            //GridPlayer2.Children.Add(grid);
        }
        public async Task StartGamePlayer1()
        {
            _player1GameManager = new GameManager(BlockControls, _rows, _columns);
            await _player1GameManager.StartGame();
        }
        public async Task StartGamePlayer2()
        {
            _player2GameManager = new GameManager(BlockControls, _rows, _columns);
            await _player2GameManager.StartGame();
        }


        private void DrawGrid(Grid grid)
        {
            // TODO Rita upp spelplanen för player1/2
            // Loopa över alla labels i arrayen? Eller 
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            // TODO make task
            //if (_gameManager.GameOver)
            //{
            //    return;
            //}

            //switch (e.Key)
            //{
            //    case Key.Left:
            //        _gameManager.MoveBlockLeft();
            //        break;
            //    case Key.Right:
            //        _gameManager.MoveBlockRight();
            //        break;
            //    case Key.Down:
            //        _gameManager.MoveBlockDown();
            //        break;
            //    case Key.Up:
            //        _gameManager.RotateBlockCW();
            //        break;
            //    case Key.Z:
            //        _gameManager.RotateBlockCCW();
            //        break;
            //    case Key.C:
            //        _gameManager.HoldBlock();
            //        break;
            //    case Key.Space:
            //        _gameManager.DropBlock();
            //        break;
            //    default:
            //        return;
            //}

            //DrawGrid(gameState);
        }

        private async void start_Click(object sender, RoutedEventArgs e)
        {
            await StartGamePlayer1();
            await StartGamePlayer2();
        }
    }
}
