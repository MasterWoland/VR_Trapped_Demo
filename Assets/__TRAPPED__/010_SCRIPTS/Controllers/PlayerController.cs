using System;
using System.Collections;
using System.Collections.Generic;
using nl.allon.configs;
using nl.allon.events;
using nl.allon.managers;
using nl.allon.utils;
using UnityEngine;

namespace nl.allon.controllers
{
    public class PlayerController : BaseSingleton<PlayerController>
    {
        [SerializeField] private PlayerConfig _config = default;
        [SerializeField] private GameObject _racketPrefab = default;
        [SerializeField] private GameObject _rightHand = default;
        
        // Events
        [SerializeField] private BoolEvent _useRightHandForRacketEvent = default;
        [SerializeField] private GameStateEvent _gameStateEvent = default;
        
        private GameObject _racketGO = null;
        private Transform _racketTF = default;
        
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        #region EVENTS
        private void OnEnable()
        {
            _useRightHandForRacketEvent.Handler += OnHandRacketChanged;
            _gameStateEvent.Handler += OnGameStateEvent;
        }
        private void OnDisable()
        {
            _useRightHandForRacketEvent.Handler -= OnHandRacketChanged;
            _gameStateEvent.Handler -= OnGameStateEvent;
        }

        private void OnHandRacketChanged(bool boolean)
        {
            _config.SetRacketHand(boolean);

            Debug.Log("Hand for Racket changed to: use right = "+boolean);
        }
        
        private void OnGameStateEvent(GameManager.GameState state)
        {
            // MRA: when development progresses, we may need a better way of switching off/on the game objects
            switch (state)
            {
                // case GameManager.GameState.MENU:
                //     _controllerGO.SetActive(true);
                //     break;
                case GameManager.GameState.PREPARE_LEVEL:
                    CreateRacket();
                    AttachRacket();
                    // _racketGO.SetActive(_useRacket);
                    // _ballSpawnerGO.SetActive(!_useRacket);
                    break;
                // default:
                //     _controllerGO.SetActive(false);
                //     break;
            }
        }

        private void CreateRacket()
        {
            if (!_racketGO)
            {
                _racketGO = Instantiate(_racketPrefab);
                _racketTF = _racketGO.transform;
            }

            // Debug.Log("[PC] racket TF pos: "+_racketTF.position);
        }

        private void AttachRacket()
        {
            _racketTF.position = _rightHand.transform.localPosition;
            _racketTF.rotation = _rightHand.transform.localRotation;

            // MRA: obtain these values from config
            Vector3 offsetPos = new Vector3(0.003f, -0.028f, -0.011f);
            Vector3 offsetRot = new Vector3(147.236f, -1.686f, 5.468994f);

            _racketTF.position += offsetPos;
            _racketTF.Rotate(offsetRot, Space.Self);
            
            FixedJoint fx = _rightHand.AddComponent<FixedJoint>();  // TODO: cache FixedJoint (use require component?)
            fx.breakForce = Mathf.Infinity; // 20000; // not infinite, we don't want to move solid objects through solid objects
            fx.breakTorque = Mathf.Infinity; // 20000;
            fx.connectedBody = _racketGO.GetComponent<Rigidbody>();

            // Debug.Log("[PC] __ racket attached: "+_racketTF.position.y);
        }
        #endregion
    }
}