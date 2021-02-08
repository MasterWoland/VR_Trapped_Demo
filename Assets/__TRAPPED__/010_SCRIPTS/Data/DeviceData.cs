using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

[CreateAssetMenu(fileName = "DeviceData", menuName = "SO/DeviceData")]
public class DeviceData : ScriptableObject
{
    private string _deviceName = string.Empty;
    public string DeviceName { get { return _deviceName; } }
  
    public bool TryObtainDeviceName()
    {
        _deviceName = XRSettings.loadedDeviceName;

        if (string.IsNullOrEmpty(_deviceName))
        {
            Debug.LogError("[DeviceData] no XR device found.");
            return false;
        }
        else
        {
            Debug.Log("[DeviceData] XR device = " + _deviceName);
            return true;
        }
    }
}