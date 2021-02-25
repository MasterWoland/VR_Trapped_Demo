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
        [SerializeField] private GameObject _ballManagerPrefab = default;

        [SerializeField] private GameObject _leftHand = default; 
        [SerializeField] private GameObject _rightHand = default;

        // Events
        [SerializeField] private BoolEvent _useRightHandForRacketEvent = default;
        [SerializeField] private GameStateEvent _gameStateEvent = default;

        private GameObject _racketGO = null;
        private Transform _racketTF = default;
        private GameObject _ballManagerGO = default;
        private Transform _ballManagerTF = default;
        private bool _useRightHandForRacket = true;

        private void Awake()
        {
            DontDestroyOnLoad(this);

            CreateRacket();
            CreateBallManager();

            _useRightHandForRacket = _config.UseRightHandForRacket;
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
            _useRightHandForRacket = _config.UseRightHandForRacket;

            Debug.Log("Hand for Racket changed to: use right = " + boolean);
        }

        private void OnGameStateEvent(GameManager.GameState state)
        {
            switch (state)
            {
                case GameManager.GameState.PREPARE_LEVEL:
                    ActivateBallManager();
                    ActivateRacket();
                    break;
            }
        }
        #endregion

        #region HELPER METHODS
        private void CreateRacket()
        {
            _racketGO = Instantiate(_racketPrefab);
            _racketTF = _racketGO.transform;
            _racketGO.SetActive(false);
        }

        private void CreateBallManager()
        {
            _ballManagerGO = Instantiate(_ballManagerPrefab);
            _ballManagerTF = _ballManagerGO.transform;
            _ballManagerGO.SetActive(false);
        }

        private void ActivateRacket()
        {
            // check for correct hand
            Transform curTransform = _useRightHandForRacket ? _rightHand.transform : _leftHand.transform;

            _racketTF.position = curTransform.localPosition;
            _racketTF.rotation = curTransform.localRotation;

            // MRA: obtain these values from config
            Vector3 offsetPos = new Vector3(0.003f, -0.028f, -0.011f);
            Vector3 offsetRot = new Vector3(147.236f, -1.686f, 5.468994f);
            _racketTF.position += offsetPos;
            _racketTF.Rotate(offsetRot, Space.Self);

            FixedJoint fx = curTransform.gameObject.AddComponent<FixedJoint>(); // TODO: cache FixedJoint (use require component?)
            fx.breakForce = Mathf.Infinity; // 20000; // not infinite, we don't want to move solid objects through solid objects
            fx.breakTorque = Mathf.Infinity; // 20000;

            _racketGO.SetActive(true);

            fx.connectedBody = _racketGO.GetComponent<Rigidbody>();
            
            // ---- temp -----
            FixedJoint fx_02 = curTransform.gameObject.AddComponent<FixedJoint>(); // TODO: cache FixedJoint (use require component?)
            fx_02.breakForce = Mathf.Infinity; // 20000; // not infinite, we don't want to move solid objects through solid objects
            fx_02.breakTorque = Mathf.Infinity; // 20000;
            fx_02.connectedBody = _racketGO.transform.GetChild(0).GetComponent<Rigidbody>();
            
            FixedJoint fx_03 = curTransform.gameObject.AddComponent<FixedJoint>(); // TODO: cache FixedJoint (use require component?)
            fx_03.breakForce = Mathf.Infinity; // 20000; // not infinite, we don't want to move solid objects through solid objects
            fx_03.breakTorque = Mathf.Infinity; // 20000;
            fx_03.connectedBody = _racketGO.transform.GetChild(1).GetComponent<Rigidbody>();
            // ---------------------------
        }

        private void ActivateBallManager()
        {
            Transform curTransform = _useRightHandForRacket ? _leftHand.transform : _rightHand.transform;

            _ballManagerTF.SetParent(curTransform);
            _ballManagerTF.position = curTransform.localPosition;
            _ballManagerTF.rotation = curTransform.localRotation;

            _ballManagerGO.SetActive(true);
        }
        #endregion
    }
}