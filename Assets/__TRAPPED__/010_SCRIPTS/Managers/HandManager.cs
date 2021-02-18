using System;
using System.Collections;
using System.Collections.Generic;
using nl.allon.data;
using nl.allon.events;
using UnityEngine;

namespace nl.allon.managers
{
    /// <summary>
    /// Applies the correct controller, hand or attribute to the hand
    /// </summary>
    public class HandManager : MonoBehaviour
    {
        // Events
        [SerializeField] private GameStateEvent _gameStateEvent = default;
       
        [SerializeField] private DeviceData _deviceData = default;
        [SerializeField] private GameObject _controllerQuest = default;
        [SerializeField] private GameObject _controllerRift = default;
        [SerializeField] private GameObject _controllerVive = default;
        [SerializeField] private GameObject _controllerIndex = default;

        private GameObject _controllerGO = null;
        
        private void Awake()
        {
            Debug.Log("[HandManager] Platform = "+_deviceData.CurrentPlatform);
            PrepareController();
        }

        private void PrepareController()
        {
            DeviceData.Platform platform= _deviceData.CurrentPlatform;
            GameObject go = null;
            
            switch (platform)
            {
                case DeviceData.Platform.QUEST:
                    go = _controllerQuest;
                    break;
                case DeviceData.Platform.OCULUS_RIFT:
                    go = _controllerRift;
                    break;
                case DeviceData.Platform.HTC_VIVE:
                    go = _controllerVive;
                    break;
                case DeviceData.Platform.VALVE_INDEX:
                    go = _controllerIndex;
                    break;
                default:
                    Debug.LogError("[HM] No valid platform detected: "+platform);
                    break;
            }

            if (go != null)
            {
                _controllerGO = Instantiate(go, transform);
                _controllerGO.SetActive(false);
            }
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
    }
}