using UnityEngine;

namespace nl.allon.events
{
    [CreateAssetMenu(fileName = "FloatEvent", menuName = "Events/FloatEvent")]
    public class FloatEvent : ScriptableObject
    {
        public delegate void EventHandler(float value);
        public EventHandler Handler;

        public void Dispatch(float value)
        {
            Handler?.Invoke(value);
        }
    }
}