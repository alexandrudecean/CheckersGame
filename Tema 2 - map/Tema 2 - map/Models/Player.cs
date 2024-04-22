using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema_2___map.Models
{
    public enum PlayerType
    {
        Red,
        Black
    }

    class Player : BaseNotification
    {
        private PlayerType type;
        public PlayerType Type
        {
            get { return type; }
            set
            {
                type = value;
                NotifyPropertyChanged();
            }
        }

        public Player(PlayerType type)
        {
            Type = type;
        }
    }
}
