﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema_2___map.ViewModels
{
    public class GameOverVM : INotifyPropertyChanged
    {
        private string _gameOverMessage;
        public string GameOverMessage
        {
            get { return _gameOverMessage; }
            set
            {
                _gameOverMessage = value;
                OnPropertyChanged(nameof(GameOverMessage));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

