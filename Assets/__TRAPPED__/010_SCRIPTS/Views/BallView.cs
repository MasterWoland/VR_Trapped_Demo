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

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<SphereCollider>();

            Reset();
        }
        
        #region PUBLIC
        public void Reset()
        {
            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = true;
            _collider.enabled = false;
            _rigidbody.velocity = _rigidbody.angularVelocity = Vector3.zero;
            _rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
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
            _rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }
        #endregion
    }
}