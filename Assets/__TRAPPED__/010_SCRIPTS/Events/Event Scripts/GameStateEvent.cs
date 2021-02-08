using System.Collections;
using System.Collections.Generic;
using nl.allon.managers;
using UnityEngine;

namespace nl.allon.events
{
    [CreateAssetMenu(fileName = "GameStateEvent", menuName = "Events/GameStateEvent")]
    public class GameStateEvent : ScriptableObject
    {
        public delegate void EventHandler(GameManager.GameState state);
        public EventHandler Handler;

        public void Dispatch(GameManager.GameState state)
        {
            Handler?.Invoke(state);
        }
    }
}