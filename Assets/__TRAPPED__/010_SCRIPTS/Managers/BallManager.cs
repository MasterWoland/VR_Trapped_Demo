using System;
using System.Collections;
using System.Collections.Generic;
using nl.allon.configs;
using nl.allon.controllers;
using nl.allon.events;
using UnityEngine;

namespace nl.allon.managers
{
    public class BallManager : MonoBehaviour
    {
        // Events
        [SerializeField] private GameStateEvent _gameStateEvent = default;
        [SerializeField] private SimpleInputEvent _selectStartedEvent = default;
        [SerializeField] private SimpleInputEvent _selectCanceledEvent = default;

        [SerializeField] private PlayerConfig _playerConfig = default;
        [SerializeField] private GameObject _ballControllerPrefab = default;
        private BallController[] _ballPool;
        private int _curBallindex = 0;
        private bool _useLeftHand = true; // the hand that is used for spawning balls
        private BallController _curBallController = default;
        private bool _isSpawning = false;
        private int _numBalls;
        
        private void Awake()
        {
            CreateBallPool();
        }

        private void Start()
        {
            // if we use the right hand for the racket, we use the left hand for the balls and vice versa
            _useLeftHand = _playerConfig.UseRightHandForRacket;
        }

        private void CreateBallPool()
        {
            int numBalls = _playerConfig.NumBalls; //MRA: Magic Number alert. Obtain this from config
            _ballPool = new BallController[numBalls];
            BallController tempBallController;

            for (int i = 0; i < numBalls; i++)
            {
                tempBallController = Instantiate(_ballControllerPrefab, transform).GetComponent<BallController>();
                _ballPool[i] = tempBallController;
            }
        }

        #region EVENTS
        private void OnEnable()
        {
            _gameStateEvent.Handler += OnGameStateChange;
        }

        private void OnDisable()
        {
            _gameStateEvent.Handler -= OnGameStateChange;
        }

        // Select = Grip
        private void OnSelectStarted(Hand hand)
        {
            if (_isSpawning) return;

            // Make ball appear
            if (hand == Hand.LEFT && _useLeftHand)
            {
                _curBallController = _ballPool[_curBallindex];
            }
            else if (hand == Hand.RIGHT && !_useLeftHand)
            {
                _curBallController = _ballPool[_curBallindex];
            }
            else
            {
                // Wrong Hand
                return;
            }

            // MRA: ball must be available
            if (_curBallController.IsAvailable)
            {
                _isSpawning = true;
                _curBallController.Activate();

                // manage index
                _curBallindex++;
                if (_curBallindex >= _ballPool.Length) _curBallindex = 0;
            }
            else
            {
                Debug.LogError("[BM] Ball is not available");
            }
        }

        // Select = Grip
        private void OnSelectCanceled(Hand hand)
        {
            // No action if event comes from the wrong hand
            if (hand == Hand.RIGHT && _useLeftHand)
            {
                return;
            }
            else if (hand == Hand.LEFT && !_useLeftHand)
            {
                return;
            }

            if(_curBallController) _curBallController.Release();
            _isSpawning = false;
        }

        private void OnGameStateChange(GameManager.GameState state)
        {
            // The BallManager only listens to input when the game is runnning
            if (state == GameManager.GameState.RUNNING)
            {
                //MRA: we listen to trigger up and down when the game is running

                _selectStartedEvent.Handler += OnSelectStarted;
                _selectCanceledEvent.Handler += OnSelectCanceled;
            }
            else
            {
                //MRA: we stop listening when the game is not running

                _selectStartedEvent.Handler -= OnSelectStarted;
                _selectCanceledEvent.Handler -= OnSelectCanceled;
            }
        }
        #endregion
    }
}