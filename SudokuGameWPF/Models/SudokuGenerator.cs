using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGameWPF.Models
{
    public class SudokuGenerator
    {
        private static int[,] mat;

        static public int[,] GetSolvedSudoku()
        {
            mat = new int[9, 9];

            FillValues();

            return mat;
        }

        static public int[,] RemoveKDigits(int k, int[,] mat)
        {
            int count = k;
            while (count != 0)
            {
                int cellId = RandomGenerator(81) - 1;

                int i = (cellId / 9);
                int j = cellId % 9;

                if (mat[i, j] != 0)
                {
                    count--;
                    mat[i, j] = 0;
                }
            }

            return mat;
        }

        static private void FillValues()
        {
            FillDiagonal();
            FillRemaining(0, 3);
        }

        static private void FillDiagonal()
        {
            for (int i = 0; i < 9; i += 3)
            {
                FillBox(i, i);
            }
        }

        static public void PrintSudoku(int[,] mat)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (mat[i, j] != 0)
                        Console.Write(mat[i, j] + " ");
                    else
                        Console.Write("  ");
                }

                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static private void FillBox(int row, int col)
        {
            int num;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    do
                    {
                        num = RandomGenerator(9);
                    } while (!UnusedInBox(row, col, num));

                    mat[row + i, col + j] = num;
                }
            }
        }

        static private bool UnusedInBox(int rowStart, int colStart, int num)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (mat[rowStart + i, colStart + j] == num)
                        return false;

            return true;
        }

        static private int RandomGenerator(int num)
        {
            Random rend = new Random();
            return (int)Math.Floor((double)rend.NextDouble() * num + 1);
        }

        static private bool CheckIfSafe(int i, int j, int num)
        {
            return (UnusedInRow(i, num) &&
                    UnusedInColumn(j, num) &&
                    UnusedInBox(i - i % 3, j - j % 3, num));
        }

        static private bool UnusedInRow(int row, int num)
        {
            for (int i = 0; i < 9; i++)
                if (mat[row, i] == num)
                    return false;

            return true;
        }

        static private bool UnusedInColumn(int col, int num)
        {
            for (int i = 0; i < 9; i++)
                if (mat[i, col] == num)
                    return false;

            return true;
        }

        static private bool FillRemaining(int i, int j)
        {
            if (j >= 9 && i < 9 - 1)
            {
                i = i + 1;
                j = 0;
            }
            if (i >= 9 && j >= 9)
                return true;

            if (i < 3)
            {
                if (j < 3)
                    j = 3;
            }
            else if (i < 9 - 3)
            {
                if (j == (int)(i / 3) * 3)
                    j = j + 3;
            }
            else
            {
                if (j == 9 - 3)
                {
                    i = i + 1;
                    j = 0;
                    if (i >= 9)
                        return true;
                }
            }

            for (int num = 1; num <= 9; num++)
            {
                if (CheckIfSafe(i, j, num))
                {
                    mat[i, j] = num;
                    if (FillRemaining(i, j + 1))
                        return true;

                    mat[i, j] = 0;
                }
            }

            return false;
        }
    }

    public static class MySudokuGenerator
    {
        private static GameData gameData;

        public static GameData GetSolvedGame()
        {
            if(gameData != null)
            {
                gameData = null;
            }

            gameData = new GameData();
            FillValues();
            return gameData;
        }

        public static GameData RemoveKDigits(int k, GameData game)
        {
            GameData removedGame = new GameData();
            int count = k;
            while (count != 0)
            {
                int cellId = RandomGenerator(81) - 1;

                int i = (cellId / 9);
                int j = cellId % 9;



                if (GetValue(removedGame, i, j) != 0)
                {
                    count--;
                    UpdateGridValue(removedGame, i, j, 0);
                }
            }

            return removedGame;
        }

        private static void FillValues()
        {
            FillDiagonal();
        }

        private static void FillDiagonal()
        {
            for (int i = 0; i < 9; i += 4)
            {
                FillBox(i);
            }
        }

        private static void FillBox(int gridIndex)
        {
            GridData grid = gameData.Grids[gridIndex];

            int num;
            for (int i = 0; i < 9; i++)
            {
                do
                {
                    num = RandomGenerator(9);

                } while (!UnusedInBox(grid, num));

                grid.Values[gridIndex] = num;
            }
        }

        private static bool FillRemaining(int i, int j)
        {
            if (j >= 9 && i < 9 - 1)
            {
                i = i + 1;
                j = 0;
            }
            if (i >= 9 && j >= 9)
                return true;

            if (i < 3)
            {
                if (j < 3)
                    j = 3;
            }
            else if (i < 9 - 3)
            {
                if (j == (int)(i / 3) * 3)
                    j = j + 3;
            }
            else
            {
                if (j == 9 - 3)
                {
                    i = i + 1;
                    j = 0;
                    if (i >= 9)
                        return true;
                }
            }

            for (int num = 1; num <= 9; num++)
            {
                if (CheckIfSafe(i, j, num))
                {
                    UpdateGridValue(i, j, num);
                    if (FillRemaining(i, j + 1))
                        return true;

                    UpdateGridValue(i, j, 0);
                }
            }

            return false;
        }

        #region Check & Random Generator
        private static int RandomGenerator(int num)
        {
            Random rend = new Random();
            return (int)Math.Floor((double)rend.NextDouble() * num + 1);
        }

        private static bool UnusedInBox(GridData box, int num)
        {
            for (int i = 0; i < box.Values.Length; i++)
            {
                if (box.Values[i] == num)
                    return false;
            }

            return true;
        }

        private static bool CheckIfSafe(int i, int j, int num)
        {
            return (UnusedInRow(i, num) &&
                    UnusedInColumn(j, num) &&
                    UnusedInBox(GetGrid(i - i % 3, j - j % 3), num));
        }

        private static bool UnusedInRow(int row, int num)
        {
            int gridRow = row % 3; //0, 1 and 2 max
            int gridStartIndex = (row / 3) * 3;//starts 0, 3 and 6

            for (int i = gridStartIndex; i < gridStartIndex + 3; i++)
            {
                int index = gridRow * 3;
                if (gameData.Grids[i].Values[index] == num)
                    return false;
                else if (gameData.Grids[i].Values[index + 1] == num)
                    return false;
                else if (gameData.Grids[i].Values[index + 2] == num)
                    return false;
            }

            return true;
        }

        private static bool UnusedInColumn(int col, int num)
        {
            int gridCol = col % 3;
            int gridStartIndex = col / 3; //0, 1 and 2 max

            for (int i = gridStartIndex; i <= gridStartIndex + 6; i+=3) 
            {
                if (gameData.Grids[i].Values[gridCol] != num) 
                    return false;
                else if (gameData.Grids[i].Values[gridCol + 3] != num)
                    return false;
                else if (gameData.Grids[i].Values[gridCol + 6] != num)
                    return false;
            }

            return true;
        }

        private static GridData GetGrid(int row, int col)
        {
            int index = (row / 3) * 3;
            index += col / 3;

            return gameData.Grids[index];
        }

        private static void UpdateGridValue(int row, int col, int num)
        {
            int gridIndex = ((row / 3) * 3) + (col / 3);
            int index = ((row % 3) * 3) + (col % 3);

            gameData.Grids[gridIndex].Values[index] = num;
        }

        private static int GetValue(GameData gameData, int row, int col) 
        {
            int gridIndex = ((row / 3) * 3) + (col / 3);
            int index = ((row % 3) * 3) + (col % 3);

            return gameData.Grids[gridIndex].Values[index];
        }

        private static void UpdateGridValue(GameData gameData, int row, int col, int num)
        {
            int gridIndex = ((row / 3) * 3) + (col / 3);
            int index = ((row % 3) * 3) + (col % 3);

            gameData.Grids[gridIndex].Values[index] = num;
        }
        #endregion
    }
}
