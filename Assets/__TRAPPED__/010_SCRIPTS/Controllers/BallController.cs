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
        private VelocityEstimator _velocityEstyimator = default;
        private BallView _view;
        
        private void Awake()
        {
            _velocityEstyimator = GetComponent<VelocityEstimator>();
            
            // instantiate view
            _view = Instantiate(_viewPrefab, transform).GetComponent<BallView>();
        }

        private void Start()
        {
            _view.Hide();
        }

        public void Activate()
        {
            _view.Show();
            _velocityEstyimator.BeginEstimatingVelocity();
        }

        public void Release()
        {
            // we must unparent this object
            transform.SetParent(transform.root);
            _view.Release(_velocityEstyimator.GetVelocityEstimate(), _velocityEstyimator.GetAngularVelocityEstimate());
            
            Debug.Log("[BC] THROW");
        }
    }
}