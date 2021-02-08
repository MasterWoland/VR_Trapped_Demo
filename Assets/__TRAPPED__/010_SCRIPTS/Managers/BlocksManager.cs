using System;
using System.Collections;
using System.Collections.Generic;
using nl.allon.configs;
using nl.allon.events;
using UnityEngine;

namespace nl.allon.managers
{
    /// <summary>
    /// Sets up and keeps track of all the blocks in the level
    /// </summary>
    public class BlocksManager : MonoBehaviour
    {
        [SerializeField] private LevelConfigEvent _prepareLevelEvent = default;

        #region EVENTS
        private void OnEnable()
        {
            _prepareLevelEvent.Handler += OnPrepareLevelEvent;
        }

        private void OnDisable()
        {
            _prepareLevelEvent.Handler -= OnPrepareLevelEvent;
        }

        private void OnPrepareLevelEvent(LevelConfig config)
        {
            Debug.Log("[BM] Prepare level "+config.LevelName);
        }
        #endregion
    }
}