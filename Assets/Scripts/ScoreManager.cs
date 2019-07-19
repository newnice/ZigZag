using System;
using DefaultNamespace.Sphere;

namespace DefaultNamespace
{
    public class ScoreManager
    {
        private int CurrentScore { get; set; }
        private GameDifficulty _difficulty;
        private Settings _scoreSettings;

        public ScoreManager(GameDifficulty gameDifficulty, Settings settings)
        {
            _difficulty = gameDifficulty;
            _scoreSettings = settings;
        }

        public void UpdateByPath()
        {
            CurrentScore += (int) (_scoreSettings.PathUnitCost * _scoreSettings.PathSpeedKoefficient *
                                   _difficulty.Difficulty);
        }

        public void UpdateByCrystalCollect()
        {
            CurrentScore += _scoreSettings.CrystalCollectCost;
        }

        public void UpdateByFallenPenalty()
        {
            CurrentScore += _scoreSettings.FallenPenalty;
        }

        public void Reset()
        {
            CurrentScore = 0;
        }

        [Serializable]
        public class Settings
        {
            public int CrystalCollectCost = 5;
            public int PathUnitCost = 1;
            public float PathSpeedKoefficient = 1;
            public int FallenPenalty = -50;
        }
    }
}