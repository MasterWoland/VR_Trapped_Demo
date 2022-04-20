using UnityEngine;

namespace nl.allon.events
{
    [CreateAssetMenu(fileName = "Vector3FloatEvent", menuName = "Events/Vector3FloatEvent")]
    public class Vector3FloatEvent : ScriptableObject
    {
        public delegate void EventHandler(Vector3 v3, float value);
        public EventHandler Handler;

        public void Dispatch(Vector3 v3, float value)
        {
            Handler?.Invoke(v3, value);
        }
    }
}