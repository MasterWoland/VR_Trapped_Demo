using System.Collections;
using System.Collections.Generic;
using nl.allon.managers;
using UnityEngine;

namespace nl.allon.events
{
    [CreateAssetMenu(fileName = "IntegerEvent", menuName = "Events/IntegerEvent")]
    public class IntEvent : ScriptableObject
    {
        public delegate void EventHandler(int integer);
        public EventHandler Handler;

        public void Dispatch(int integer)
        {
            Handler?.Invoke(integer);
        }
    }
}