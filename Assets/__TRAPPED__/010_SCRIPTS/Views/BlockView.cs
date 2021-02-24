using nl.allon.controllers;
using Pixelplacement;
using TMPro;
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

        // MRA: TEMP
        [SerializeField] private TextMeshPro _debugText;
        
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
        
        
        private void OnCollisionEnter(Collision collision)
        {
            float impact = collision.rigidbody.velocity.sqrMagnitude;// * 0.05f; // MRA: Magic number alert!
            // _health -= (int)impact;

            // HealthText.text = _health.ToString();

            // _blockImpactEvent?.Dispatch(impact);

            // MRA: temp
            ShowDebugInfo(impact);
            Debug.Log("__impact: "+impact);
            
            // if (_health < 0f) _view.Hide();
        }
        
        // MRA: TEMP
        public void ShowDebugInfo(float info)
        {
            _debugText.text = info.ToString("0000");
        }
    }
}