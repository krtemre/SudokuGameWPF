using SudokuGameWPF.Models;
using SudokuGameWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace SudokuGameWPF.Views
{
    public partial class PlayGroundUserControl : UserControl
    {
        private PlaygroundSelectorButton lastSelectorBtn = null;
        private PlaygroundButton lastPlaygroundBtn = null;

        private List<PlaygroundGrid> playgroundGrids = null;

        private Dictionary<int, int> TotalValuesInGame = new Dictionary<int, int>();
        private int TotatDisabeledButtons = 0;

        #region Timer
        private DateTime selectorButtonPressedTime;
        private const int SELECTOR_ACTIVATOR_TIME_IN_MS = 1000;
        private DateTime playgroundButtonPressedTime;
        private const int ACTIVATOR_TIME_IN_MS = 1000;
        #endregion

        public PlayGroundUserControl()
        {
            InitializeComponent();
            InitGrids(PlayerManager.Instance.PlayerData.Game);

            selectorButtonPressedTime = DateTime.MaxValue;
        }

        public void UpdatePlayground()
        {
            UpdateGrids(PlayerManager.Instance.PlayerData.Game);

            if (lastSelectorBtn != null)
            {
                lastSelectorBtn.IsDefault = false;
            }
            lastSelectorBtn = null;
            lastPlaygroundBtn = null;
        }

        #region Buttons
        private void Playground_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is PlaygroundButton playgroundBtn)
            {
                playgroundButtonPressedTime = DateTime.UtcNow;
                if (lastSelectorBtn == null || !lastSelectorBtn.IsSelected)
                {
                    DeHighlightAll();
                }

                if (lastPlaygroundBtn != null)
                {
                    lastPlaygroundBtn.IsSelected = false;
                }
            }
        }

        private void Playground_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is PlaygroundButton playgroundBtn)
            {
                playgroundBtn.IsSelected = true;
                lastPlaygroundBtn = playgroundBtn;

                if (DateTime.UtcNow.Subtract(playgroundButtonPressedTime).TotalMilliseconds > ACTIVATOR_TIME_IN_MS)
                {
                    playgroundBtn.InHighlightArea = true;
                    HighlightByPosition(playgroundBtn.Row, playgroundBtn.Col, playgroundBtn.GridIndex);
                }
                else
                {
                    TryUpdatePlayGround();
                }
            }
        }

        private void Selector_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is PlaygroundSelectorButton selectorBtn)
            {
                selectorButtonPressedTime = DateTime.UtcNow;

                if (lastSelectorBtn != null)
                {
                    lastSelectorBtn.IsSelected = false;
                }
            }
        }

        private void Selector_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is PlaygroundSelectorButton selectorBtn)
            {
                lastSelectorBtn = selectorBtn;

                if (DateTime.UtcNow.Subtract(selectorButtonPressedTime).TotalMilliseconds > SELECTOR_ACTIVATOR_TIME_IN_MS)
                {
                    lastSelectorBtn.IsSelected = true;
                    HighlightByNumber(selectorBtn.Value);
                }
                else
                {
                    TryUpdatePlayGround();

                    if (lastPlaygroundBtn == null || !lastPlaygroundBtn.IsSelected)
                    {
                        DeHighlightAll();
                    }

                    lastSelectorBtn = null;
                }
            }
        }

        private void TryUpdatePlayGround()
        {
            if (lastSelectorBtn == null || lastPlaygroundBtn == null) { return; }

            if (lastPlaygroundBtn.Value != 0) { return; }

            int value = lastSelectorBtn.Value;
            int row = lastPlaygroundBtn.Row;
            int col = lastPlaygroundBtn.Col;

            bool correct = PlayerManager.Instance.ControlIsValueTrue(row, col, value);

            if (correct)
            {
                lastPlaygroundBtn.Value = value;
                lastPlaygroundBtn.IsSelected = false;
                lastPlaygroundBtn.InHighlightArea = true;

                if (TotalValuesInGame.ContainsKey(value))
                {
                    TotalValuesInGame[value]++;
                }
                else
                {
                    TotalValuesInGame.Add(value, 1);
                }
                CheckRemainingNumbers();
            }
            else
            {
                NotifactionController.Instance.ShowNotification(NotificationType.Warning, $"The {value} is not suitable at Row : {row} and Col : {col}");
                PlayerManager.Instance.PlayerData.Mistakes++;
            }
        }

        private void CheckRemainingNumbers()
        {
            bool anyHiglight = false;

            if (lastPlaygroundBtn != null)
            {
                anyHiglight |= lastPlaygroundBtn.InHighlightArea;
            }
            if (lastSelectorBtn != null)
            {
                anyHiglight |= lastSelectorBtn.IsSelected;
            }

            var noRemainingList = TotalValuesInGame.Where(s => s.Value >= 9).ToList();

            if (noRemainingList != null && noRemainingList.Count > 0)
            {
                foreach (var value in noRemainingList)
                {
                    if (SelectorButtonsGrid.Children[value.Key - 1] is PlaygroundSelectorButton btn)
                    {
                        if (!btn.IsEnabled) { continue; }

                        if(btn == lastSelectorBtn)
                        {
                            lastSelectorBtn = null;
                        }

                        btn.IsSelected = false;
                        btn.IsEnabled = false;
                        btn.Background = Brushes.DarkGray;
                        btn.Foreground = Brushes.Yellow;
                        TotatDisabeledButtons++;
                    }
                }
            }

            if(TotatDisabeledButtons >= 9)
            {
                //TODO Win Condition
            }
        }
        #endregion

        private void HighlightByPosition(int row, int col, int gridIndex)
        {
            //TODO
            //if 2,2 is selected highlight first Grid and all 2,x & x,2.
            //Highlight differently 2,2

            foreach (PlaygroundGrid grid in playgroundGrids)
            {
                for (int i = 0; i < grid.Children.Count; i++)
                {
                    if (grid.Children[i] is PlaygroundButton btn)
                    {
                        btn.InHighlightArea = btn.GridIndex == gridIndex;
                        btn.InHighlightArea |= btn.Row == row;
                        btn.InHighlightArea |= btn.Col == col;
                    }
                }
            }
        }

        private void DeHighlightAll()
        {
            foreach (PlaygroundGrid grid in playgroundGrids)
            {
                for (int i = 0; i < grid.Children.Count; i++)
                {
                    if (grid.Children[i] is PlaygroundButton btn)
                    {
                        btn.InHighlightArea = false;
                    }
                }
            }
        }

        private void HighlightByNumber(int number)
        {
            //TODO 
            //if 3 is selected highlight all 3s in game

            foreach (PlaygroundGrid grid in playgroundGrids)
            {
                for (int i = 0; i < grid.Children.Count; i++)
                {
                    if (grid.Children[i] is PlaygroundButton btn)
                    {
                        btn.InHighlightArea = btn.Value == number;
                        btn.IsSelected = false;
                    }
                }
            }
        }

        private void CheckUpdates()
        {
            if (lastSelectorBtn != null && lastPlaygroundBtn != null)
            {
                bool isTrue = string.IsNullOrEmpty(lastPlaygroundBtn.Content.ToString());
                string tag = lastSelectorBtn.Content.ToString();
                int val = int.Parse(tag);

                string[] pos = lastPlaygroundBtn.Tag.ToString().Split(',');
                int x = int.Parse(pos[0]);
                int y = int.Parse(pos[1]);

                isTrue &= PlayerManager.Instance.ControlIsValueTrue(x, y, val);

                if (isTrue)
                {
                    lastPlaygroundBtn.Content = tag;
                    PlayerManager.Instance.UpdateValue(x, y, val);
                    lastPlaygroundBtn.Foreground = Brushes.Green;
                    lastPlaygroundBtn = null;
                    lastSelectorBtn.IsDefault = false;
                    lastSelectorBtn = null;
                }
                else
                {
                    NotifactionController.Instance.ShowNotification(NotificationType.Warning, "TEST, TEST");
                }
            }
        }

        private void InitGrids(GameData gameData)
        {
            playgroundGrids = new List<PlaygroundGrid>()
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
            TotatDisabeledButtons = 0;
            TotalValuesInGame.Clear();
            foreach (var item in SelectorButtonsGrid.Children)
            {
                if (item is PlaygroundSelectorButton btn)
                {
                    btn.IsEnabled = true;
                    btn.Foreground = Brushes.White;
                }
            }

            for (int i = 0; i < playgroundGrids.Count; i++)
            {
                PlaygroundGrid grid = playgroundGrids[i];
                for (int j = 0; j < grid.Children.Count; j++)
                {
                    if (grid.Children[j] is PlaygroundButton btn)
                    {
                        int val = gameData.Grids[i].Values[j];
                        btn.Value = val;
                        btn.GridIndex = grid.GridIndex;
                        btn.GridPosition_X = j / 3; // 0 1 2
                        btn.GridPosition_Y = j % 3; // 0 1 2
                        btn.IsSelected = false;
                        btn.InHighlightArea = false;
                        btn.HasDefaultValue = val != 0;

                        if (val != 0)
                        {
                            if (TotalValuesInGame.ContainsKey(val))
                            {
                                TotalValuesInGame[val]++;
                            }
                            else
                            {
                                TotalValuesInGame.Add(val, 1);
                            }

                            if (TotalValuesInGame[val] >= 9)
                            {
                                if(SelectorButtonsGrid.Children[val] is PlaygroundSelectorButton playgroundSelectorButton)
                                {
                                    playgroundSelectorButton.IsSelected = false;
                                    playgroundSelectorButton.IsEnabled = false;
                                    playgroundSelectorButton.Background = Brushes.DarkGray;
                                    playgroundSelectorButton.Foreground = Brushes.Yellow;
                                }
                                TotatDisabeledButtons++;
                            }
                        }
                    }
                }
            }
        }
    }
}