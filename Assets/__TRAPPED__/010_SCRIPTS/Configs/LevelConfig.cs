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
        
        // Blocks
        public GameObject BlockControllerPrefab = default;
        public GameObject BlockViewPrefab = default;
        public int NumRows = 0;
        public int NumColumns = 0;
        public float MinColumnSpeed = 0;
        public float MaxColumnSpeed = 0;
        public Vector3 TopLeftPosition = default; // MRA: used to calculate all subsequent positions   
        
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