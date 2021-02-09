#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pixelplacement
{
    [ExecuteInEditMode]
    public class MatchNearClip : MonoBehaviour
    {
        //Private Variables:
        [SerializeField] Camera _camera;
        [SerializeField] float _offset = .01f;

        //Init:
        void LateUpdate ()
        {
            //don't proceed until a camera has been set:
            if (_camera == null) return; 

            GetComponent<Canvas> ().planeDistance = _camera.nearClipPlane + _offset;
        }
    }
}