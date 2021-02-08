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
        [SerializeField] private LevelModel _model = default;
        [SerializeField] private GameStateEvent _gameStateEvent = default;
        [SerializeField] private IntEvent _levelReadyEvent = default;
        [SerializeField] private LevelConfigEvent _prepareLevelEvent = default;
        
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
        }

        private void OnDisable()
        {
            _gameStateEvent.Handler -= OnGameStateEvent;
        }

        private void OnGameStateEvent(GameManager.GameState state)
        {
            Debug.Log("[LevelController] new game state: " + state.ToString());
            switch (state)
            {
                case GameManager.GameState.LOADING_LEVEL:
                    // preparing & setting up the level
                    _curLevelConfig = _model.GetCurrentLevel();
                    _prepareLevelEvent?.Dispatch(_curLevelConfig);
                    PrepareLevelEvent();
                    break;
            }
        }

        private void PrepareLevelEvent()
        {
            _view.PrepareNewLevel(_curLevelConfig);
            _view.gameObject.SetActive(true); // MRA do this in view
        }
        #endregion
    }
}