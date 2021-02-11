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
            LOADING_SCENE,
            MENU,
            PREPARE_LEVEL,
            LEVEL_INFO, // MRA: ready to play level, showing info [= PRE_GAME?]
            LEVEL_INTRO, // MRA: the proper level is setup according to its config
            RUNNING,
            GAME_OVER
        }

        private GameState _currentGameState = GameState.BOOT;
        // private GameState CurrentGameState { get { return _currentGameState; } }

        // Events
        [SerializeField] private SimpleEvent _obtainDeviceDataEvent = default;
        [SerializeField] private GameStateEvent _gameStateEvent = default;
        [SerializeField] private SceneEvent _sceneLoadedEvent = default;
        [SerializeField] private SceneEvent _loadSceneEvent = default;
        [SerializeField] private SimpleEvent _mainMenuContinueEvent = default;
        [SerializeField] private IntEvent _levelReadyEvent = default;
        [SerializeField] private HandSimpleInputEvent _activatePerformedEvent = default;
        [SerializeField] private SimpleEvent _blocksAppearCompleteEvent = default;

        private void Start()
        {
            DontDestroyOnLoad(this);
        }

        #region STATE CHANGE
        private void ChangeState(GameState toGameState)
        {
            GameState fromGameState = _currentGameState;
            _currentGameState = toGameState;

            // we may need to check from which state we are coming in order to perform to correct action
            switch (_currentGameState)
            {
                case GameState.BOOT:
                    // MRA: Complete reload of everything
                    _currentGameState = GameState.BOOT;
                    break;
                case GameState.LOADING_SCENE:
                    // MRA: We are currently waiting for a scene to be loaded
                    _currentGameState = GameState.LOADING_SCENE;
                    break;
                case GameState.MENU:
                    // MRA: We have a Menu that requires attention before we can move on
                    _currentGameState = GameState.MENU;
                    break;
                case GameState.PREPARE_LEVEL:
                    // MRA: The next Level is being prepared
                    _currentGameState = GameState.PREPARE_LEVEL;
                    break;
                case GameState.LEVEL_INFO:
                    // MRA: Showing info to the player before the level can be played					
                    _currentGameState = GameState.LEVEL_INFO;
                    break;
                case GameState.LEVEL_INTRO:
                    // MRA: Blocks appear and ambience arises before the actual gameplay starts
                    _currentGameState = GameState.LEVEL_INTRO;
                    break;
                case GameState.RUNNING:
                    // MRA: The game is being played
                    _currentGameState = GameState.RUNNING;
                    break;
                case GameState.GAME_OVER:
                    // MRA: Show game over state
                    _currentGameState = GameState.GAME_OVER;
                    break;
                default:
                    break;
            }

            _gameStateEvent?.Dispatch(_currentGameState);
            Debug.Log("[GM] cur game state: " + _currentGameState.ToString());
        }
        #endregion

        #region EVENT SUBSCRIPTIONS
        private void OnEnable()
        {
            _sceneLoadedEvent.Handler += OnSceneLoaded;
            _obtainDeviceDataEvent.Handler += OnObtainDeviceDataEvent;
            _mainMenuContinueEvent.Handler += OnContinueFromMainMenu;
            _levelReadyEvent.Handler += OnLevelReadyEvent;
            _activatePerformedEvent.Handler += OnActivatePerformed;
            _blocksAppearCompleteEvent.Handler += OnBlocksHaveAllAppeared;
        }

        private void OnDisable()
        {
            _sceneLoadedEvent.Handler -= OnSceneLoaded;
            _mainMenuContinueEvent.Handler -= OnContinueFromMainMenu;
            _levelReadyEvent.Handler -= OnLevelReadyEvent;
            _activatePerformedEvent.Handler -= OnActivatePerformed;
            _blocksAppearCompleteEvent.Handler -= OnBlocksHaveAllAppeared;
        }
        #endregion

        #region EVENT HANDLERS
        private void OnSceneLoaded(SCENE_NAME sceneName)
        {
            switch (sceneName)
            {
                case SCENE_NAME.Game:
                    // We have no Menu State at this moment, so we go to the Level Info State
                    ChangeState(GameState.MENU);
                    break;
                default:
                    break;
            }
        }

        private void OnObtainDeviceDataEvent()
        {
            _obtainDeviceDataEvent.Handler -= OnObtainDeviceDataEvent;

            ChangeState(GameState.LOADING_SCENE);

            // After obtaining the Device Data we are ready to load the menu scene
            // if we have one, else we load the Game scene
            _loadSceneEvent?.Dispatch(SCENE_NAME.Game);
        }

        private void OnContinueFromMainMenu()
        {
            ChangeState(GameState.PREPARE_LEVEL);
        }

        private void OnLevelReadyEvent(int levelNumber)
        {
            ChangeState(GameState.LEVEL_INFO);
        }

        private void OnActivatePerformed(InputManager.Hand hand)
        {
            // During intro we wait for Trigger to Press
            if (_currentGameState == GameState.LEVEL_INFO)
            {
                // Blocks can be shown now
                ChangeState(GameState.LEVEL_INTRO);
            }
        }

        private void OnBlocksHaveAllAppeared()
        {
            // MRA: All the blocks have animated into position, the game can start
            // MRA: alternatively we can start the game after the score view has appeared completely
            ChangeState(GameState.RUNNING);
        }
        #endregion
    }
}