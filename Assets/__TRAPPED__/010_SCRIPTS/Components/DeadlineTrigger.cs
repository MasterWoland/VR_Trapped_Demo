using System;
using System.Collections;
using System.Collections.Generic;
using nl.allon.events;
using UnityEngine;

namespace nl.allon.components
{
    public class DeadlineTrigger : MonoBehaviour
    {
        [SerializeField] private SimpleEvent _deadlineTriggeredEvent = default;
        
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("[Deadline] Triggered by: "+other.name);
            _deadlineTriggeredEvent?.Dispatch();
        }
    }
}