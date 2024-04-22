using Tema_2___map.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Tema_2___map.Services
{
    class Helper
    {
        public static Piece SourcePiece { get; set; }
        public static Piece TargetPiece { get; set; }

        public static ObservableCollection<ObservableCollection<Piece>> InitGameBoard()
        {
            ObservableCollection<ObservableCollection<Piece>> result = new ObservableCollection<ObservableCollection<Piece>>();

            for (int row = 0; row < 8; row++)
            {
                ObservableCollection<Piece> rowPieces = new ObservableCollection<Piece>();
                for (int column = 0; column < 8; column++)
                {
                    rowPieces.Add(new Piece(PieceType.None, row, column));
                }
                result.Add(rowPieces);
            }

            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column += 2)
                {
                    if (row >= 0 && row < 3)
                    {
                        if (row % 2 == 0)
                        {
                            result[row][column + 1].Type = PieceType.Black;
                            result[row][column + 1].UpdateImage();
                        }
                        else
                        {
                            result[row][column].Type = PieceType.Black;
                            result[row][column].UpdateImage();
                        }
                    }

                    if (row >= 5 && row < 8)
                    {
                        if (row % 2 == 0)
                        {
                            result[row][column + 1].Type = PieceType.Red;
                            result[row][column + 1].UpdateImage();
                        }
                        else
                        {
                            result[row][column].Type = PieceType.Red;
                            result[row][column].UpdateImage();
                        }
                    }
                }
            }
            return result;
        }
    }
}
