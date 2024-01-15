using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SudokuGameWPF.Helpers;
using SudokuGameWPF.Models;

namespace SudokuGameWPF.ViewModel
{
    public class SudokuPlaygroundVM
    {

        public SudokuPlaygroundVM() { }

        public void AddGrid(SudokuGridModel grid)
        {
            if(grid != null && Grids.DoesGridExists(grid))
            {
                Grids.Add(grid);
            }
        }

        public ObservableCollection<SudokuGridModel> Grids { get; private set; } = new ObservableCollection<SudokuGridModel>();
    }
}
