using UnityEngine;

namespace nl.allon.configs
{
    /// <summary>
    /// All Player-specific settings
    /// </summary>
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Config/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        private bool _useRightHandForRacket = true;
        public bool UseRightHandForRacket => _useRightHandForRacket;
        [SerializeField] private int _numBalls = 40;
        public int NumBalls => _numBalls;

        public void SetRacketHand(bool useRightHand)
        {
            _useRightHandForRacket = useRightHand;
        }
    }
}