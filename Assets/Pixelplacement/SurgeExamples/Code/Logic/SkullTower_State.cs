#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class SkullTower_State : State
{
    //Private Variables:
    [SerializeField] SkullTower_Arrow _arrow;
    [SerializeField] Transform _skull;
    Material _skullMaterial;
    Color _initialSkullColor;
    bool _rotating;
    bool _shuttingDown;

    //Init:
    void Awake ()
    {
        _skullMaterial = _skull.GetComponent<Renderer> ().material;
        _initialSkullColor = _skullMaterial.color;
    }

    //Flow:
    void OnEnable ()
    {
        Tween.Color (_skullMaterial, Color.yellow, .25f, 0);
        _arrow.Move (_skull.position);
        InputManagerPP.Instance.OnUpPressed += HandleUpPressed;
        InputManagerPP.Instance.OnDownPressed += HandleDownPressed;
        InputManagerPP.Instance.OnLeftPressed += HandleLeftPressed;
        InputManagerPP.Instance.OnRightPressed += HandleRightPressed;
    }

    void OnDisable ()
    {
        //we don't need this code running when shutting down since OnDisable will be called at that point:
        if (_shuttingDown) return;

        Tween.Color (_skullMaterial, _initialSkullColor, .25f, 0);
        InputManagerPP.Instance.OnUpPressed -= HandleUpPressed;
        InputManagerPP.Instance.OnDownPressed -= HandleDownPressed;
        InputManagerPP.Instance.OnLeftPressed -= HandleLeftPressed;
        InputManagerPP.Instance.OnRightPressed -= HandleRightPressed;
    }

    //Event Handlers:
    void OnApplicationQuit ()
    {
        _shuttingDown = true;
    }

    void HandleUpPressed ()
    {
        Next ();
    }

    void HandleDownPressed ()
    {
        Previous ();
    }

    void HandleLeftPressed ()
    {
        if (_rotating) return;
        _rotating = true;
        Tween.Rotate (_skull, Vector3.up * 90, Space.Self, .3f, 0, Tween.EaseOutBack, Tween.LoopType.None, null, HandleRotationComplete);
    }

    void HandleRightPressed ()
    {
        if (_rotating) return;
        _rotating = true;
        Tween.Rotate (_skull, Vector3.up * -90, Space.Self, .3f, 0, Tween.EaseOutBack, Tween.LoopType.None, null, HandleRotationComplete);
    }

    void HandleRotationComplete ()
    {
        _rotating = false;
    }
}