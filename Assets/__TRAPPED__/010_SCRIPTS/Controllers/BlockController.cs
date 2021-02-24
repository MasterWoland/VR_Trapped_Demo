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
        [SerializeField] private FloatEvent _blockImpactEvent;
        [SerializeField] private SimpleEvent _blockInPositionEvent = default;
        private BlockModel _model;
        private BlockView _view;
        private float _health = 100; // MRA: temp

        // temp
        public TextMeshPro HealthText;
        
        public void Init(BlockModel model, BlockView view)
        {
            _model = model;
            _view = view;
            _view.MoveToInitPosition(_model.Height);
        }

        public void StartBlocks()
        {
            _view.Appear(_model.BlockNumber, BlockDelegate);
        }
        
        private void BlockDelegate(BlockView view)
        {
            if (view == _view)
            {
                _blockInPositionEvent?.Dispatch();
            }
        }
        
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