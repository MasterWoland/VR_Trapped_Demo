using System.Collections;
using nl.allon.data;
using nl.allon.events;
using UnityEngine;

namespace nl.allon.utils
{
    public class ObtainDeviceData : MonoBehaviour
    {
        [SerializeField] private DeviceData _deviceData;
        [SerializeField] private SimpleEvent _deviceDataObtainedEvent;
        private Coroutine _currentCoroutine = null;

        private IEnumerator Start()
        {
            _currentCoroutine = StartCoroutine(TryObtainDeviceName());
            yield return _currentCoroutine;

            _deviceDataObtainedEvent.Dispatch();
            Destroy(this.gameObject); // we no longer need this object
        }
       
        // MRA: the only data we are storing right now is the Device Name
        private IEnumerator TryObtainDeviceName()
        {
            bool hasObtainedDeviceName = false;
            while (!hasObtainedDeviceName)
            {
                hasObtainedDeviceName = _deviceData.TryObtainDeviceName();
                yield return null;
            }
        }
    }
}