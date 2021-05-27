using System.Collections.Generic;

namespace TicTacToe.Model
{
    enum StatesOfGame { GOING, O_WON, X_WON, DRAW };
    //going - gra w trakcjie
    //o_won - wygral gracz 'O'
    //x_won - wygral gracz 'X'
    //draw - remis w grze pomiedzy graczami

    class TicTacToeModel
    {
        //Utowrzenie slownika przechowujacego wartosci zmiennych
        private readonly Dictionary<StatesOfGame, int> score = new Dictionary<StatesOfGame, int>()
        {
        {StatesOfGame.DRAW, 0},
        {StatesOfGame.X_WON, 0},
        {StatesOfGame.O_WON, 0 }
        };

        public Dictionary<StatesOfGame, int> Score
        {
            get => score;
            //pobranie wartosci score
        }

        private readonly int[] board = new int[9];
        //utowrzenie tablicy na klasyczne 9-polowe kolko i krzyzyk

        public int[] Board
        {
            get => board;
            //pobranie tablicy game_Board do zmiennej Board
        }

        private int current_Player = 1;
        //ustawienie domyslnej wartosci dla gracza

        public string CurrentPlayer
        {
            get => current_Player == 1 ? "O" : "X";
            //Jezeli wartosc zmiennej current_player == 1, to obecnie gra O, w przeciwnym wypadku na odwrot
        }

        private int starting_player = 1;
        //ustawienie domyslnej wartosci gracza startowego


        //Metoda odpowiedzialna za sprawdzanie aktualnego stanu gry
        private StatesOfGame CurrentGameState
        {
            get
            {
                //Sprawdzenie rzedow
                //Skaczemy, co 3 aby sprawdzac wylacznie rzedy bez zbednych operacji, ktore powodowalyby bledy
                for (int i = 0; i <= 6; i += 3)
                {
                    if (board[i] * board[i + 1] * board[i + 2] == 1) return StatesOfGame.O_WON;
                    if (board[i] * board[i + 1] * board[i + 2] == 8) return StatesOfGame.X_WON;
                }

                //Sprawdzenie kolumn
                for (int i = 0; i < 3; i++)
                {
                    if (board[i] * board[i + 3] * board[i + 6] == 1) return StatesOfGame.O_WON;
                    if (board[i] * board[i + 3] * board[i + 6] == 8) return StatesOfGame.X_WON;
                }

                //Sprawdzenie przekatnych
                if (board[0] * board[4] * board[8] == 1) return StatesOfGame.O_WON;
                if (board[0] * board[4] * board[8] == 8) return StatesOfGame.X_WON;
                if (board[2] * board[4] * board[6] == 1) return StatesOfGame.O_WON;
                if (board[2] * board[4] * board[6] == 8) return StatesOfGame.X_WON;
                foreach (int i in board)
                {
                    if (i == 0) return StatesOfGame.GOING;
                }
                //Jezeli wyzej wymieone warunki nie zachodza to zwroc remis
                return StatesOfGame.DRAW;
            }
        }

        //metoda odpowiedzialna za symulowanie ruchu
        public void Move(int field)
        {
            if (board[field] != 0 || CurrentGameState != StatesOfGame.GOING) 
                return;
            //Jezeli wybrane pole nie jest puste lub jezeli gra nie jest w trakcie to metoda nic nie zwroci

            board[field] = current_Player; //ustawienie "znacznika" na polu 

            if (CurrentGameState != StatesOfGame.GOING) score[CurrentGameState] += 1;
            //jezeli gra nie jest w trakcie (tzn. jest skonczona) to inkrementujemy wartosc z enuma tj.
            //zwiekszamy ilosc wygranych 0, ilosc wygranych X, remisow

            SwitchCurrentPlayer();
        }

        //metoda odpowiedzialna za tworzenie nowej gry
        public void NewGame()
        {
            SwitchStartingPlayer(); //zmien obecnego gracza
            current_Player = starting_player; // ustawienie obecnego gracza na startowy
            ClearBoard(); // oczyszczenie tablicy ze znacznikow poprzedniej gry
        }

        //metoda odpowiedzialna za czyszcenie tablicy
        private void ClearBoard()
        {
            for (int i = 0; i < board.Length; i++)
            {
                board[i] = 0;
                //ustawienie wartosci na polu na domyslne 0 oznaczajace puste pole
            }
        }

        //Zmiana grajacego
        private void SwitchCurrentPlayer()
        {
            if (current_Player == 1) current_Player = 2;
            else if (current_Player == 2) current_Player = 1;
        }
        
        //Co kazda rozgrywke nastepuje zmiana gracza startowego
        private void SwitchStartingPlayer()
        {
            if (starting_player == 1) starting_player = 2;
            else if (starting_player == 2) starting_player = 1;
        }
    }
}
