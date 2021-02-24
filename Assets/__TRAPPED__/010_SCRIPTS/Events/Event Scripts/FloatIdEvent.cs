using UnityEngine;

namespace nl.allon.events
{
    [CreateAssetMenu(fileName = "FloatIdEvent", menuName = "Events/FloatIdEvent")]
    public class FloatIdEvent : ScriptableObject
    {
        public delegate void EventHandler(int id, float value);
        public EventHandler Handler;

        public void Dispatch(int id, float value)
        {
            Handler?.Invoke(id, value);
        }
    }
}