using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace SudokuGameWPF.ViewModel
{
    public class PlaygroundButton : Button
    {
        public int GridIndex { get; set; }
        public int GridPosition_X { get; set; }
        public int GridPosition_Y { get; set; }

        public int Row
        {
            get
            {
                return ((GridIndex / 3) * 3) + GridPosition_X;
            }
        }

        public int Col
        {
            get
            {
                return (GridIndex % 3) * 3 + GridPosition_Y;
            }
        }

        private bool hasDefaultValue;
        public bool HasDefaultValue
        {
            get { return hasDefaultValue; }
            set
            {
                hasDefaultValue = value;
                Foreground = hasDefaultValue ? NormalForegroundColor : ForegroundColor;
            }
        }

        private int value;
        public int Value
        {
            get { return value; }
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    Content = this.value == 0 ? "" : this.value.ToString();
                }
            }
        }

        #region Colors
        private readonly SolidColorBrush SelectedColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A518DCFF"));
        private readonly SolidColorBrush NormalColor = Brushes.Gainsboro;
        private readonly SolidColorBrush NormalForegroundColor = Brushes.Black;
        private readonly SolidColorBrush ForegroundColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7158e2"));
        #endregion

        private bool isSelected;
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;

                    IsDefault = isSelected;
                    Background = isSelected ? SelectedColor : NormalColor;
                }
            }
        }

        private bool inHighlightArea;
        public bool InHighlightArea
        {
            get
            {
                return inHighlightArea;
            }
            set
            {
                inHighlightArea = value;

                if (!isSelected)
                {
                    IsDefault = inHighlightArea;
                    Background = inHighlightArea ? SelectedColor : NormalColor;
                }
            }
        }
    }

    public class PlaygroundSelectorButton : Button
    {
        #region Colors
        private readonly SolidColorBrush SelectedColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF10329C"));
        private readonly SolidColorBrush NormalColor = Brushes.Black;
        private readonly SolidColorBrush DisabledColor = Brushes.Gray;
        private readonly SolidColorBrush DisabledForeground = Brushes.LightGray;
        #endregion

        private Storyboard holdStoryboard;

        public PlaygroundSelectorButton()
        {
            holdStoryboard = new Storyboard();

            ColorAnimation colorAnimation = new ColorAnimation
            {
                To = SelectedColor.Color,
                Duration = new System.Windows.Duration(TimeSpan.FromSeconds(1)),
            };

            Storyboard.SetTarget(colorAnimation, this);
            Storyboard.SetTargetProperty(colorAnimation, new System.Windows.PropertyPath("(Button.Background).(SolidColorBrush.Color)"));

            holdStoryboard.Children.Add(colorAnimation);

            this.PreviewMouseLeftButtonDown += PlaygroundSelectorButton_PreviewMouseLeftButtonDown;
            this.PreviewMouseLeftButtonUp += PlaygroundSelectorButton_PreviewMouseLeftButtonUp;
        }

        private void PlaygroundSelectorButton_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            holdStoryboard.Stop();
        }

        private void PlaygroundSelectorButton_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            holdStoryboard.Begin();
        }

        private int value;
        public int Value
        {
            get { return value; }
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    Content = this.value.ToString();
                }
            }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                isSelected = value;

                IsDefault = isSelected;
                Background = isSelected ? SelectedColor : NormalColor;
            }
        }

        private bool completed;
        public bool Completed
        {
            get { return completed; }
            set
            {
                completed = value;
                this.IsEnabled = !value;

                if (completed)
                {
                    isSelected = false;
                    this.Background = DisabledColor;
                    this.Foreground = DisabledForeground;
                }
            }
        }
    }

    public class PlaygroundGrid : Grid
    {
        public int GridIndex { get; set; }

        public bool HasRow(int row)
        {
            return (row / 3) == (GridIndex / 3);
        }

        public bool HasColumn(int column)
        {
            return (column / 3) == (GridIndex % 3);
        }
    }
}