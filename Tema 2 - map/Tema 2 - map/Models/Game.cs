using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema_2___map.Models
{
    public enum GameTurn
    {
        RedTurn,
        WhiteTurn,
        Finished
    }

    class Game
    {
        public Player CurrentPlayer { get; set; }
        public GameTurn Turn { get; set; }

        public bool AllowMultipleJumps { get; set; }
        public Game()
        {
            CurrentPlayer= new Player(PlayerType.Red);
            Turn = GameTurn.RedTurn;
            AllowMultipleJumps = false;
        }

        public void ChangeTurn()
        {
            if (Turn == GameTurn.RedTurn)
            {
                Turn = GameTurn.WhiteTurn;
                CurrentPlayer.Type = PlayerType.Black;
            }
            else if (Turn == GameTurn.WhiteTurn)
            {
                Turn = GameTurn.RedTurn;
                CurrentPlayer.Type = PlayerType.Red;
            }
        }


        public void EndGame()
        {
            Turn = GameTurn.Finished;
        }

    }
}
