using System;
using System.Collections;
using System.Collections.Generic;
using nl.allon.events;
using nl.allon.utils;
using UnityEngine;
using UnityEngine.iOS;

namespace nl.allon.managers
{
    public class GameManager : BaseSingleton<GameManager>
    {
        public enum GameState
        {
            BOOT, 
            LOADING,
            MENU,
            LOADING_LEVEL, // MRA: the proper level is setup according to its config
            LEVEL_INFO, // MRA: ready to play level, showing info [= PRE_GAME?]
            RUNNING,
            GAME_OVER
        }

        private GameState _currentGameState = GameState.BOOT;
        public GameState CurrentGameState { get { return _currentGameState; } }

        // Events
        [SerializeField] private SimpleEvent _obtainDeviceDataEvent = default;
        [SerializeField] private GameStateEvent _gameStateEvent = default;
        [SerializeField] private SceneEvent _sceneLoadedEvent = default;
        [SerializeField] private SceneEvent _loadSceneEvent = default;
        [SerializeField] private IntEvent _levelReadyEvent = default;
        private void Start()
        {
            DontDestroyOnLoad(this);
        }

        private void ChangeState(GameState toGameState)
        {
            GameState fromGameState = _currentGameState;
            _currentGameState = toGameState;

            // we may need to check from which state we are coming in order to perform to correct action
            switch (_currentGameState)
            {
                case GameState.BOOT:
                    // MRA: complete reload of everything
                    _currentGameState = GameState.BOOT;
                    break;
                case GameState.LOADING:
                    // MRA: we are preparing the (next) level
                    _currentGameState = GameState.LOADING;
                    break;
                case GameState.MENU:
                    // MRA: returning to the pre-game menu
                    _currentGameState = GameState.MENU;
                    break;
                case GameState.LOADING_LEVEL:
                    // MRA: returning to the pre-game menu
                    _currentGameState = GameState.LOADING_LEVEL;
                    break;
                case GameState.LEVEL_INFO:
                    // MRA: showing info to player before the level can be played					
                    _currentGameState = GameState.LEVEL_INFO;
                    // listen for user input
                    // _leftTriggerDownEvent.Handler += OnTriggerDown;
                    // _rightTriggerDownEvent.Handler += OnTriggerDown;
                    break;
                case GameState.RUNNING:
                    // MRA: put everything in place to start playing
                    _currentGameState = GameState.RUNNING;
                    break;
                case GameState.GAME_OVER:
                    // MRA: show game over state
                    _currentGameState = GameState.GAME_OVER;
                    break;
                default:
                    break;
            }

            _gameStateEvent?.Dispatch(_currentGameState);
            Debug.Log("[GM] cur game state: "+_currentGameState.ToString());
        }

        #region EVENTS
        private void OnEnable()
        {
            _sceneLoadedEvent.Handler += OnSceneLoaded;
            _obtainDeviceDataEvent.Handler += OnObtainDeviceDataEvent;
        }

        private void OnDisable()
        {
            _sceneLoadedEvent.Handler -= OnSceneLoaded;
        }

        private void OnSceneLoaded(SCENE_NAME sceneName)
        {
            switch (sceneName) {
                case SCENE_NAME.Game:
                    // Debug.Log("GM: loaded: "+sceneName.ToString());
                    // We have no Menu State at this moment, so we go to the Level Info State
                    ChangeState(GameState.LOADING_LEVEL);
                    break;
                default:
                    break;
            }
        }

        private void OnObtainDeviceDataEvent()
        {
            _obtainDeviceDataEvent.Handler -= OnObtainDeviceDataEvent;
            
            ChangeState(GameState.LOADING);
            
            // After obtaining the Device Data we are ready to load the menu scene
            // if we have one, else we load the Game scene
            _loadSceneEvent?.Dispatch(SCENE_NAME.Game);
        }
        #endregion
    }
}