using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace Tema_2___map.Models
{
    public enum PieceType
    {
        None,
        CanMove,
        Red,
        Black
    }

    class Piece : BaseNotification
    {
        private PieceType type;
        public PieceType Type
        {
            get { return type; }
            set
            {
                type = value;
                NotifyPropertyChanged();
            }
        }
        private int row;

        public int Row
        {
            get { return row; }
            set
            {
                row = value;
                NotifyPropertyChanged("RawPosition");
            }
        }

        private int column;

        public int Column
        {
            get { return column; }
            set
            {
                column = value;
                NotifyPropertyChanged("ColumnPosition");
            }
        }

        private bool isKing;
        public bool IsKing
        {
            get { return isKing; }
            set
            {
                isKing = value;
                NotifyPropertyChanged("IsKing");
            }
        }

        private BitmapImage image;
        public BitmapImage PieceImage
        {
            get { return image; }
            set
            {
                image = value;
                NotifyPropertyChanged("PieceImage");
            }
        }
        public void UpdateImage()
        {
            PieceImage = Commands.Converter.ConvertByType(Type, IsKing);
        }

        public Piece(int row, int column)
        {
            Type = PieceType.None;
            Row = row;
            Column = column;
            IsKing = false;
            PieceImage = Commands.Converter.ConvertByType(Type, IsKing);
        }

        public Piece()
        {
            Type = PieceType.None;
            Row = 0;
            Column = 0;
            IsKing = false;
            PieceImage = Commands.Converter.ConvertByType(Type, IsKing);
        }

        public Piece(PieceType type, int row, int column, bool isKing)
        {
            Type = type;
            Row = row;
            Column = column;
            IsKing = isKing;
            PieceImage = Commands.Converter.ConvertByType(Type, IsKing);
        }

        public Piece(PieceType type, int row, int column)
        {
            Type = type;
            Row = row;
            Column = column;
            IsKing = false;
            PieceImage = Commands.Converter.ConvertByType(type, false);
        }
    }
}
