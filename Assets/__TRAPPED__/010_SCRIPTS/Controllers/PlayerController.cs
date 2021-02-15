using System;
using System.Collections;
using System.Collections.Generic;
using nl.allon.configs;
using nl.allon.events;
using nl.allon.utils;
using UnityEngine;

namespace nl.allon.controllers
{
    public class PlayerController : BaseSingleton<PlayerController>
    {
        [SerializeField] private PlayerConfig _config = default;

        // Events
        [SerializeField] private BoolEvent _useRightHandForRacketEvent = default;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        #region EVENTS
        private void OnEnable()
        {
            _useRightHandForRacketEvent.Handler += OnHandRacketChanged;
        }
        private void OnDisable()
        {
            _useRightHandForRacketEvent.Handler -= OnHandRacketChanged;
        }

        private void OnHandRacketChanged(bool boolean)
        {
            _config.SetRacketHand(boolean);
        }
        #endregion
    }
}