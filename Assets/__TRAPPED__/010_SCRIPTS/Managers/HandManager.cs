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
    /// Applies the correct controller, hand or attribute to the hand
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
        [SerializeField] private PlayerConfig _playerConfig = default;
        [SerializeField] private GameObject _controllerQuestPrefab = default;
        [SerializeField] private GameObject _controllerRiftPrefab = default;
        [SerializeField] private GameObject _controllerVivePrefab = default;
        [SerializeField] private GameObject _controllerIndexPrefab = default;
        [SerializeField] private GameObject _racketPrefab = default;
        [SerializeField] private GameObject _ballSpawnerPrefab = default;
        
        private GameObject _controllerGO = null;
        private GameObject _racketGO = null;
        private GameObject _ballSpawnerGO = null;
        private bool _useRacket = false;

        private void Awake()
        {
            Debug.Log("[HandManager] Platform = " + _deviceData.CurrentPlatform);
            PrepareController();
            PrepareRacket();
            PrepareBallSpawner();
            _useRacket = UseRacketInThisHand();
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
                case GameManager.GameState.PREPARE_LEVEL:
                    _racketGO.SetActive(_useRacket);
                    _ballSpawnerGO.SetActive(!_useRacket);
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

        private void PrepareRacket()
        {
            // _racketGO = Instantiate(_racketPrefab, transform);
            _racketGO = Instantiate(_racketPrefab);
            _racketGO.transform.position = transform.position;
            _racketGO.transform.rotation = transform.rotation;
            
            FixedJoint fixedJoint = gameObject.AddComponent<FixedJoint>();
            fixedJoint.breakForce = Mathf.Infinity; // 20000; // not infinite, we don't want to move solid objects through solid objects
            fixedJoint.breakTorque = Mathf.Infinity; // 20000;
            fixedJoint.connectedBody = _racketGO.GetComponent<Rigidbody>();

            // _racketGO.transform.Translate(_racketSnapPositionOffset, Space.Self);
            // _racketGO.transform.Rotate(_racketSnapRotationOffset);
            
            // fixedJoint.connectedBody = _racketGO.GetComponent<Rigidbody>();
            _racketGO.SetActive(false);
        }

        private void PrepareBallSpawner()
        {
            _ballSpawnerGO = Instantiate(_ballSpawnerPrefab, transform);
            _ballSpawnerGO.SetActive(false);
        }

        private bool UseRacketInThisHand()
        {
            if (_playerConfig.UseRightHandForRacket && CurrentHand == Hand.RIGHT)
            {
                return true;
            }
            else if (!_playerConfig.UseRightHandForRacket && CurrentHand == Hand.LEFT)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}