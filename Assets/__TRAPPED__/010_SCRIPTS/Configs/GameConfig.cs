using UnityEngine;

namespace nl.allon.configs
{
    /// <summary>
    /// All Game-specific settings
    /// </summary>
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Config/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        public GameObject BallHitWallParticle = default;
    }
}