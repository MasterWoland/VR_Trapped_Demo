using System.Collections;
using System.Collections.Generic;
using nl.allon.models;
using nl.allon.views;
using UnityEngine;

namespace nl.allon.controllers
{
    public class BlockController : MonoBehaviour
    {
        private BlockModel _model; // MRA: what should be in here?
        private BlockView _view;

        public void Init(BlockModel model, BlockView view)
        {
            _model = model;
            _view = view;
            // _view.Index = model.BlockNumber;
            _view.MoveToInitPosition(_model.Height);
        }

        public void StartBlocks()
        {
            _view.Appear(_model.BlockNumber);
        }
    }
}