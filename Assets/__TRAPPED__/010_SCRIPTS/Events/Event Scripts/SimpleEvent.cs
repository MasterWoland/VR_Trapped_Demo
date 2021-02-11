using UnityEngine;

namespace nl.allon.events
{
    [CreateAssetMenu(fileName = "SimpleEvent", menuName = "Events/SimpleEvent")]
    public class SimpleEvent : ScriptableObject
    {
        public delegate void EventHandler();
        public EventHandler Handler;

        public void Dispatch()
        {
            Handler?.Invoke();
        }
    }
}