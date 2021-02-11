using System;
using System.Collections;
using System.Collections.Generic;
using nl.allon.configs;
using nl.allon.controllers;
using nl.allon.events;
using nl.allon.models;
using nl.allon.utils;
using nl.allon.views;
using UnityEngine;

namespace nl.allon.managers
{
    /// <summary>
    /// Sets up and moves the blocks in the level
    /// </summary>
    public class BlocksManager : BaseSingleton<BlocksManager>
    {
        [SerializeField] private LevelConfigEvent _prepareLevelEvent = default;
        [SerializeField] private SimpleEvent _blocksAreSetupEvent = default;
        [SerializeField] private GameStateEvent _gameStateEvent = default;
        [SerializeField] private SimpleEvent _blockInPositionEvent = default;
        [SerializeField] private SimpleEvent _blocksAppearCompleteEvent = default;
        
        private Transform _transform;
        private Transform[] _columns;
        private BlockController[] _blockControllers;
        private float _minColumnSpeed, _maxColumnSpeed;
        private int _numBlocksInPosition = 0;

        private const string COLUMN_STRING = "Column_";
        private const string BLOCK_STRING = "Block_";

        #region EVENTS
        private void OnEnable()
        {
            _transform = this.transform;
            _prepareLevelEvent.Handler += OnPrepareLevel;
            _gameStateEvent.Handler += OnGameStateChange;
            _blockInPositionEvent.Handler += OnBlockInPosition;
        }

        private void OnDisable()
        {
            _prepareLevelEvent.Handler -= OnPrepareLevel;
            _gameStateEvent.Handler -= OnGameStateChange;
            _blockInPositionEvent.Handler -= OnBlockInPosition;
        }

        private void OnBlockInPosition()
        {
            _numBlocksInPosition++;
            if (_numBlocksInPosition >= _blockControllers.Length)
            {
                _blocksAppearCompleteEvent?.Dispatch();
                _numBlocksInPosition = 0; // MRA: reset value right away
            }
        }

        private void OnPrepareLevel(LevelConfig config)
        {
            // assign values
            _minColumnSpeed = config.MinColumnSpeed;
            _maxColumnSpeed = config.MaxColumnSpeed;

            SetupBlockColumns(config);
        }
        
        private void OnGameStateChange(GameManager.GameState state)
        {
            switch (state)
            {
                case GameManager.GameState.LEVEL_INTRO:
                    // Blocks should appear
                    StartBlocks();
                    break;
            }
        }

        private void StartBlocks()
        {
            int numBlocks = _blockControllers.Length;
            
            for (int i = 0; i < numBlocks; i++)
            {
                _blockControllers[i].StartBlocks();
            }
        }
        #endregion

        #region HELPER METHODS
        private void SetupBlockColumns(LevelConfig config)
        {
            // columns
            int numColumns = config.NumColumns;
            int numRows = config.NumRows;
            _columns = new Transform[numColumns];
            Transform parent;
            BlockController tempBlockController;

            // block values
            GameObject tempBlockView;
            GameObject blockControllerPrefab = config.BlockControllerPrefab;
            GameObject blockViewPrefab = config.BlockViewPrefab;
            Vector3 startPos = config.TopLeftPosition;
            float blockWidth = config.GetBlockWidth();
            float blockHeight = config.GetBlockHeight();
            float blockDepth = config.GetBlockDepth();
            int blockCounter = 0;
            _blockControllers = new BlockController[numColumns * numRows];

            for (int columnIndex = 0; columnIndex < numColumns; columnIndex++)
            {
                // Instantiate new Column
                parent = new GameObject().transform;
                parent.SetParent(_transform);
                parent.name = COLUMN_STRING + columnIndex.ToString();

                _columns[columnIndex] = parent;

                for (int rowIndex = 0; rowIndex < numRows; rowIndex++)
                {
                    // Fill the column with blocks
                    Vector3 position = new Vector3(startPos.x + (columnIndex * blockWidth),
                        startPos.y,
                        startPos.z - (rowIndex * blockDepth));

                    // Instantiate the controllers
                    tempBlockController = Instantiate(blockControllerPrefab, position, Quaternion.identity, parent)
                        .GetComponent<BlockController>();
                    tempBlockController.name = BLOCK_STRING + rowIndex.ToString();
                    _blockControllers[blockCounter] = tempBlockController;

                    // Add the views as children
                    tempBlockView = Instantiate(blockViewPrefab, tempBlockController.transform);
                    tempBlockView.name = BLOCK_STRING + rowIndex.ToString();

                    // blockNumber is used to make blocks appear in a certain order
                    int blockNumber = ((numColumns - 1) * rowIndex) + rowIndex + columnIndex;
                    BlockModel model = new BlockModel(config.GetDimensions(), blockNumber);
                    tempBlockController.Init(model, tempBlockView.GetComponent<BlockView>());

                    blockCounter++;
                }
            }
            
            _blocksAreSetupEvent.Dispatch();
        }
        #endregion
    }
}