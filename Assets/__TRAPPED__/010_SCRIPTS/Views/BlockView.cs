using System;
using System.Collections;
using System.Collections.Generic;
using nl.allon.controllers;
using nl.allon.events;
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
        private BlockDelegate _appearCompleteCallback = null;

        private void Awake()
        {
            _transform = transform;
        }

        public void Appear(int blockNumber, BlockDelegate callback)
        {
            _appearCompleteCallback = callback;
            float delay = blockNumber * _appearDelay;
         
            // tween to start position
            Tween.LocalPosition(_transform, _targetPos, _appearSpeed, delay, 
                _appearCurve, Tween.LoopType.None, null, OnComplete);
        }
     
        private void OnComplete()
        {
            _appearCompleteCallback?.Invoke(this);
            _appearCompleteCallback = null; 
        }

        public void MoveToInitPosition(float height)
        {
            // add a margin because of z-fighting 
            float posY = (height * -1f) + 0.01f; // MRA: Magic Number Alert!
            _transform.Translate(0, posY, 0);
        }
    }
}