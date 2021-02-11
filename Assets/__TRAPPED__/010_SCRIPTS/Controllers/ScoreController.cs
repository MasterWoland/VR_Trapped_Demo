using System;
using System.Collections;
using System.Collections.Generic;
using nl.allon.events;
using nl.allon.views;
using UnityEngine;

namespace nl.allon.controllers
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private SimpleEvent _blocksAppearCompleteEvent = default;
        [SerializeField] private GameObject _scoreViewPrefab = default;
        private ScoreView _view;
        
        private void Awake()
        {
            _view = Instantiate(_scoreViewPrefab, transform).GetComponent<ScoreView>();
        }

        #region EVENTS
        private void OnEnable()
        {
            _blocksAppearCompleteEvent.Handler += OnBlocksInPosition;
        }
        
        private void OnDisable()
        {
            _blocksAppearCompleteEvent.Handler -= OnBlocksInPosition;
        }

        private void OnBlocksInPosition()
        {
            _view.Appear();
        }
        #endregion
    }
}