using SudokuGameWPF.Models;
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
using System.Windows.Shapes;

namespace SudokuGameWPF.Views
{
    public partial class MainSudokuWindow : Window
    {
        public MainSudokuWindow()
        {
            InitializeComponent();
            PlayerManager.Instance.LoadPlayer();

            this.DataContext = PlayerManager.Instance.PlayerData;
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            settingsScreen.OpenSettings();
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine( );
            PlayerManager.Instance.NewGame();
            PlayGroundWindow window = new PlayGroundWindow();
            window.Show();
        }

        private void BtnContinue_Click(object sender, RoutedEventArgs e)
        {
            PlayGroundWindow window = new PlayGroundWindow();
            window.Show();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ClosingGame();
        }

        private void ClosingGame()
        {
            PlayerManager.Instance.SavePlayer();
        }
    }
}
