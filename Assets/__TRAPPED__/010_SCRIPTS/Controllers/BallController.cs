using System;
using System.Collections;
using System.Collections.Generic;
using nl.allon.views;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace nl.allon.controllers
{
    [RequireComponent(typeof(VelocityEstimator))]
    public class BallController : MonoBehaviour
    {
        [SerializeField] private GameObject _viewPrefab = default;
        private VelocityEstimator _velocityEstimator = default;
        private Transform _transform;
        private Transform _spawnParentTF;
        private BallView _view;
        private bool _isAvailable = true;
        public bool IsAvailable => _isAvailable;

        private void Awake()
        {
            _transform = transform;
            _spawnParentTF = _transform.parent;
            
            _view = Instantiate(_viewPrefab, transform).GetComponent<BallView>();
            _velocityEstimator = GetComponent<VelocityEstimator>();
        }

        private void Start()
        {
            _view.Hide();
        }

        public void Activate()
        {
            _isAvailable = false;
            _view.Show();
            _velocityEstimator.BeginEstimatingVelocity();
        }

        public void Release()
        {
            // we must unparent this object
            _transform.SetParent(null);
            Debug.Log("[BC] Parent on release: "+transform.name);
            _view.Release(_velocityEstimator.GetVelocityEstimate(), _velocityEstimator.GetAngularVelocityEstimate());
            Invoke(nameof(Remove), 4f); //MRA: from config!
        }

        private void Remove()
        {
            // MRA: re-parent to hand
            _transform.SetParent(_spawnParentTF);
            Debug.Log("[BC] Parent on Remove: "+transform.name);
            _view.Reset();
            _view.Hide();
            _isAvailable = true;
        }
    }
}