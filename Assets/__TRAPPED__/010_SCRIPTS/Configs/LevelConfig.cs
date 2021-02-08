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
        
        // Blocks
        public GameObject BlockPrefab = default;
        public int NumRows = 0;
        public int NumColumns = 0;
        public float MinColumnSpeed = 0;
        public float MaxColumnSpeed = 0;
            
        public float GetBlockHeight()
        {
            return BlockPrefab.transform.localScale.y;
        }
        
        public float GetBlockWidth()
        {
            return BlockPrefab.transform.localScale.x;
        }
        
        public float GetBlockDepth()
        {
            return BlockPrefab.transform.localScale.z;
        }
    }
}