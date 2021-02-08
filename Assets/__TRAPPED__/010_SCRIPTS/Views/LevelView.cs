using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using nl.allon.configs;
using UnityEngine;

namespace nl.allon.views
{
    public class LevelView : MonoBehaviour
    {
        private Transform _transform = default;
        private GameObject _environment = default;
        
        private void Awake()
        {
            _transform = transform;
        }

        public void PrepareNewLevel(LevelConfig config)
        {
            Debug.Log("[LevelView] level: "+config.LevelNum);
            
            if (_environment != null)
            {
                Debug.Log("[LevelView] We already have an environment "+_environment.name);
                _environment = null; // MRA: or must we destroy it?????
            }
            else
            {
                Debug.Log("[LevelView] No environment yet");
                _environment = Instantiate(config.EnvironmentPrefab, _transform);
            }
        }
    }
}