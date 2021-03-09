using nl.allon.components;
using nl.allon.controllers;
using nl.allon.events;
using Pixelplacement;
using TMPro;
using UnityEngine;

namespace nl.allon.views
{
    public class BlockView : MonoBehaviour
    {
        [SerializeField] private FloatIdEvent _blockImpactEvent = default;
        [SerializeField] private AnimationCurve _appearCurve = default;
        private Transform _transform;
        private Vector3 _targetPos = Vector3.zero;
        private float _appearSpeed = 0.5f; //MRA: obtain this from model/config
        private float _appearDelay = 0.33f; //MRA: obtain this from model/config
        private BlockDelegate _appearCompleteCallback = null;
        private int _id = -1;
        private BlockMaterial _blockMaterial = default;
        
        // MRA: TEMP
        [SerializeField] private TextMeshPro _debugText;
        private float _highestImpact = 0;

        private void Awake()
        {
            _transform = transform;
            _blockMaterial = GetComponent<BlockMaterial>();
            
            if(_blockMaterial == null) Debug.LogError("[BlockView] No BlockMaterial Component found");
        }

        public void Appear(int blockId, BlockDelegate callback)
        {
            _id = blockId;

            _appearCompleteCallback = callback;
            float delay = blockId * _appearDelay;

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
            // are we colliding with a ball?
            // dispatch sqr magnitude

            float impact = collision.rigidbody.velocity.magnitude; // * 0.05f; // MRA: Magic number alert!
            // _health -= (int)impact;
            if (_id >= 0)
            {
                _blockMaterial.ApplyHitEffect(impact);
                _blockImpactEvent?.Dispatch(_id, impact);
                ShowDebugInfo(impact);
            }
            else
            {
                Debug.LogError("[BlockView] No Id has been assigned: " + _id);
            }
            // HealthText.text = _health.ToString();

            // _blockImpactEvent?.Dispatch(impact);

            // MRA: temp
            // ShowDebugInfo(impact);
            // Debug.Log("__impact: " + impact);

            // if (_health < 0f) _view.Hide();
        }

        // MRA: TEMP
        public void ShowDebugInfo(float info)
        {
            // health
            _debugText.text = info.ToString("000.0");

            // if (info > _highestImpact)
            // {
            //     _debugText.text = info.ToString("0000");
            //     _highestImpact = info;
            // }
        }
    }
}