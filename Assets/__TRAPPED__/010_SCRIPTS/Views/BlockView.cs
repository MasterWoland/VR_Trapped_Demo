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

        // MRA: TEMP
        [SerializeField] private TextMeshPro _debugText;
        private float _highestImpact = 0;

        private void Awake()
        {
            _transform = transform;
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
                _blockImpactEvent?.Dispatch(_id, impact);
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
        public void ShowDebugInfo(int info)
        {
            // health
            _debugText.text = info.ToString("0000");

            // if (info > _highestImpact)
            // {
            //     _debugText.text = info.ToString("0000");
            //     _highestImpact = info;
            // }
        }
    }
}