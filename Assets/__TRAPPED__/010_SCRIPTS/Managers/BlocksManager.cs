using System;
using System.Collections;
using System.Collections.Generic;
using nl.allon.configs;
using nl.allon.controllers;
using nl.allon.events;
using nl.allon.models;
using nl.allon.utils;
using nl.allon.views;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using static UnityEngine.Random;
using Random = System.Random;

namespace nl.allon.managers
{
    /// <summary>
    /// Sets up and moves the blocks in the level
    /// </summary>
    public class BlocksManager : BaseSingleton<BlocksManager>
    {
        // events
        [SerializeField] private LevelConfigEvent _prepareLevelEvent = default;
        [SerializeField] private SimpleEvent _blocksAreSetupEvent = default;
        [SerializeField] private GameStateEvent _gameStateEvent = default;
        [SerializeField] private SimpleEvent _blockInPositionEvent = default;
        [SerializeField] private SimpleEvent _blocksAppearCompleteEvent = default;
        [SerializeField] private IntEvent _blockDestroyedEvent = default;
        [SerializeField] private SimpleEvent _allBlocksDestroyedEvent = default;
        
        private LevelConfig _levelConfig = null;

        // columns setup
        private Transform _transform;
        private Transform[] _columns;
        private BlockController[] _blockControllers;
        private List<BlockController> _destroyedBlocks = new List<BlockController>();
        private int _numBlocksInPosition = 0;

        // columns movement
        private float _minColumnMoveSpeed, _maxColumnMoveSpeed, _columnMoveSpeed = 0;
        private float _minColumnMoveDuration, _maxColumnMoveDuration, _columnMoveDuration = 0;
        private int _columnIndex = 0;
        private bool _isGameRunning = false;

        private void Update()
        {
            if (_isGameRunning)
            {
                if (_columnMoveDuration > 0)
                {
                    Vector3 direction = Vector3.forward * (-1f * Time.deltaTime * _columnMoveSpeed);
                    _columns[_columnIndex].Translate(direction);
                    _columnMoveDuration -= Time.deltaTime;
                }
                else
                {
                    _columnMoveSpeed = GetRandomColumnMoveSpeed();
                    _columnMoveDuration = GetRandomColumnMoveDuration();
                    _columnIndex = GetRandomColumnIndex();
                }
            }
        }

        #region EVENTS
        private void OnEnable()
        {
            _transform = this.transform;
            _prepareLevelEvent.Handler += OnPrepareLevel;
            _gameStateEvent.Handler += OnGameStateChange;
            _blockInPositionEvent.Handler += OnBlockInPosition;
            _blockDestroyedEvent.Handler += OnBlockDestroyed;
        }

        private void OnDisable()
        {
            _prepareLevelEvent.Handler -= OnPrepareLevel;
            _gameStateEvent.Handler -= OnGameStateChange;
            _blockInPositionEvent.Handler -= OnBlockInPosition;
            _blockDestroyedEvent.Handler -= OnBlockDestroyed;
        }

        private void OnBlockDestroyed(int blockId)
        {
            int amount = _blockControllers.Length;

            for (int i = 0; i < amount; i++)
            {
                if (_blockControllers[i].GetId() != blockId) continue;

                if (!_destroyedBlocks.Contains(_blockControllers[blockId]))
                {
                    _destroyedBlocks.Add(_blockControllers[blockId]);
                    Debug.Log("_____ found destroyed block! : " + blockId);
                    Debug.Log("Num destroyed Blocks: " + _destroyedBlocks.Count);

                    if (_destroyedBlocks.Count >= amount)
                    {
                        // MRA all blocks are gone!
                        _allBlocksDestroyedEvent?.Dispatch();
                    }
                    // _blockControllers[blockId].DestroyBlock();
                    return;
                }
            }
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
            _levelConfig = config;

            // assign values
            _minColumnMoveSpeed = _levelConfig.MinColumnSpeed;
            _maxColumnMoveSpeed = _levelConfig.MaxColumnSpeed;
            _minColumnMoveDuration = _levelConfig.MinColumnMoveDuration;
            _maxColumnMoveDuration = _levelConfig.MaxColumnMoveDuration;

            SetupBlockColumns(_levelConfig);

            // prepare values for game start
            _columnMoveSpeed = GetRandomColumnMoveSpeed();
            _columnMoveDuration = GetRandomColumnMoveDuration();
            _columnIndex = GetRandomColumnIndex();
        }

        private void OnGameStateChange(GameManager.GameState state)
        {
            switch (state)
            {
                case GameManager.GameState.LEVEL_INTRO:
                    // Blocks should appear
                    ShowBlocks();
                    break;
                case GameManager.GameState.RUNNING:
                    _isGameRunning = true;
                    break;
                case GameManager.GameState.GAME_OVER:
                    _isGameRunning = false;
                    break;
            }
        }

        private void ShowBlocks()
        {
            int numBlocks = _blockControllers.Length;

            for (int i = 0; i < numBlocks; i++)
            {
                _blockControllers[i].StartBlocks();
            }
        }
        #endregion

        #region HELPER METHODS
        private float GetRandomColumnMoveSpeed()
        {
            return Range(_minColumnMoveSpeed, _maxColumnMoveSpeed);
        }

        private float GetRandomColumnMoveDuration()
        {
            return Range(_minColumnMoveDuration, _maxColumnMoveDuration);
        }

        private int GetRandomColumnIndex()
        {
            return Range(0, _columns.Length);
        }

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
                parent.name = "Column_" + columnIndex.ToString();

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
                    tempBlockController.name = "Block_" + rowIndex.ToString();
                    _blockControllers[blockCounter] = tempBlockController;

                    // Add the views as children
                    tempBlockView = Instantiate(blockViewPrefab, tempBlockController.transform);
                    tempBlockView.name = "Block_" + rowIndex.ToString();

                    // blockId is used to make blocks appear in a certain order
                    int blockId = ((numColumns - 1) * rowIndex) + rowIndex + columnIndex;
                    BlockModel model = new BlockModel(config, blockId);
                    tempBlockController.Init(model, tempBlockView.GetComponent<BlockView>());

                    blockCounter++;
                }
            }

            _blocksAreSetupEvent.Dispatch();
        }
        #endregion
    }
}