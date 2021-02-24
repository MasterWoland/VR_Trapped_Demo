using System.Collections;
using System.Collections.Generic;
using nl.allon.configs;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

namespace nl.allon.models
{
    public class BlockModel
    {
        public float Width;
        public float Height;
        public float Depth;
        public Vector3 StartPosition;
        public int BlockId;
        private LevelConfig _config;
        public LevelConfig Config => _config;
        private int _health;
        public int Health { get => _health; set => _health = value; }

        public BlockModel(LevelConfig config, int id)
        {
            _config = config;
            Width = _config.GetDimensions().x;
            Height = _config.GetDimensions().y;
            Depth = _config.GetDimensions().z;
            _health = _config.BlockStartHealth;
            BlockId = id;
        }
    }
}