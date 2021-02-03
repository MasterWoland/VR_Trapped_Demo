using System.Collections;
using System.Collections.Generic;
using nl.allon.managers;
using UnityEngine;

namespace nl.allon.events
{
    [CreateAssetMenu(fileName = "SceneEvent", menuName = "Events/SceneEvent")]
    public class SceneEvent : ScriptableObject
    {
        public delegate void EventHandler(SCENE_NAME sceneName);
        public EventHandler Handler;

        public void Dispatch(SCENE_NAME sceneName)
        {
            Handler?.Invoke(sceneName);
        }
    }
}