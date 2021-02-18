using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

namespace nl.allon.data
{
    [CreateAssetMenu(fileName = "DeviceData", menuName = "SO/DeviceData")]
    public class DeviceData : ScriptableObject
    {
        public enum Platform
        {
            UNKNOWN,
            QUEST_01,
            OCULUS_RIFT,
            HTC_VIVE,
            VALVE_INDEX
        }
        private Platform _currentPlatform = Platform.UNKNOWN;
        public Platform CurrentPlatform => _currentPlatform;

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
                Debug.Log("[DeviceData] XR device = " + SystemInfo.deviceModel);
                
                // MRA: we need a more extensive method of determining which platform we are on.
                // MRA: unfortunately Unity does not offer a good way of doing so currently

                if (_deviceName.ToLower().Contains("oculus"))
                {
                    if (Application.platform == RuntimePlatform.Android)
                    {
                        _currentPlatform = Platform.QUEST_01;
                    }
                    else
                    {
                        _currentPlatform = Platform.OCULUS_RIFT;
                    }
                }
                else
                {
                    //TODO: it could be either Index or Vive. For now we choose index.
                    _currentPlatform = Platform.VALVE_INDEX;
                }
                
                return true;
            }
        }
    }
}