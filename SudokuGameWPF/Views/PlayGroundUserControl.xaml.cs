using SudokuGameWPF.Models;
using SudokuGameWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

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
        private DispatcherTimer confettiTimer;

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
            confettiTimer = new DispatcherTimer();
            confettiTimer.Tick += ConfettiTimerTick;
            confettiTimer.Interval = TimeSpan.FromMilliseconds(100);
        }

        public void UpdatePlayground()
        {
            WinGrid.Visibility = System.Windows.Visibility.Hidden;
            confetiCount = 0;
            UpdateGrids(PlayerManager.Instance.PlayerData.Game);

            if (lastSelectorBtn != null)
            {
                lastSelectorBtn.IsDefault = false;
            }
            lastSelectorBtn = null;
            lastPlaygroundBtn = null;
            confettiTimer.Stop();
        }

        #region Buttons
        private void Playground_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is PlaygroundButton playgroundBtn)
            {
                playgroundButtonPressedTime = DateTime.UtcNow;

                if (lastSelectorBtn == null || !lastSelectorBtn.IsSelected)
                    DeHighlightAll();

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
                    if (lastPlaygroundBtn == null || !lastPlaygroundBtn.IsSelected)
                    {
                        DeHighlightAll();
                    }
                    else
                    {
                        TryUpdatePlayGround();
                    }
                    selectorBtn.IsSelected = false;
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

                int index = lastPlaygroundBtn.GridPosition_X * 3 + lastPlaygroundBtn.GridPosition_Y;

                PlayerManager.Instance.PlayerData.Game.Grids[lastPlaygroundBtn.GridIndex].Values[index] = value;

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
            if (lastSelectorBtn == null) { return; }

            if (TotalValuesInGame.ContainsKey(lastSelectorBtn.Value))
            {
                if (TotalValuesInGame[lastSelectorBtn.Value] >= 9)
                {
                    lastSelectorBtn.Completed = true;
                    TotatDisabeledButtons++;
                    lastSelectorBtn = null;
                }
            }

            if (TotatDisabeledButtons >= 9)
            {
                PlayerWon();
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
                        btn.HasDefaultValue = gameData.Grids[i].DefaultValue[j];

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
                                if (SelectorButtonsGrid.Children[val - 1] is PlaygroundSelectorButton playgroundSelectorButton)
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

        private void ConfettiTimerTick(object sender, EventArgs e)
        {
            PlayConfettiCanvas();
        }

        private void PlayerWon()
        {
            WinGrid.Visibility = System.Windows.Visibility.Visible;
            confettiTimer.Start();
        }

        private void PlayAgain_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            PlayerManager.Instance.NewGame();
            UpdatePlayground();
        }

        int confetiCount = 0;
        Random _Random = new Random();

        private void PlayConfettiCanvas()
        {
            double size = _Random.Next(10, 20);

            Rectangle confettiPiece = new Rectangle
            {
                Width = size * 2,
                Height = size,
                Fill = PickBrush(),
            };

            confettiCanvas.Children.Add(confettiPiece);

            double startX = _Random.NextDouble() * confettiCanvas.ActualWidth;
            double startY = 0;

            Canvas.SetLeft(confettiPiece, startX);
            Canvas.SetTop(confettiPiece, startY);

            var duration = TimeSpan.FromSeconds(_Random.Next(1, 4));

            DoubleAnimation doubleAnimationY = new DoubleAnimation
            {
                To = confettiCanvas.ActualHeight + 50,
                Duration = duration,
            };

            DoubleAnimation doubleAnimationRotation = new DoubleAnimation
            {
                To = _Random.Next(360), // Rastgele bir dönme miktarı
                Duration = duration,
            };

            confettiPiece.RenderTransformOrigin = new Point(0.5, 0.5); // Döndürme etrafındaki orijin noktası
            confettiPiece.RenderTransform = new RotateTransform();

            Storyboard.SetTarget(doubleAnimationY, confettiPiece);
            Storyboard.SetTarget(doubleAnimationRotation, confettiPiece);

            Storyboard.SetTargetProperty(doubleAnimationY, new PropertyPath(Canvas.TopProperty));
            Storyboard.SetTargetProperty(doubleAnimationRotation, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));

            Storyboard storyboard = new Storyboard();
            storyboard.Completed += (sender, e) => confettiCanvas.Children.Remove(confettiPiece);
            storyboard.Children.Add(doubleAnimationY);
            storyboard.Children.Add(doubleAnimationRotation);

            storyboard.Begin();
        }

        private void PlayConfetti()
        {
            double x = _Random.Next(10, (int)WinGrid.ActualWidth - 10);
            double y = 0;
            double s = _Random.Next(5, 15) * .1;
            double r = _Random.Next(0, 270);

            TranslateTransform translate = new TranslateTransform();
            translate.X = x;
            translate.Y = y;

            RotateTransform rotate = new RotateTransform();
            rotate.Angle = r;

            ScaleTransform scale = new ScaleTransform();
            scale.ScaleX = s;
            scale.ScaleY = s;

            TransformGroup transformGroup = new TransformGroup();
            transformGroup.Children.Add(translate);
            transformGroup.Children.Add(rotate);
            transformGroup.Children.Add(scale);

            var confetti = new Confetti
            {
                RenderTransform = transformGroup,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                VerticalAlignment = System.Windows.VerticalAlignment.Top,
                Background = PickBrush(),
            };
            WinGrid.Children.Add(confetti);
            confetiCount++;

            var duration = TimeSpan.FromSeconds(_Random.Next(1, 4));

            y = WinGrid.ActualHeight + 100;
            var ay = new DoubleAnimation
            {
                To = y,
                Duration = duration,
            };
            Storyboard.SetTarget(ay, confetti.RenderTransform);
            Storyboard.SetTargetProperty(ay, new System.Windows.PropertyPath(TranslateTransform.YProperty));

            //r += _Random.Next(90, 360);
            //var ar = new DoubleAnimation
            //{
            //    To = r,
            //    Duration = duration,
            //};
            //Storyboard.SetTarget(ar, confetti.RenderTransform);
            //Storyboard.SetTargetProperty(ay, new System.Windows.PropertyPath(RotateTransform.AngleProperty));

            //Storyboard storyboard = new Storyboard();
            //storyboard.Completed += (sender, e) => WinGrid.Children.Remove(confetti);
            //storyboard.Children.Add(ay);
            //storyboard.Children.Add(ar);
            //storyboard.Begin();
        }

        private Brush PickBrush()
        {
            Brush result = Brushes.Transparent;

            Random rnd = new Random();

            Type brushesType = typeof(Brushes);

            PropertyInfo[] properties = brushesType.GetProperties();

            int random = rnd.Next(properties.Length);
            result = (Brush)properties[random].GetValue(null, null);

            return result;
        }
    }
}