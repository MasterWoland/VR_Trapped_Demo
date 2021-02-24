using System;
using nl.allon.events;
using nl.allon.models;
using nl.allon.views;
using TMPro;
using UnityEngine;

namespace nl.allon.controllers
{
    public delegate void BlockDelegate(BlockView view);
 
    public class BlockController : MonoBehaviour
    {
        [SerializeField] private FloatIdEvent _blockImpactEvent;
        [SerializeField] private SimpleEvent _blockInPositionEvent = default;
        private BlockModel _model;
        private BlockView _view;
        // private float _health = 100; // MRA: temp

        // temp
        // public TextMeshPro HealthText;
        
        public void Init(BlockModel model, BlockView view)
        {
            _model = model;
            _view = view;
            _view.MoveToInitPosition(_model.Height);
        }

        public void StartBlocks()
        {
            _view.Appear(_model.BlockId, BlockDelegate);
            
            // MRA: Temp
            // _view.ShowDebugInfo(_model.Health);
        }
        
        private void BlockDelegate(BlockView view)
        {
            if (view == _view)
            {
                _blockInPositionEvent?.Dispatch();
            }
        }
        
        #region EVENT
        private void OnEnable()
        {
            _blockImpactEvent.Handler += OnBlockImpact;
        }

        private void OnBlockImpact(int id, float value)
        {
            if (_model.BlockId == id)
            {
                Debug.Log("[BC] Corresponding Block: "+id);
                
                // MRA: for now we set a limit to 100 for the value.
                // In unforeseen cases the velocity of the ball may be beyond logical values, so we have to cap this

                if (value > 100f) value = 100f;
                
                float impact = value * _model.Config.BlockImpactMultiplier;
                _model.Health -= (int)impact;
                _view.ShowDebugInfo(_model.Health);
            }
            else
            {
                Debug.Log("[BC] Wrong Block: "+id+", instead of: "+_model.BlockId);
            }

            Debug.Log("[BC] Impact value: "+value);
        }
        #endregion
        // private void OnCollisionEnter(Collision collision)
        // {
        //     float impact = collision.rigidbody.velocity.sqrMagnitude;// * 0.05f; // MRA: Magic number alert!
        //     _health -= (int)impact;
        //
        //     // HealthText.text = _health.ToString();
        //
        //     // _blockImpactEvent?.Dispatch(impact);
        //
        //     // MRA: temp
        //     _view.ShowDebugInfo(impact);
        //     Debug.Log("__impact: "+impact);
        //     
        //     // if (_health < 0f) _view.Hide();
        // }
    }
}