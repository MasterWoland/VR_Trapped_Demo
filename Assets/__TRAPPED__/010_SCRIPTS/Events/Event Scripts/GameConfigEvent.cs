using nl.allon.configs;
using UnityEngine;

namespace nl.allon.events
{
    [CreateAssetMenu(fileName = "GameConfigEvent", menuName = "Events/GameConfigEvent")]
    public class GameConfigEvent : ScriptableObject
    {
        public delegate void EventHandler(GameConfig config);
        public EventHandler Handler;

        public void Dispatch(GameConfig config)
        {
            Handler?.Invoke(config);
        }
    }
}