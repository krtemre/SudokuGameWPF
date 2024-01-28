using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SudokuGameWPF.Models
{
    public enum PlayerDiffuciltySettingsEnum : byte
    {
        [Description("Easy")]
        Easy = 0,
        [Description("Medium")]
        Medium = 1,
        [Description("Hard")]
        Hard = 2,
        [Description("Expert")]
        Expert = 3,
    }

    public class PlayerData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public PlayerData()
        {
            canContinue = false;
            diffuciltySetting = PlayerDiffuciltySettingsEnum.Easy;
            game = new GameData();
            solvedGame = new GameData();
        }

        private bool canContinue;
        public bool CanContinue
        {
            get { return canContinue; }
            set
            {
                if (value != canContinue)
                {
                    canContinue = value;
                    OnPropertyChanged();
                }
            }
        }

        private GameData game;
        public GameData Game
        {
            get { return game; }
            set { game = value; }
        }

        private GameData solvedGame;
        public GameData SolvedGame
        {
            get { return solvedGame; }
            set { solvedGame = value; }
        }

        private int mistakes;
        public int Mistakes
        {
            get { return mistakes; }
            set { mistakes = value; }
        }

        private PlayerDiffuciltySettingsEnum diffuciltySetting;
        public PlayerDiffuciltySettingsEnum DiffuciltySetting
        {
            get { return diffuciltySetting; }
            set { diffuciltySetting = value; }
        }
    }
}
