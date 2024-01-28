using SudokuGameWPF.Models;
using System;
using System.Windows;

namespace SudokuGameWPF.Views
{
    public partial class MainSudokuWindow : Window
    {
        PlayGroundUserControl playGroundUserControl = null;
        SettingsUserControl settings = null;
        public MainSudokuWindow()
        {
            InitializeComponent();
            PlayerManager.Instance.LoadPlayer();

            this.DataContext = PlayerManager.Instance.PlayerData;
            ContentController.Visibility = Visibility.Collapsed;
        }

        private void OpenPlayground()
        {
            if (playGroundUserControl == null)
            {
                playGroundUserControl = new PlayGroundUserControl();
            }
            else
            {
                playGroundUserControl.UpdatePlayground();
            }
            ContenGrid.Children.Insert(0, playGroundUserControl);
            ContentController.Visibility = Visibility.Visible;
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            if (settings == null)
            {
                settings = new SettingsUserControl();
            }
            settings.OpenSettings();
            ContenGrid.Children.Insert(0, settings);
            ContentController.Visibility = Visibility.Visible;
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            PlayerManager.Instance.NewGame();
            OpenPlayground();
        }

        private void BtnContinue_Click(object sender, RoutedEventArgs e)
        {
            OpenPlayground();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if (ContenGrid.Children.Count > 0)
            {
                ContenGrid.Children.RemoveAt(0);
            }
            ContentController.Visibility = Visibility.Collapsed;
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
