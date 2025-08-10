using System;
using GameControllers.Views;
using SaveSystems;
using SaveSystems.DataTypes;
using UnityEngine;

namespace GameControllers.Controllers
{
    public class GameLevelHandler
    {
        private readonly SaveSystem _saveSystem;
        private readonly int _indexCurrentLevel;
        private readonly GameLevelHandlerView _gameLevelHandlerView;
        
        private int _countEnemies;
        private Transform _ninjaTransform;

        public event Action<Transform> OnWinLevel;
        public event Action<Transform> OnLoseLevel; 

        public GameLevelHandler(
            SaveSystem saveSystem, 
            int indexCurrentLevel,
            GameLevelHandlerView gameLevelHandlerView)
        {
            _saveSystem = saveSystem;
            _indexCurrentLevel = indexCurrentLevel;
            _gameLevelHandlerView = gameLevelHandlerView;
        }

        public void IncrementEnemies()
        {
            _countEnemies++;
        }

        public void DecrementEnemies()
        {
            _countEnemies--;
            CheckGameState();
        }

        public void InitNinjaTransform(Transform ninjaTransform)
        {
            _ninjaTransform = ninjaTransform;
        }

        public void DiePlayer()
        {
            OnLoseLevel?.Invoke(_ninjaTransform);
            _gameLevelHandlerView.ShowLoseBlock();
        }

        private void CheckGameState()
        {
            if (_countEnemies <= 0)
            {
                OnWinLevel?.Invoke(_ninjaTransform);
                _gameLevelHandlerView.ShowWinBlock();
                
                if (LevelsSaveData.LastLevelIndex >= _indexCurrentLevel + 1)
                {
                    _saveSystem.SaveData<LevelsSaveData, bool>(
                        true, $"{LevelsSaveData.GUIDLevelState}{_indexCurrentLevel + 1}");
                }
            }
        }
    }
}
