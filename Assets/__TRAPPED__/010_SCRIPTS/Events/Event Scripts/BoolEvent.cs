using System.Collections;
using System.Collections.Generic;
using nl.allon.managers;
using UnityEngine;

namespace nl.allon.events
{
    [CreateAssetMenu(fileName = "BoolEvent", menuName = "Events/BoolEvent")]
    public class BoolEvent : ScriptableObject
    {
        public delegate void EventHandler(bool boolean);
        public EventHandler Handler;

        public void Dispatch(bool boolean)
        {
            Handler?.Invoke(boolean);
        }
    }
}