using System;
using System.Collections;
using System.Collections.Generic;
using nl.allon.configs;
using nl.allon.events;
using nl.allon.managers;
using nl.allon.models;
using nl.allon.utils;
using nl.allon.views;
using UnityEngine;

namespace nl.allon.controllers
{
    public class LevelController : BaseSingleton<LevelController>
    {
        // events
        [SerializeField] private GameStateEvent _gameStateEvent = default;
        [SerializeField] private IntEvent _levelReadyEvent = default;
        [SerializeField] private LevelConfigEvent _prepareLevelEvent = default;
        [SerializeField] private SimpleEvent _blocksAreSetupEvent = default;
        
        [SerializeField] private LevelModel _model = default;
        
        private LevelConfig _curLevelConfig;
        private LevelView _view = null;
        private LevelInfoView _infoView = null;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            _view = Instantiate(new GameObject("LevelView"), transform).AddComponent<LevelView>();
        }

        #region EVENTS
        private void OnEnable()
        {
            _gameStateEvent.Handler += OnGameStateEvent;
            _blocksAreSetupEvent.Handler += OnBlocksAreSetup;
        }

        private void OnDisable()
        {
            _gameStateEvent.Handler -= OnGameStateEvent;
            _blocksAreSetupEvent.Handler -= OnBlocksAreSetup;
        }

        private void OnGameStateEvent(GameManager.GameState state)
        {
            switch (state)
            {
                case GameManager.GameState.PREPARE_LEVEL:
                    // preparing & setting up the level
                    PrepareLevel();
                    break;
                case GameManager.GameState.LEVEL_INTRO:
                    // Remove LevelInfo
                    _infoView.Hide();
                    break;
                default:
                    break;
            }
        }

        private void OnBlocksAreSetup()
        {
            // If the BlocksManager is ready we can dispatch the ready event
            _levelReadyEvent.Dispatch(_curLevelConfig.LevelNum);
            Debug.Log("[LC] OnBlocksManager Ready");
        }
        #endregion
        
        #region HELPER METHODS
        private void PrepareLevel()
        {
            _curLevelConfig = _model.GetCurrentLevel();
            _prepareLevelEvent?.Dispatch(_curLevelConfig);
           
            if (_infoView == null)
            {
                _infoView = Instantiate(_curLevelConfig.LevelInfoPrefab, transform).GetComponent<LevelInfoView>();
            }
            
            _infoView.SetInfo(_curLevelConfig);
            _infoView.Appear();
            
            _view.PrepareNewLevel(_curLevelConfig);
        }
        #endregion
    }
}