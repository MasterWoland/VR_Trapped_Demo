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
        private SphereCollider _collider = default;
        private Vector3 _velocity;
        private Vector3 _angularVelocity;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<SphereCollider>();

            Reset();
        }

        private void Reset()
        {
            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = true;
            _collider.enabled = false;
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

        public void Release(Vector3 velocity, Vector3 angularVelocity)
        {
            _rigidbody.useGravity = true;
            _rigidbody.isKinematic = false;
            _collider.enabled = true;
            _rigidbody.velocity = velocity;
            _rigidbody.angularVelocity = angularVelocity;

            Debug.LogFormat("Vel: {0}, Ang vel: {1}", velocity, _angularVelocity.magnitude);

        }
        #endregion
    }
}