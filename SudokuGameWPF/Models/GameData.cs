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
        public int GridCount { get; set; } = 9;
        public int GridRowCount { get; set; } = 3;
        public int GridColCount { get; set; } = 3;

        public List<GridData> Grids { get; set; }

        public GameData() { }

        public void CreateGrids()
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
        public GridData() { }

        public int GridIndex { get; set; }

        public int[] Values { get; set; }

        public bool[] DefaultValue { get; set; }

        public GridData(int gridIndex, int totalValuesNumber) 
        {
            GridIndex = gridIndex;
            Values = new int[totalValuesNumber];
            DefaultValue = new bool[totalValuesNumber];
        }

        public GridData(int gridIndex, int[] values, bool[] defaultValue)
        {
            GridIndex = gridIndex;
            Values = values;
            DefaultValue = defaultValue;
        }   
    }
}
