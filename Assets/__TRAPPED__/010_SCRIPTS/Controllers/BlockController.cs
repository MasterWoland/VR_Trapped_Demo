using nl.allon.events;
using nl.allon.models;
using nl.allon.views;
using UnityEngine;

namespace nl.allon.controllers
{
    public delegate void BlockDelegate(BlockView view);
 
    public class BlockController : MonoBehaviour
    {
        [SerializeField] private SimpleEvent _blockInPositionEvent = default;
        private BlockModel _model;
        private BlockView _view;
        
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
    }
}