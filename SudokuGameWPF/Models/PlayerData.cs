using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SudokuGameWPF.Models
{
    public enum PlayerDiffuciltySettings
    {
        Easy = 0,
        Medium = 1,
        Hard = 2,
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
            diffuciltySettings = PlayerDiffuciltySettings.Easy;
            game = new List<int[]>();
            solvedGame = new List<int[]>();
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

        private List<int[]> game;
        public List<int[]> Game
        {
            get { return game; }
            set { game = value; }
        }

        private List<int[]> solvedGame;

        public List<int[]> SolvedGame
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


        private PlayerDiffuciltySettings diffuciltySettings;
        public PlayerDiffuciltySettings DiffuciltySettings
        {
            get { return diffuciltySettings; }
            set { diffuciltySettings = value; }
        }
    }
}
