using System;
using System.Collections;
using System.Collections.Generic;
using nl.allon.configs;
using nl.allon.data;
using nl.allon.events;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

namespace nl.allon.managers
{
    /// <summary>
    /// Applies the correct controller or hand object to the hand
    /// </summary>
    public class HandManager : MonoBehaviour
    {
        public enum Hand
        {
            LEFT,
            RIGHT
        }
        public Hand CurrentHand = default;

        // Events
        [SerializeField] private GameStateEvent _gameStateEvent = default;

        [SerializeField] private DeviceData _deviceData = default;
        [SerializeField] private GameObject _controllerQuestPrefab = default;
        [SerializeField] private GameObject _controllerRiftPrefab = default;
        [SerializeField] private GameObject _controllerVivePrefab = default;
        [SerializeField] private GameObject _controllerIndexPrefab = default;
        
        private GameObject _controllerGO = null;

        private void Awake()
        {
            PrepareController();
        }

        #region Events
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
            // MRA: when development progresses, we may need a better way of switching off/on the game objects
            switch (state)
            {
                case GameManager.GameState.MENU:
                    _controllerGO.SetActive(true);
                    break;
                default:
                    _controllerGO.SetActive(false);
                    break;
            }
        }
        #endregion

        #region HELPER METHODS
        private void PrepareController()
        {
            DeviceData.Platform platform = _deviceData.CurrentPlatform;
            GameObject go = null;

            switch (platform)
            {
                case DeviceData.Platform.QUEST:
                    go = _controllerQuestPrefab;
                    break;
                case DeviceData.Platform.OCULUS_RIFT:
                    go = _controllerRiftPrefab;
                    break;
                case DeviceData.Platform.HTC_VIVE:
                    go = _controllerVivePrefab;
                    break;
                case DeviceData.Platform.VALVE_INDEX:
                    go = _controllerIndexPrefab;
                    break;
                default:
                    Debug.LogError("[HM] No valid platform detected: " + platform);
                    break;
            }

            if (go != null)
            {
                _controllerGO = Instantiate(go, transform);
                _controllerGO.SetActive(false);
            }
        }
        #endregion
    }
}