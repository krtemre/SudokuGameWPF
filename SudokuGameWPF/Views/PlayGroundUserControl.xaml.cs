using SudokuGameWPF.Models;
using SudokuGameWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace SudokuGameWPF.Views
{
    /// <summary>
    /// Interaction logic for PlayGroundUserControl.xaml
    /// </summary>
    public partial class PlayGroundUserControl : UserControl
    {
        private Button lastButton = null;
        private Button lastSelected = null;

        private List<Grid> playgroundGrids = null;

        #region Timer
        private DateTime buttonPressedTime;
        private const int ACTIVETOR_TIME_IN_MS = 1000;
        private DispatcherTimer timer = new DispatcherTimer();
        #endregion

        public PlayGroundUserControl()
        {
            InitializeComponent();
            InitGrids(PlayerManager.Instance.PlayerData.Game);

            buttonPressedTime = DateTime.MaxValue;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if(DateTime.UtcNow.Subtract(buttonPressedTime).TotalMilliseconds > ACTIVETOR_TIME_IN_MS)
            {
                //TODO
            }
        }

        public void UpdatePlayground()
        {
            UpdateGrids(PlayerManager.Instance.PlayerData.Game);
        }

        #region Buttons
        private void BtnPlayground_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                if (lastButton != null)
                {
                    lastButton.IsDefault = false;
                }

                btn.IsDefault = true;
                lastButton = btn;

                CheckUpdates();
            }
        }

        private void BtnSelection_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                if (lastSelected != null)
                {
                    if (lastSelected.Content.Equals(btn.Content))
                    {
                        lastSelected.IsDefault = false;
                        lastSelected = null;
                    }
                    else
                    {
                        lastSelected.IsDefault = false;

                        btn.IsDefault = true;
                        lastSelected = btn;
                    }
                }
                else
                {
                    btn.IsDefault = true;
                    lastSelected = btn;
                }

                CheckUpdates();
            }
        }

        private void Selector_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void Selector_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
        #endregion

        private void HighlightByPosition(int x, int y, int gridIndex)
        {
            //TODO
            //if 2,2 is selected highlight first Grid and all 2,x & x,2.
            //Highlight differently 2,2
        }

        private void HighlightByNumber(int number)
        {
            //TODO 
            //if 3 is selected highlight all 3s in game

        }

        private void CheckUpdates()
        {
            if (lastSelected != null && lastButton != null)
            {
                bool isTrue = string.IsNullOrEmpty(lastButton.Content.ToString());
                string tag = lastSelected.Content.ToString();
                int val = int.Parse(tag);

                string[] pos = lastButton.Tag.ToString().Split(',');
                int x = int.Parse(pos[0]);
                int y = int.Parse(pos[1]);

                isTrue &= PlayerManager.Instance.ControlIsValueTrue(x, y, val);

                if (isTrue)
                {
                    lastButton.Content = tag;
                    PlayerManager.Instance.UpdateValue(x, y, val);
                    lastButton.Foreground = Brushes.Green;
                    lastButton = null;
                    lastSelected.IsDefault = false;
                    lastSelected = null;
                }
                else
                {
                    NotifactionController.Instance.ShowNotification(NotificationType.Warning, "TEST, TEST");
                }
            }
        }

        private void InitGrids(GameData gameData)
        {
            playgroundGrids = new List<Grid>()
            {
                Grid0_0,
                Grid0_1,
                Grid0_2,

                Grid1_0,
                Grid1_1,
                Grid1_2,

                Grid2_0,
                Grid2_1,
                Grid2_2,
            };

            UpdateGrids(gameData);
        }

        private void UpdateGrids(GameData gameData)
        {
            if (playgroundGrids == null) { return; }


            for (int i = 0; i < playgroundGrids.Count; i++)
            {
                Grid grid = playgroundGrids[i];
                for (int j = 0; j < grid.Children.Count; j++)
                {
                    if (grid.Children[j] is Button btn)
                    {
                        int val = gameData.Grids[i].Values[j];
                        btn.Content = val == 0 ? "" : val.ToString();
                    }
                }
            }
        }
    }
}