using System.Collections.Generic;
using System.Windows.Controls;

namespace SudokuGameWPF.Models
{
    public class SudokuGridModel
    {
        public SudokuGridModel() { }

        public SudokuGridModel(int x, int y, Grid grid)
        {
            Index_X = x;
            Index_Y = y;
            VisualGrid = grid;
            Buttons = new List<Button>();
        }

        public int Index_X { get; set; }
        public int Index_Y { get; set; }
        public Grid VisualGrid { get; set; }
        public List<Button> Buttons { get; set; }
    }
}
