#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class CharacterCustomizer_IntroLogic : State 
{
    //Private Variables:
    [SerializeField] Transform _cameraIntroStart;
    [SerializeField] Transform _cameraIntroEnd;
    [SerializeField] float _duration = 3;

    //Flow:
    void OnEnable ()
    {
        Tween.Position (Camera.main.transform, _cameraIntroStart.position, _cameraIntroEnd.position, _duration, 0, Tween.EaseInOut);
        Tween.Rotation (Camera.main.transform, _cameraIntroStart.rotation, _cameraIntroEnd.rotation, _duration, 0, Tween.EaseInOut, Tween.LoopType.None, null, HandleIntroComplete);
    }

    //Event Handlers:
    void HandleIntroComplete ()
    {
        ChangeState ("Idle");
    }
}
