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
        private BallView _view;
        
        private void Awake()
        {
            _view = Instantiate(_viewPrefab, transform).GetComponent<BallView>();
            _velocityEstimator = GetComponent<VelocityEstimator>();
        }

        private void Start()
        {
            _view.Hide();
        }

        public void Activate()
        {
            _view.Show();
            _velocityEstimator.BeginEstimatingVelocity();
        }

        public void Release()
        {
            // we must unparent this object
            transform.SetParent(transform.root);
            _view.Release(_velocityEstimator.GetVelocityEstimate(), _velocityEstimator.GetAngularVelocityEstimate());
            Invoke(nameof(Remove), 4f); //MRA: from config!
        }

        private void Remove()
        {
            _view.Reset();
            _view.Hide();
        }
    }
}