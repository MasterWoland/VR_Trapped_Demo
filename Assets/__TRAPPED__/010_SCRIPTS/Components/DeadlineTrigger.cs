using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nl.allon.components
{
    public class DeadlineTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("[Deadline] Triggered by: "+other.name);
        }
    }
}