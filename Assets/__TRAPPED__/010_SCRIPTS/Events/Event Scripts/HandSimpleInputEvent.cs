using nl.allon.managers;
using UnityEngine;

namespace nl.allon.events
{
    [CreateAssetMenu(fileName = "HandSimpleInputEvent", menuName = "Events/HandSimpleInputEvent")]
    public class HandSimpleInputEvent : ScriptableObject
    {
        public delegate void EventHandler(InputManager.Hand hand);
        public EventHandler Handler;

        public void Dispatch(InputManager.Hand hand)
        {
            Handler?.Invoke(hand);
        }
    }
}