using System;
using System.Collections;
using System.Collections.Generic;
using nl.allon.views;
using UnityEngine;

namespace nl.allon.controllers
{
    public class BallController : MonoBehaviour
    {
        [SerializeField] private GameObject _viewPrefab = default;
        private BallView _view;
        
        private void Awake()
        {
            Debug.Log("[BallController] Says hello");
            
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
        }

        public void Throw()
        {
            // we must unparent this object
            transform.SetParent(transform.root);
            _view.Throw();
            
            Debug.Log("[BC] THROW");
        }
    }
}