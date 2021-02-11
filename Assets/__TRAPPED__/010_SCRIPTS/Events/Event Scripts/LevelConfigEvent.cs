using nl.allon.configs;
using UnityEngine;

namespace nl.allon.events
{
    [CreateAssetMenu(fileName = "LevelConfigEvent", menuName = "Events/LevelConfigEvent")]
    public class LevelConfigEvent : ScriptableObject
    {
        public delegate void EventHandler(LevelConfig config);
        public EventHandler Handler;

        public void Dispatch(LevelConfig config)
        {
            Handler?.Invoke(config);
        }
    }
}