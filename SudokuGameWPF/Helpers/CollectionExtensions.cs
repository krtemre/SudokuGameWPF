using SudokuGameWPF.Models;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;


namespace SudokuGameWPF.Helpers
{
    public static class CollectionExtensions
    {
        public static bool DoesGridExists(this ObservableCollection<SudokuGridModel> collection, SudokuGridModel grid)
        {
            if (grid == null)
            {
                return true;
            }

            foreach (var item in collection)
            {
                if (item.Index_X == grid.Index_X && item.Index_Y == grid.Index_Y)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
