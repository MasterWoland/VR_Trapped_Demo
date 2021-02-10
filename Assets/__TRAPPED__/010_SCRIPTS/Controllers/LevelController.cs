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
        [SerializeField] private SimpleEvent _blocksManagerReadyEvent = default;
        
        [SerializeField] private LevelModel _model = default;
        
        private LevelConfig _curLevelConfig;
        private LevelView _view = default;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            _view = Instantiate(new GameObject("LevelView"), transform).AddComponent<LevelView>();
            _view.gameObject.SetActive(false); //MRA do this in view
        }

        #region EVENTS
        private void OnEnable()
        {
            _gameStateEvent.Handler += OnGameStateEvent;
            _blocksManagerReadyEvent.Handler += OnBlocksManagerReady;
        }

        private void OnDisable()
        {
            _gameStateEvent.Handler -= OnGameStateEvent;
            _blocksManagerReadyEvent.Handler -= OnBlocksManagerReady;
        }

        private void OnGameStateEvent(GameManager.GameState state)
        {
            Debug.Log("[LevelController] new game state: " + state.ToString());
            switch (state)
            {
                case GameManager.GameState.LEVEL_INTRO:
                    // preparing & setting up the level
                    _curLevelConfig = _model.GetCurrentLevel();
                    _prepareLevelEvent?.Dispatch(_curLevelConfig);
                    PrepareLevelEvent();
                    break;
                case GameManager.GameState.RUNNING:
                    // preparing & setting up the level
                    // Debug.Log("[LevelController] Game has started");
                    break;
            }
        }

        private void PrepareLevelEvent()
        {
            _view.PrepareNewLevel(_curLevelConfig);
        }

        private void OnBlocksManagerReady()
        {
            // If the BlocksManager is ready we can dispatch the ready event
            _levelReadyEvent.Dispatch(_curLevelConfig.LevelNum);
        }
        #endregion
    }
}