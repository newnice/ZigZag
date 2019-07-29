using System;
using DefaultNamespace.Sphere;
using Zenject;

namespace DefaultNamespace
{
    public class ScoreManager
    {
        private int _pathCost, _crystalCount, _fallenCount;

        private int TotalScore => _pathCost -_fallenCount * _scoreSettings.FallenPenalty +
                                  _scoreSettings.CrystalCollectCost * _crystalCount;

        private GameDifficulty _difficulty;
        private Settings _scoreSettings;
        private IScoreView _view;

        [Inject]
        public void Construct(GameDifficulty gameDifficulty, Settings settings, IScoreView view)
        {
            _difficulty = gameDifficulty;
            _scoreSettings = settings;
            _view = view;
        }

        public void UpdateByPath()
        {
            _pathCost += (int) (_difficulty.Difficulty * _scoreSettings.PathSpeedKoefficient *
                                _scoreSettings.PathUnitCost);
            _view.Path = _pathCost;
        }

        public void UpdateByCrystalCollect()
        {
            _crystalCount++;
            _view.CrystalCount = _crystalCount;
        }

        public void UpdateByFallenPenalty()
        {
            _fallenCount++;
        }

        public void Reset()
        {
            _pathCost = 0;
            _crystalCount = 0;
            _fallenCount = 0;
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