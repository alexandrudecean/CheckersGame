using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Tema_2___map.Models;

namespace Tema_2___map.Commands
{
    internal static class Converter
    {
        public static BitmapImage ConvertByType(PieceType type, bool isKing)
        {
            string imagePath;
            if (isKing)
            {
                imagePath = (type == PieceType.Red) ? "pack://application:,,,/Tema 2 - map;component/Resources/redKing.png" : "pack://application:,,,/Tema 2 - map;component/Resources/whiteKing.png";
            }
            else
            {
                imagePath = (type == PieceType.Red) ? "pack://application:,,,/Tema 2 - map;component/Resources/redPiece.png" : "pack://application:,,,/Tema 2 - map;component/Resources/whitePiece.png";
            }

            if (type == PieceType.None)
            {
                imagePath = "pack://application:,,,/Tema 2 - map;component/Resources/blank.png";
            }

            if (type == PieceType.CanMove)
            {
                imagePath = "pack://application:,,,/Tema 2 - map;component/Resources/PossibleMove.png";
            }

            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(imagePath);
            bitmapImage.EndInit();
            return bitmapImage;
        }

        public class RoundToTextConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is PieceType.Black)
                {
                    return "negru";
                }
                if (value is PieceType.Red)
                {
                    return "rosu";
                }
                return string.Empty;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
    }
}
