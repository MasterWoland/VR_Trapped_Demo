using System;
using System.Collections;
using System.Collections.Generic;
using nl.allon.configs;
using nl.allon.events;
using nl.allon.interfaces;
using nl.allon.views;
using UnityEngine;

namespace nl.allon.managers
{
    public class ParticleEffectManager : MonoBehaviour
    {
        [SerializeField] private GameConfigEvent _gameConfigEvent = default;
        [SerializeField] private Vector3FloatEvent _ballHitsBlockEvent = default;
        public GameObject _ballHitBlockParticlePrefab;
        private IParticleEffect[] _ballHitBlockPool;
        private int _poolSize = 10; // MRA: obtain this from a model or a config
        private int _ballHitBlockIndex = 0;

        private void CreateBallHitWallPool()
        {
            _ballHitBlockPool = new IParticleEffect[_poolSize];
            PS_BallHitBlock tempObject;

            for (int i = 0; i < _poolSize; i++)
            {
                tempObject = Instantiate(_ballHitBlockParticlePrefab, transform).GetComponent<PS_BallHitBlock>();
                tempObject.gameObject.SetActive(false);
                _ballHitBlockPool[i] = tempObject;
            }
        }

        #region EVENTS
        private void OnEnable()
        {
            _gameConfigEvent.Handler += OnGameConfigEvent;
            _ballHitsBlockEvent.Handler += OnBallHitsBlock;
        }

        private void OnDisable()
        {
            _ballHitsBlockEvent.Handler -= OnBallHitsBlock;
        }
        #endregion
        
        #region EVENT HANDLING
        private void OnGameConfigEvent(GameConfig config)
        {
            _gameConfigEvent.Handler -= OnGameConfigEvent;
            _ballHitBlockParticlePrefab = config.BallHitWallParticle;

            CreateBallHitWallPool();
        }

        private void OnBallHitsBlock(Vector3 position, float impact)
        {
            Debug.Log("[PEM] Ball hits block: " + impact);

            PS_BallHitBlock tempObject = (PS_BallHitBlock)_ballHitBlockPool[_ballHitBlockIndex];
            tempObject.gameObject.SetActive(true);
            tempObject.transform.position = position; 
            // MRA: perhaps apply offset in z-axis
            // MRA: use tempObject.Apply() !!!


            _ballHitBlockIndex++;
            if (_ballHitBlockIndex >= _poolSize) _ballHitBlockIndex = 0;
        }
        #endregion
    }
}