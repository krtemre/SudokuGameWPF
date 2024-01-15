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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SudokuGameWPF.Views
{
    /// <summary>
    /// Interaction logic for SettingsUserControl.xaml
    /// </summary>
    public partial class SettingsUserControl : UserControl
    {
        public SettingsUserControl()
        {
            InitializeComponent();
        }

        private Button lastActiveButton = null;

        public void OpenSettings()
        {
            int currentDiffIndex = (int)PlayerManager.Instance.PlayerData.DiffuciltySettings;

            for (int i = 0; i < grid_Buttons.Children.Count; i++)
            {
                if (grid_Buttons.Children[i] is Button btn)
                {
                    btn.IsDefault = false;
                }
            }

            if (grid_Buttons.Children.Count > currentDiffIndex)
            {
                if (grid_Buttons.Children[currentDiffIndex] is Button btn)
                {
                    lastActiveButton = btn;
                    btn.IsDefault = true;
                }
            }

            this.Visibility = Visibility.Visible;
        }

        private void BtnDiff_Click(object sender, RoutedEventArgs e)
        {
            if(sender is Button btn)
            {
                if(lastActiveButton != null)
                {
                    lastActiveButton.IsDefault = false;
                }

                int diffIndex = int.Parse(btn.Tag.ToString());
                PlayerManager.Instance.PlayerData.DiffuciltySettings = (PlayerDiffuciltySettings)diffIndex;
                PlayerManager.Instance.PlayerData.CanContinue = false;

                btn.IsDefault = true;
                lastActiveButton = btn;
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
