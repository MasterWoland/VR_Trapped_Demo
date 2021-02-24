using System.Security.Cryptography;
using UnityEngine;

namespace nl.allon.configs
{
    /// <summary>
    /// All Level-specific settings
    /// </summary>
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Config/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        public int LevelNum = 0;
        public string LevelName = string.Empty;
        public GameObject EnvironmentPrefab = default;
        public GameObject LevelInfoPrefab = default;
        public GameObject Deadline = default;
        public Color ScoreColor = default;
        
        // Blocks
        [Header("Blocks")]
        public GameObject BlockControllerPrefab = default;
        public GameObject BlockViewPrefab = default;
        public int NumRows = 0;
        public int NumColumns = 0;
        public float MinColumnSpeed = 0;
        public float MaxColumnSpeed = 0;
        public float MinColumnMoveDuration = 0;
        public float MaxColumnMoveDuration = 0;
        public Vector3 TopLeftPosition = default; // MRA: used to calculate all subsequent positions   
        public int BlockStartHealth = 100;
        public float BlockImpactMultiplier = 0.05f; // Multiplier for the velocity square magnitude. 10000 is a high value for this.
        
        public float GetBlockHeight()
        {
            return BlockViewPrefab.transform.localScale.y;
        }
        
        public float GetBlockWidth()
        {
            return BlockViewPrefab.transform.localScale.x;
        }
        
        public float GetBlockDepth()
        {
            return BlockViewPrefab.transform.localScale.z;
        }

        public Vector3 GetDimensions()
        {
            return BlockViewPrefab.transform.localScale;
        }
    }
}