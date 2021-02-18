using System;
using System.Collections;
using System.Collections.Generic;
using nl.allon.data;
using UnityEngine;

namespace nl.allon.managers
{
    /// <summary>
    /// Applies the correct controller, hand or attribute to the hand
    /// </summary>
    public class HandManager : MonoBehaviour
    {
       
        [SerializeField] private DeviceData _deviceData = default;

        private void Awake()
        {
            Debug.Log("[HandManager] Platform = "+_deviceData.CurrentPlatform);
        }
    }
}