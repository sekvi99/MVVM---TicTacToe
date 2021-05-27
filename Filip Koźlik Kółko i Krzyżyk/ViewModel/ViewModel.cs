using System.Windows.Input;
using TicTacToe.Model;

namespace TicTacToe.ViewModel
{
    public class ViewModel : ViewModelBase
    {
        //Utworzenie instacji modelu w ViewModel
        private readonly TicTacToeModel model = new TicTacToeModel();

        //Pobranie obecnego gracza
        public string CurrentPlayer
        {
            get => model.CurrentPlayer;
        }

        //Tablica wynikow
        public string Score
        {
            get => "X: " + model.Score[StatesOfGame.X_WON].ToString() + " do " + 
                " O: " + model.Score[StatesOfGame.O_WON].ToString() + "\n" +
                "Ilość remisów: " + model.Score[StatesOfGame.DRAW].ToString();
        }

        //Utworzenie tablicy w viewmodelu
        public char[] Board
        {
            //Potrzebne do iloczynu x = 2, o = 1
            get
            {
                char[] game_Board = new char[9];
                for (int i = 0; i < 9; i++)
                {
                    if (model.Board[i] == 1) game_Board[i] = 'O';
                    else if (model.Board[i] == 2) game_Board[i] = 'X';
                    else game_Board[i] = ' ';
                }
                return game_Board;
            }
        }

        private ICommand move;
        public ICommand Move
        {
            get
            {
                return move ??= new RelayCommand(
                        p =>
                        {
                            model.Move(int.Parse(p.ToString()));
                            OnPropertyChanged("Board");
                            OnPropertyChanged("Score", "CurrentPlayer");
                        },
                        p => true
                    );
            }
        }

        private ICommand newGame;
        public ICommand NewGame
        {
            get
            {
                return newGame ??= new RelayCommand(
                    p =>
                    {
                        model.NewGame();
                        OnPropertyChanged("Board", "CurrentPlayer");
                    },
                    p => true
                );
            }
        }

    }
}
