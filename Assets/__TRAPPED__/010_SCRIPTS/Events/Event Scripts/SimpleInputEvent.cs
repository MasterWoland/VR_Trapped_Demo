using nl.allon.managers;
using UnityEngine;

namespace nl.allon.events
{
    [CreateAssetMenu(fileName = "SimpleInputEvent", menuName = "Events/SimpleInputEvent")]
    public class SimpleInputEvent : ScriptableObject
    {
        public delegate void EventHandler(Hand hand);
        public EventHandler Handler;

        public void Dispatch(Hand hand)
        {
            Handler?.Invoke(hand);
        }
    }
}