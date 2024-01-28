using SudokuGameWPF.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Ink;

namespace SudokuGameWPF.Models
{
    public class PlayerManager
    {
        private static object padlock = new object();
        private static PlayerManager _instance = null;

        private const string saveFilePath = @"SudokuGame.xml";

        public static PlayerManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new PlayerManager();
                    }

                    return _instance;
                }
            }
        }

        private PlayerManager() { }

        private GameData ConvertToGameData(int[,] values)
        {
            GameData gameData = new GameData();

            int gridIndex = 0;
            int valueIndex = 0;

            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    gridIndex = ((row / 3) * 3) + (col / 3);
                    valueIndex = ((row % 3) * 3) + (col % 3);

                    gameData.Grids[gridIndex].Values[valueIndex] = values[row, col];
                }
            }

            return gameData;
        }

        private void LoadGame()
        {

        }

        public PlayerData PlayerData { get; set; } = new PlayerData();

        public void NewGame()
        {
            var arraySolved = SudokuGenerator.GetSolvedSudoku();
            this.PlayerData.SolvedGame = ConvertToGameData(arraySolved);

            int removeK = 0;

            switch (this.PlayerData.DiffuciltySetting)
            {
                case PlayerDiffuciltySettingsEnum.Easy:
                    removeK = 25;
                    break;
                case PlayerDiffuciltySettingsEnum.Medium:
                    removeK = 35;
                    break;
                case PlayerDiffuciltySettingsEnum.Hard:
                    removeK = 50;
                    break;
                case PlayerDiffuciltySettingsEnum.Expert:
                    removeK = 60;
                    break;
                default:
                    break;
            }

            this.PlayerData.Game = ConvertToGameData(SudokuGenerator.RemoveKDigits(removeK, arraySolved));
            this.PlayerData.CanContinue = true;
            LoadGame();
        }

        public void Continue()
        {
            if (this.PlayerData.CanContinue) 
            {
                LoadGame();
            }
        }

        public void LoadPlayer()
        {
            string path = Path.Combine(Environment.CurrentDirectory, saveFilePath);
            if (File.Exists(path))
            {
                this.PlayerData = (PlayerData)XmlHelper.LoadFromXML(typeof(PlayerData), path);
            }
        }

        public void SavePlayer()
        {
            string path = Path.Combine(Environment.CurrentDirectory, saveFilePath);
            if (!XmlHelper.SaveToXML(path, this.PlayerData, typeof(PlayerData)))
            {
                MessageBox.Show("An error occured!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public bool ControlIsValueTrue(int row, int col, int num)
        {
            if (PlayerData.SolvedGame == null)
            {
                return false;
            }

            int gridIndex = ((row / 3) * 3) + (col / 3);
            int index = ((row % 3) * 3) + (col % 3);

            return PlayerData.SolvedGame.Grids[gridIndex].Values[index] == num;
        }

        public void UpdateValue(int row, int col, int num)
        {
            if (PlayerData.Game == null)
            {
                return;
            }

            int gridIndex = ((row / 3) * 3) + (col / 3);
            int index = ((row % 3) * 3) + (col % 3);

            PlayerData.Game.Grids[gridIndex].Values[index] = num;
        }
    }
}
