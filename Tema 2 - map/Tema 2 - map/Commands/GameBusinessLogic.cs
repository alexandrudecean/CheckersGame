using Tema_2___map.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Tema_2___map.Services;
using System.Security.Policy;
using System.Windows.Documents;
using System.Windows.Interop;
using Tema_2___map.ViewModels;
using Tema_2___map.Views;

namespace Tema_2___map.Services
{
    enum PiecePosition
    {
        upLeft,
        upRight,
        downLeft,
        downRight,
        Default
    }
    class GameBusinessLogic : BaseNotification
    {
        private ObservableCollection<ObservableCollection<Piece>> pieces { get; set; }
        private ObservableCollection<Tuple<int, int, PiecePosition>> pieceMoves { get; set; }

        private int takenRedPieces { get; set; }
        private int takenBlackPieces { get; set; }



        Game game;

        public ObservableCollection<ObservableCollection<Piece>> Pieces
        {
            get { return pieces; }
            set { pieces = value; }
        }

        public GameBusinessLogic(ObservableCollection<ObservableCollection<Piece>> pieces)
        {
            this.pieces = pieces;
            pieceMoves = new ObservableCollection<Tuple<int, int, PiecePosition>>();
            takenRedPieces = 0;
            takenBlackPieces = 0;
            CanMultipleJump = false;
            game = new Game();
        }

        public void DoubleClick(Piece currentPiece)
        {
            if (Helper.SourcePiece == null && currentPiece.Type != PieceType.None)
            {
                if (currentPiece.Type == (game.CurrentPlayer.Type == PlayerType.Red ? PieceType.Red : PieceType.Black))
                {
                    Helper.SourcePiece = currentPiece;
                    FindPieceMoves(currentPiece);
                    ShowPieceMoves();
                }

            }
            else
            if (Helper.SourcePiece != null)
            {
                Helper.TargetPiece = currentPiece;
                if (Helper.TargetPiece.Type == PieceType.CanMove && pieceMoves.Any(x => x.Item1 == Helper.TargetPiece.Row && x.Item2 == Helper.TargetPiece.Column))
                {
                    PiecePosition piecePosition = pieceMoves
                        .Where(x => x.Item1 == Helper.TargetPiece.Row
                                 && x.Item2 == Helper.TargetPiece.Column
                                 && x.Item3 != PiecePosition.Default)
                        .Select(x => x.Item3)
                        .FirstOrDefault();
                    Move(Helper.SourcePiece, Helper.TargetPiece, piecePosition);
                    game.ChangeTurn();
                }
                Helper.SourcePiece = null;
                Helper.TargetPiece = null;
                HidePossibleMoves();
            }
        }

        private void Move(Piece sourcePiece, Piece targetPiece, PiecePosition piecePosition)
        {
            // Mutare simplă
            if ((targetPiece.Row - sourcePiece.Row == 1 || sourcePiece.Column - targetPiece.Column == 1) ||
               (targetPiece.Row - sourcePiece.Row == -1 || sourcePiece.Column - targetPiece.Column == -1))
            {
                if (targetPiece.Type == PieceType.CanMove)
                {
                    targetPiece.Type = PieceType.None;

                }
                Tuple<PieceType, bool> aux = new Tuple<PieceType, bool>(targetPiece.Type, targetPiece.IsKing);

                targetPiece.Type = sourcePiece.Type;
                targetPiece.IsKing = sourcePiece.IsKing || (sourcePiece.Type == PieceType.Red && sourcePiece.Row == 0) || (sourcePiece.Type == PieceType.Black && sourcePiece.Row == 7);

                if ((targetPiece.Row == 0 && sourcePiece.Type == PieceType.Red) || (targetPiece.Row == 7 && sourcePiece.Type == PieceType.Black))
                {
                    // Piesa a ajuns la capătul corespunzător, deci o transformăm în "king"
                    targetPiece.IsKing = true;
                    targetPiece.UpdateImage();
                }

                sourcePiece.Type = aux.Item1;
                sourcePiece.IsKing = aux.Item2;
                sourcePiece.UpdateImage();
                targetPiece.UpdateImage();

                if ((targetPiece.Row == 0 && sourcePiece.Type == PieceType.Red) || (targetPiece.Row == 7 && sourcePiece.Type == PieceType.Black))
                {
                    // Piesa a ajuns la capătul corespunzător, deci o transformăm în "king"
                    targetPiece.IsKing = true;
                    targetPiece.UpdateImage();
                }


            }
            //Saritura peste o piesa
            else
            {
                int jumpedRow = (sourcePiece.Row + targetPiece.Row) / 2;
                int jumpedColumn = (sourcePiece.Column + targetPiece.Column) / 2;

                if ((targetPiece.Row - sourcePiece.Row == 2 || sourcePiece.Column - targetPiece.Column == 2) ||
                   (targetPiece.Row - sourcePiece.Row == -2 || sourcePiece.Column - targetPiece.Column == -2))
                {
                    if (pieces[jumpedRow][jumpedColumn].Type != PieceType.None && pieces[jumpedRow][jumpedColumn].Type != sourcePiece.Type)
                    {
                        if (pieces[jumpedRow][jumpedColumn].Type == PieceType.Red)
                        {
                            takenRedPieces++;
                        }
                        else
                        {
                            takenBlackPieces++;
                        }
                        pieces[jumpedRow][jumpedColumn].Type = PieceType.None;

                        if (targetPiece.Type == PieceType.CanMove)
                        {
                            targetPiece.Type = PieceType.None;
                            CheckEndGame();
                        }
                        Tuple<PieceType, bool> aux = new Tuple<PieceType, bool>(targetPiece.Type, targetPiece.IsKing);

                        targetPiece.Type = sourcePiece.Type;
                        targetPiece.IsKing = sourcePiece.IsKing || (sourcePiece.Type == PieceType.Red && sourcePiece.Row == 0) || (sourcePiece.Type == PieceType.Black && sourcePiece.Row == 7);

                        if ((targetPiece.Row == 0 && sourcePiece.Type == PieceType.Red) || (targetPiece.Row == 7 && sourcePiece.Type == PieceType.Black))
                        {
                            // Piesa a ajuns la capătul corespunzător, deci o transformăm în "king"
                            targetPiece.IsKing = true;
                            targetPiece.UpdateImage();
                            CheckEndGame();
                        }

                        sourcePiece.Type = aux.Item1;
                        sourcePiece.IsKing = aux.Item2;
                        sourcePiece.UpdateImage();
                        targetPiece.UpdateImage();
                        pieces[jumpedRow][jumpedColumn].UpdateImage();
                        //  CheckEndGame();

                    }
                }
            }


            // Resetăm starea pentru următoarea mutare
            Helper.SourcePiece = null;
            Helper.TargetPiece = null;
            HidePossibleMoves();

        }

        public bool CanMultipleJump { get; set; }
        public void MultipleJump(Piece sourcePiece, Piece targetPiece, PiecePosition piecePosition)
        {
            if (CanMultipleJump)
            {
                if (targetPiece.Type == PieceType.CanMove && pieceMoves.Any(x => x.Item1 == targetPiece.Row && x.Item2 == targetPiece.Column))
                {
                    PiecePosition jumpDirection = pieceMoves
                        .Where(x => x.Item1 == targetPiece.Row
                                 && x.Item2 == targetPiece.Column
                                 && x.Item3 != PiecePosition.Default)
                        .Select(x => x.Item3)
                        .FirstOrDefault();
                    Move(sourcePiece, targetPiece, jumpDirection);
                    CanMultipleJump = false;
                    FindPieceMoves(targetPiece);
                    ShowPieceMoves();
                }
            }
        }

        private void HidePossibleMoves()
        {
            foreach (var move in pieceMoves)
            {
                if (pieces[move.Item1][move.Item2].Type == PieceType.CanMove)
                {
                    pieces[move.Item1][move.Item2].Type = PieceType.None;
                    pieces[move.Item1][move.Item2].UpdateImage();
                }
            }
            pieceMoves.Clear();
        }

        private void ShowPieceMoves()
        {
            foreach (var move in pieceMoves)
            {
                if (pieces[move.Item1][move.Item2].Type == PieceType.None)
                {
                    pieces[move.Item1][move.Item2].Type = PieceType.CanMove;
                    pieces[move.Item1][move.Item2].UpdateImage();
                }
            }
        }

        private void FindPieceMoves(Piece piece)
        {
            pieceMoves.Clear();
            int row = piece.Row;
            int column = piece.Column;

            // Verificăm mutările posibile pentru piesele roșii
            if (piece.Type == PieceType.Red || piece.IsKing)
            {
                if (row > 0)
                {
                    if (column > 0 && pieces[row - 1][column - 1].Type == PieceType.None)
                    {
                        pieceMoves.Add(new Tuple<int, int, PiecePosition>(row - 1, column - 1, PiecePosition.Default));
                    }
                    if (column < 7 && pieces[row - 1][column + 1].Type == PieceType.None)
                    {
                        pieceMoves.Add(new Tuple<int, int, PiecePosition>(row - 1, column + 1, PiecePosition.Default));
                    }
                    if (row > 1)
                    {
                        if (column > 1 && pieces[row - 2][column - 2].Type == PieceType.None && pieces[row - 1][column - 1].Type == PieceType.Black)
                        {
                            pieceMoves.Add(new Tuple<int, int, PiecePosition>(row - 2, column - 2, PiecePosition.upLeft));
                            CanMultipleJump = true;
                        }
                        if (column < 6 && pieces[row - 2][column + 2].Type == PieceType.None && pieces[row - 1][column + 1].Type == PieceType.Black)
                        {
                            pieceMoves.Add(new Tuple<int, int, PiecePosition>(row - 2, column + 2, PiecePosition.downRight));
                            CanMultipleJump = true;
                        }
                    }
                }

                if (piece.IsKing)
                {
                    // Adăugăm mișcările posibile pentru piesele "king" roșii
                    if (row < 7)
                    {
                        if (column > 0 && (pieces[row + 1][column - 1].Type == PieceType.None))
                        {
                            pieceMoves.Add(new Tuple<int, int, PiecePosition>(row + 1, column - 1, PiecePosition.Default));
                        }
                        if (column < 7 && (column + 1) != 8 && (pieces[row + 1][column + 1].Type == PieceType.None))
                        {
                            pieceMoves.Add(new Tuple<int, int, PiecePosition>(row + 1, column + 1, PiecePosition.Default));
                        }
                        if (row < 6)
                        {
                            if ((column - 2) >= 0)
                            {
                                if (pieces[row + 1][column - 1].Type == PieceType.Black && pieces[row + 2][column - 2].Type == PieceType.None)
                                {
                                    pieceMoves.Add(new Tuple<int, int, PiecePosition>(row + 2, column - 2, PiecePosition.upLeft));
                                }
                            }
                            if ((column + 2) < 8)
                            {
                                if (pieces[row + 1][column + 1].Type == PieceType.Black && pieces[row + 2][column + 2].Type == PieceType.None)
                                {
                                    pieceMoves.Add(new Tuple<int, int, PiecePosition>(row + 2, column + 2, PiecePosition.downRight));
                                }
                            }

                        }
                    }

                    // Verificăm mutările posibile în direcția opusă pentru piesele "king" roșii
                    if (row > 0)
                    {
                        if (column > 0 && (pieces[row - 1][column - 1].Type == PieceType.None))
                        {
                            pieceMoves.Add(new Tuple<int, int, PiecePosition>(row - 1, column - 1, PiecePosition.Default));
                        }

                        if (column < 7 && (pieces[row - 1][column + 1].Type == PieceType.None))
                        {
                            pieceMoves.Add(new Tuple<int, int, PiecePosition>(row - 1, column + 1, PiecePosition.Default));
                        }

                        if (row > 1)
                        {
                            if ((column - 2) >= 0)
                            {
                                if (pieces[row - 1][column - 1].Type == PieceType.Black && pieces[row - 2][column - 2].Type == PieceType.None)
                                {
                                    pieceMoves.Add(new Tuple<int, int, PiecePosition>(row - 2, column - 2, PiecePosition.downLeft));
                                }
                            }

                            if ((column + 2) < 8)
                            {
                                if (pieces[row - 1][column + 1].Type == PieceType.Black && pieces[row - 2][column + 2].Type == PieceType.None)
                                {
                                    pieceMoves.Add(new Tuple<int, int, PiecePosition>(row - 2, column + 2, PiecePosition.downRight));
                                }
                            }
                        }
                    }
                }
            }

            // Verificăm mutările posibile pentru piesele negre
            else if (piece.Type == PieceType.Black || piece.IsKing)
            {
                if (row < 7)
                {
                    if (column > 0 && pieces[row + 1][column - 1].Type == PieceType.None)
                    {
                        pieceMoves.Add(new Tuple<int, int, PiecePosition>(row + 1, column - 1, PiecePosition.Default));
                    }

                    if (column < 7 && pieces[row + 1][column + 1].Type == PieceType.None)
                    {
                        pieceMoves.Add(new Tuple<int, int, PiecePosition>(row + 1, column + 1, PiecePosition.Default));
                    }

                    if (row < 6)
                    {
                        if (column > 1 && pieces[row + 2][column - 2].Type == PieceType.None && pieces[row + 1][column - 1].Type == PieceType.Red)
                        {
                            pieceMoves.Add(new Tuple<int, int, PiecePosition>(row + 2, column - 2, PiecePosition.downLeft));
                            CanMultipleJump = true;
                        }
                        if (column < 6 && pieces[row + 2][column + 2].Type == PieceType.None && pieces[row + 1][column + 1].Type == PieceType.Red)
                        {
                            pieceMoves.Add(new Tuple<int, int, PiecePosition>(row + 2, column + 2, PiecePosition.downRight));
                            CanMultipleJump = true;
                        }
                    }
                }

                if (piece.IsKing)
                {
                    // Adăugăm mișcările posibile pentru piesele "king" negre
                    if (row > 0)
                    {
                        if (column > 0 && (pieces[row + 1][column - 1].Type == PieceType.None))
                        {
                            pieceMoves.Add(new Tuple<int, int, PiecePosition>(row + 1, column - 1, PiecePosition.Default));
                        }
                        if (column < 7 && (pieces[row + 1][column + 1].Type == PieceType.None))
                        {
                            pieceMoves.Add(new Tuple<int, int, PiecePosition>(row + 1, column + 1, PiecePosition.Default));
                        }
                        if (row > 1)
                        {
                            if ((column - 2) >= 0)
                            {
                                if (pieces[row + 1][column - 1].Type == PieceType.Red && pieces[row + 2][column - 2].Type == PieceType.None)
                                {
                                    pieceMoves.Add(new Tuple<int, int, PiecePosition>(row + 2, column - 2, PiecePosition.downLeft));
                                }
                            }
                            if ((column + 2) < 8)
                            {
                                if (pieces[row + 1][column + 1].Type == PieceType.Red && pieces[row + 2][column + 2].Type == PieceType.None)
                                {
                                    pieceMoves.Add(new Tuple<int, int, PiecePosition>(row + 2, column + 2, PiecePosition.downRight));
                                }
                            }
                        }

                    }
                    // Verificăm mutările posibile în direcția opusă pentru piesele "king" negre
                    if (row < 7)
                    {
                        if (column > 0 && (pieces[row - 1][column - 1].Type == PieceType.None))
                        {
                            pieceMoves.Add(new Tuple<int, int, PiecePosition>(row - 1, column - 1, PiecePosition.Default));
                        }
                        if (column < 7 && (pieces[row - 1][column + 1].Type == PieceType.None))
                        {
                            pieceMoves.Add(new Tuple<int, int, PiecePosition>(row - 1, column + 1, PiecePosition.Default));
                        }
                        if (row < 6)
                        {
                            if ((column - 2) >= 0)
                            {
                                if (pieces[row - 1][column - 1].Type == PieceType.Red && pieces[row - 2][column - 2].Type == PieceType.None)
                                {
                                    pieceMoves.Add(new Tuple<int, int, PiecePosition>(row - 2, column - 2, PiecePosition.downLeft));
                                }
                            }

                            if ((column + 2) < 8)
                            {
                                if (pieces[row - 1][column + 1].Type == PieceType.Red && pieces[row - 2][column + 2].Type == PieceType.None)
                                {
                                    pieceMoves.Add(new Tuple<int, int, PiecePosition>(row - 2, column + 2, PiecePosition.downRight));
                                }
                            }
                        }
                    }
                }

            }
        }


        private void CheckEndGame()
        {
            if (takenRedPieces == 12)
            {
                // Jucătorul Black este câștigător
                EndGame(PlayerType.Black);
            }
            else if (takenBlackPieces == 12)
            {
                // Jucătorul roșu este câștigător
                EndGame(PlayerType.Red);
            }

        }

        private void EndGame(PlayerType player)
        {
            string winnerName = player == PlayerType.Black ? "Negru" : "rosu";
            GameOverVM viewModel = new GameOverVM();
            viewModel.GameOverMessage = "Castigatorul este: " + winnerName;
            GameOverWindow gameOverWindow = new GameOverWindow(viewModel);
            gameOverWindow.ShowDialog();
            GameOver?.Invoke(this, EventArgs.Empty);
        }

        public EventHandler GameOver;

        public PlayerType CurrentTurn { get; private set; }

        public void ChangeTurn()
        {
            CurrentTurn = game.CurrentPlayer.Type;
        }
    }
}
