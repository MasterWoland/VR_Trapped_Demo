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
            LOADING_LEVEL,
            MENU,
            LEVEL_INFO, // MRA: ready to play level, showing info
            RUNNING,
            GAME_OVER
        }

        private GameState _currentGameState = GameState.BOOT;
        public GameState CurrentGameState { get { return _currentGameState; } }

        // Events
        [SerializeField] private SimpleEvent _obtainDeviceDataEvent;
        [SerializeField] private SceneEvent _sceneLoadedEvent;

        private void Start()
        {
            DontDestroyOnLoad(this);
        }
        
        #region EVENTS
        private void OnEnable()
        {
            _obtainDeviceDataEvent.Handler += OnObtainDeviceDataEvent;
        }

        private void OnDisable()
        {
        }

        private void OnObtainDeviceDataEvent()
        {
            _obtainDeviceDataEvent.Handler -= OnObtainDeviceDataEvent;
            
            // load scene (game)
            SceneLoadManager.Instance.LoadSceneAsync(SCENE_NAME.Game); // MRA: do this with event
        }
        #endregion
    }
}