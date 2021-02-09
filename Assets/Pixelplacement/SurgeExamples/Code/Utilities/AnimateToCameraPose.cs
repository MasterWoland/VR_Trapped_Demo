#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

namespace Pixelplacement
{
    public class AnimateToCameraPose : MonoBehaviour
    {
        //Private Variables:
        [SerializeField] Transform _camera;
        [SerializeField] float _transitionDuration = 2;
        
        //Flow:
        void OnEnable ()
        {
            Tween.Position (Camera.main.transform, _camera.position, _transitionDuration, 0, Tween.EaseInOut);
            Tween.Rotation (Camera.main.transform, _camera.rotation, _transitionDuration, 0, Tween.EaseInOut);
        }
    }
}