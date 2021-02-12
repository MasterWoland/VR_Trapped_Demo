using System;
using System.Collections;
using System.Collections.Generic;
using nl.allon.configs;
using nl.allon.events;
using nl.allon.managers;
using nl.allon.views;
using UnityEngine;

namespace nl.allon.controllers
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private LevelConfigEvent _prepareLevelEvent = default;
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
            _prepareLevelEvent.Handler += OnPrepareLevel;
            _blocksAppearCompleteEvent.Handler += OnBlocksInPosition;
        }
        
        private void OnDisable()
        {
            _prepareLevelEvent.Handler -= OnPrepareLevel;
            _blocksAppearCompleteEvent.Handler -= OnBlocksInPosition;
        }

        private void OnPrepareLevel(LevelConfig config)
        {
            _view.ApplyColor(config.ScoreColor);
        }

        private void OnBlocksInPosition()
        {
            _view.Appear();
        }
        #endregion
    }
}