using System;
using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
using UnityEngine;

namespace nl.allon.views
{
    public class BlockView : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _appearCurve = default;
        private Transform _transform;
        private Vector3 _targetPos = Vector3.zero;
        // public int Index;
        
        private void Awake()
        {
            _transform = transform;
        }

        public void Appear(int blockNumber)
        {
            float delay = blockNumber * 0.75f; // MRA: Magic Number alert
            
            // tween to start position
            Tween.LocalPosition(_transform, _targetPos, 2f, delay, 
                _appearCurve, Tween.LoopType.None, null, OnComplete);
        }

        private void OnComplete()
        {
            Debug.Log("[VIEW] tween complete");
        }

        public void MoveToInitPosition(float height)
        {
            float posY = (height + 0.01f) * -1f; // add a margin because of z-fighting 
            _transform.Translate(0, posY, 0);
        }
    }
}