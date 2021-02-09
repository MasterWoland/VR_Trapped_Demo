using System.Collections;
using System.Collections.Generic;
using nl.allon.configs;
using UnityEngine;

namespace nl.allon.models
{
    /// <summary>
    /// Data of all levels. Contains config files of all levels.
    /// </summary>
    [CreateAssetMenu(fileName = "LevelModel", menuName = "Model/LevelModel")]
    public class LevelModel:ScriptableObject
    {
        [SerializeField] private LevelConfig[] _levelConfigs = default;
        private int _currentLevelIndex = 0;
        public int CurrentLevelIndex { get { return _currentLevelIndex; } }

        public LevelConfig GetCurrentLevel()
        {
            return _levelConfigs[_currentLevelIndex];
        }

        public void GoToNextLevel()
        {
            if (HasNextLevel())
            {
                _currentLevelIndex++;
            }
            else
            {
                // MRA: for now we return to the first level after all levels are played
                // MRA: we may want to change this

                Debug.LogWarning("[LevelModel] All levels are played. We return to the first level now.");
                Reset();
            }
        }

        #region HELPER METHODS
        private void Reset()
        {
            _currentLevelIndex = 0;
        }

        private bool HasNextLevel()
        {
            return _levelConfigs.Length > _currentLevelIndex + 1;
        }
        #endregion
    }
}