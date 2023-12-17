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

        private void LoadGame()
        {

        }

        public PlayerData PlayerData { get; set; } = new PlayerData();

        public void NewGame()
        {
            var game = SudokuGenerator.GetSolvedSudoku();

            this.PlayerData.SolvedGame = Convert(game);

            int removeK = 0;

            switch (this.PlayerData.DiffuciltySettings)
            {
                case PlayerDiffuciltySettings.Easy:
                    removeK = 25;
                    break;
                case PlayerDiffuciltySettings.Medium:
                    removeK = 35;
                    break;
                case PlayerDiffuciltySettings.Hard:
                    removeK = 50;
                    break;
                case PlayerDiffuciltySettings.Expert:
                    removeK = 60;
                    break;
                default:
                    break;
            }

            this.PlayerData.Game = Convert(SudokuGenerator.RemoveKDigits(removeK, game));
            this.PlayerData.CanContinue = true;
            LoadGame();
        }

        public List<int[]> Convert(int[,] game)
        {
            List<int[]> converted = new List<int[]>();

            for (int i = 0; i < 9; i++)
            {
                var mat = new int[9];

                for (int j = 0; j < 9; j++)
                {
                    mat[j] = game[i, j];
                }
                converted.Add(mat);
            }
            return converted;
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
    }
}
