using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace nl.allon.views
{
    public class BallView : MonoBehaviour
    {
        private Rigidbody _rigidbody = default;
        private Vector3 _velocity;
        private Vector3 _angularVelocity;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();

            Reset();
        }

        private void Reset()
        {
            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = true;
        }

        private void FixedUpdate()
        {
            _velocity = _rigidbody.velocity;
            _angularVelocity = _rigidbody.angularVelocity;

            // Debug.Log("_______velocity = "+_velocity.magnitude);
        }

        #region PUBLIC
        public void Hide()
        {
            gameObject.SetActive(false);

            Debug.Log("[View] hiding from view");
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Throw()
        {
            _rigidbody.useGravity = true;
            _rigidbody.isKinematic = false;

            _rigidbody.AddForce(_velocity, ForceMode.Impulse);
            // _rigidbody.AddTorque(_angularVelocity, ForceMode.Impulse);

        }
        #endregion
    }
}