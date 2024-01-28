using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGameWPF.Models
{
    [Serializable]
    public class GameData
    {
        public int GridCount { get; set; }
        public int GridRowCount {  get; set; }
        public int GridColCount {  get; set; }

        public List<GridData> Grids { get; set; }

        public GameData() 
        {
            GridCount = 9;
            GridRowCount = 3;
            GridColCount = 3;

            CreateGrids();
        }

        private void CreateGrids()
        {
            if(Grids == null)
                Grids = new List<GridData>();
            else
                Grids.Clear();

            for (int i = 0; i < GridCount; i++)
            {
                Grids.Add(new GridData(i, GridColCount * GridRowCount));
            }
        }
    }

    [Serializable]
    public class GridData
    {
        public int GridIndex { get; set; }

        public int[] Values { get; set; }

        public GridData(int gridIndex, int totalValuesNumber) 
        {
            GridIndex = gridIndex;
            Values = new int[totalValuesNumber];
        }
    }
}
