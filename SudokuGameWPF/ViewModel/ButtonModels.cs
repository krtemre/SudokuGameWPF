using System.Windows.Controls;
using System.Windows.Media;

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
        private readonly SolidColorBrush SelectedColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A518DCFF"));
        private readonly SolidColorBrush NormalColor = Brushes.Black;
        #endregion

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
                if (isSelected != value)
                {
                    isSelected = value;

                    IsDefault = isSelected;
                    Background = isSelected ? SelectedColor : NormalColor;
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