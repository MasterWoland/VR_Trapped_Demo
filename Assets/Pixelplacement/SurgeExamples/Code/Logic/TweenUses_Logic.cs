#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class TweenUses_Logic : MonoBehaviour
{
    //Private Variables:
    [SerializeField] Transform _target;
    [SerializeField] Renderer _lensRenderer;
    [SerializeField] float _tweenDuration;
    [SerializeField] float _movementAmount;
    [SerializeField] float _rotateByAmount;
    [SerializeField] float _wobbleDuration;
    [SerializeField] float _wobbleAmount;
    [SerializeField] float _scaleAmount;
    [SerializeField] Vector3 _shakeIntensity;
    [SerializeField] float _shakeDuration;
    [SerializeField] Color[] _lensColors;
    [SerializeField] float _windUpAmount;
    [SerializeField] float _windUpDuration;
    Vector3 _initialPosition;
    Quaternion _initialRotation;
    Vector3 _initialScale;
    float _currentLensColor;

    //Init:
    void Reset ()
    {
        _tweenDuration = .25f;
        _movementAmount = .2f;
        _rotateByAmount = 45;
        _wobbleDuration = .6f;
        _wobbleAmount = 30;
        _scaleAmount = .25f;
        _shakeIntensity = Vector3.one * .015f;
        _shakeDuration = .6f;
        _windUpAmount = 30;
        _windUpDuration = .4f;
        _lensColors = new Color[] {Color.red, Color.blue, Color.yellow, Color.green, Color.cyan};
    }

    void Awake ()
    {
        _initialPosition = _target.position;
        _initialRotation = _target.rotation;
        _initialScale = _target.localScale;
    }

    //GUI
    void OnGUI ()
    {
        GUILayout.BeginVertical ();
        ResetButton ();
        GUILayout.BeginHorizontal ();
        MoveUpButton ();
        MoveDownButton ();
        GUILayout.EndHorizontal ();
        GUILayout.BeginHorizontal ();
        MoveLeftButton ();
        MoveRightButton ();
        GUILayout.EndHorizontal ();
        GUILayout.BeginHorizontal ();
        MoveForwardButton ();
        MoveBackButton ();
        GUILayout.EndHorizontal ();
        GUILayout.BeginHorizontal ();
        ScaleUpButton ();
        ScaleDownButton ();
        GUILayout.EndHorizontal ();
        RotateByButton ();
        WobbleButton ();
        ShakeButton ();
        ChangeLensColorButton ();
        WindUpButton ();
        GUILayout.EndVertical ();
    }

    //Button Methods
    void WindUpButton ()
    {
        if (GUILayout.Button ("Wind Up"))
        {
            Tween.Rotate (_target, Vector3.forward * -_windUpAmount, Space.Self, _windUpDuration, 0, Tween.EaseOut);	
            Tween.Rotate (_target, Vector3.forward * (360 + _windUpAmount), Space.Self, _windUpDuration * 3.5f, _windUpDuration, Tween.EaseInOutStrong);
        }
    }

    void ShakeButton ()
    {
        if (GUILayout.Button ("Shake"))
        {
            Tween.Shake (_target, _target.position, _shakeIntensity, _shakeDuration, 0);	
        }
    }

    void ChangeLensColorButton ()
    {
        if (GUILayout.Button ("Change Lens Color"))
        {
            int nextColorIndex = (int)Mathf.Repeat (_currentLensColor, _lensColors.Length - 1);
            Tween.ShaderColor (_lensRenderer.material, "_EmissionColor", _lensColors[nextColorIndex], _tweenDuration, 0);
            _currentLensColor++;
        }
    }

    void ResetButton ()
    {
        if (GUILayout.Button ("Reset"))
        {
            _currentLensColor = 0;
            Tween.ShaderColor (_lensRenderer.material, "_EmissionColor", _lensColors[(int)_currentLensColor], _tweenDuration, 0);
            Tween.Position (_target, _initialPosition, _tweenDuration, 0, Tween.EaseInOutStrong);
            Tween.Rotation (_target, _initialRotation, _tweenDuration, 0, Tween.EaseInOutStrong);
            Tween.LocalScale (_target, _initialScale, _tweenDuration, 0, Tween.EaseInOutStrong);
        }
    }

    void ScaleUpButton ()
    {
        if (GUILayout.Button ("Scale Up"))
        {
            Tween.LocalScale (_target, _target.localScale + (Vector3.one * _scaleAmount), _tweenDuration, 0, Tween.EaseOutBack);
        }
    }

    void ScaleDownButton ()
    {
        if (GUILayout.Button ("Scale Down"))
        {
            Tween.LocalScale (_target, _target.localScale - (Vector3.one * _scaleAmount), _tweenDuration, 0, Tween.EaseOutBack);
        }
    }

    void MoveForwardButton ()
    {
        if (GUILayout.Button ("Move Forward"))
        {
            Tween.Position (_target, _target.position + _target.forward * _movementAmount, _tweenDuration, 0, Tween.EaseOutBack);
        }
    }

    void MoveBackButton ()
    {
        if (GUILayout.Button ("Move Back"))
        {
            Tween.Position (_target, _target.position + _target.forward * -_movementAmount, _tweenDuration, 0, Tween.EaseOutBack);
        }
    }

    void MoveUpButton ()
    {
        if (GUILayout.Button ("Move Up"))
        {
            Tween.Position (_target, _target.position + _target.up * _movementAmount, _tweenDuration, 0, Tween.EaseOutBack);
        }
    }

    void MoveDownButton ()
    {
        if (GUILayout.Button ("Move Down"))
        {
            Tween.Position (_target, _target.position + _target.up * -_movementAmount, _tweenDuration, 0, Tween.EaseOutBack);
        }
    }

    void MoveLeftButton ()
    {
        if (GUILayout.Button ("Move Left"))
        {
            Tween.Position (_target, _target.position + _target.right * -_movementAmount, _tweenDuration, 0, Tween.EaseOutBack);
        }
    }

    void MoveRightButton ()
    {
        if (GUILayout.Button ("Move Right"))
        {
            Tween.Position (_target, _target.position + _target.right * _movementAmount, _tweenDuration, 0, Tween.EaseOutBack);
        }
    }

    void RotateByButton ()
    {
        if (GUILayout.Button ("Rotate By"))
        {
            Tween.Rotate (_target, Vector3.up * _rotateByAmount, Space.Self, _tweenDuration, 0, Tween.EaseOutBack);
        }
    }

    void WobbleButton ()
    {
        if (GUILayout.Button ("Wobble"))
        {
            Quaternion currentRotation = _target.rotation;
            _target.Rotate (Vector3.forward * _wobbleAmount);
            Tween.Rotation (_target, currentRotation, _target.rotation, _wobbleDuration, 0, Tween.EaseWobble); 
        }
    }
}