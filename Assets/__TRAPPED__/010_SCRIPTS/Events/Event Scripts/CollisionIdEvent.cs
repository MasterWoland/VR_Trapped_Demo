using UnityEngine;

namespace nl.allon.events
{
    [CreateAssetMenu(fileName = "CollisionIdEvent", menuName = "Events/CollisionIdEvent")]
    public class CollisionIdEvent : ScriptableObject
    {
        public delegate void EventHandler(Collision collision, int id);
        public EventHandler Handler;

        public void Dispatch(Collision collision, int id)
        {
            Handler?.Invoke(collision, id);
        }
    }
}