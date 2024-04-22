using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tema_2___map.Commands;
using Tema_2___map.Models;
using Tema_2___map.Services;

namespace Tema_2___map.ViewModels
{
    class PieceVM: BaseNotification
    {
        GameBusinessLogic bl;
        private Piece simplePiece = new Piece();
        public Piece SimplePiece
        {
            get { return simplePiece; }
            set
            {
                simplePiece = value;
                NotifyPropertyChanged("SimplePiece");
            }
        }
        
        public PieceVM(Piece p, GameBusinessLogic bl)
        {
            this.bl = bl;
            SimplePiece = p;
        }

        public PieceVM(PieceType type, int row, int column, GameBusinessLogic bl)
        {
            this.bl = bl;
            SimplePiece=new Piece(type, row, column);
        }

        private ICommand clickCommand;

        public ICommand ClickCommand
        {
            get
            {
                if (clickCommand == null)
                {
                    clickCommand = new RelayCommand<Piece>(bl.DoubleClick);
                }
                return clickCommand;
            }
        }
    }
}
