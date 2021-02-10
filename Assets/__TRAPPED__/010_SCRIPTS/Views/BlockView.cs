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
        private float _appearSpeed = 0.5f; //MRA: obtain this from model/config
        private float _appearDelay = 0.33f; //MRA: obtain this from model/config
        // public int Index;
        
        private void Awake()
        {
            _transform = transform;
        }

        public void Appear(int blockNumber)
        {
            float delay = blockNumber * _appearDelay;
         
            // tween to start position
            Tween.LocalPosition(_transform, _targetPos, _appearSpeed, delay, 
                _appearCurve, Tween.LoopType.None, null, OnComplete);
        }

        private void OnComplete()
        {
            Debug.Log("[VIEW] tween complete");
        }

        public void MoveToInitPosition(float height)
        {
            // add a margin because of z-fighting 
            float posY = (height * -1f) + 0.01f; // MRA: Magic NUmber Alert!
            _transform.Translate(0, posY, 0);
        }
    }
}