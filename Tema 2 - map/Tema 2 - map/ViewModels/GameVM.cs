using Tema_2___map.Models;
using Tema_2___map.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tema_2___map.ViewModels;

namespace Tema_2___map.ViewModels
{
    class GameVM : BaseNotification
    {
        private GameBusinessLogic bl;


        public ObservableCollection<ObservableCollection<PieceVM>> GameBoard { get; set; }
        public GameVM()
        {
            ObservableCollection<ObservableCollection<Piece>> board = Helper.InitGameBoard();
            bl = new GameBusinessLogic(board);
            GameBoard = GameBoardToGameVMBoard(board);
        }

        private ObservableCollection<ObservableCollection<PieceVM>> GameBoardToGameVMBoard(ObservableCollection<ObservableCollection<Piece>> board)
        {
            ObservableCollection<ObservableCollection<PieceVM>> result = new ObservableCollection<ObservableCollection<PieceVM>>();
            for (int i = 0; i < board.Count; i++)
            {
                ObservableCollection<PieceVM> line = new ObservableCollection<PieceVM>();
                for (int j = 0; j < board[i].Count; j++)
                {
                    Piece p = board[i][j];
                    PieceVM PieceVM = new PieceVM(p, bl);
                    line.Add(PieceVM);
                }
                result.Add(line);
            }
            return result;
        }

        public PlayerType CurrentTurn => bl.CurrentTurn;

    }
}
