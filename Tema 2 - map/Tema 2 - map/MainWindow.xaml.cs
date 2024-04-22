using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tema_2___map.Views;

namespace Tema_2___map
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            // Deschide fereastra pentru un joc nou
            GameWindow gameWindow = new GameWindow();
            gameWindow.Show();
            Close(); // Închide fereastra curentă (meniul)
        }

        private void OpenSavedGame_Click(object sender, RoutedEventArgs e)
        {
            // Adaugă logica pentru a deschide un joc salvat
            MessageBox.Show("Funcționalitatea pentru deschiderea unui joc salvat va fi implementată în curând.");
        }

        private void ViewStatistics_Click(object sender, RoutedEventArgs e)
        {
            // Adaugă logica pentru afișarea statisticilor
            MessageBox.Show("Funcționalitatea pentru vizualizarea statisticilor va fi implementată în curând.");
        }

        private void ExitMenu_Click(object sender, RoutedEventArgs e)
        {
            // Ieșire din aplicație
            Close();
        }
    }
}
