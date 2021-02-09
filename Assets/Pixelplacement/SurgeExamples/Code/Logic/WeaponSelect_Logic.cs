#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pixelplacement;

public class WeaponSelect_Logic : MonoBehaviour
{
    //Private Variables:
    [SerializeField] StateMachine _weaponsStateMachine;
    [SerializeField] Button _handgunButton;
    [SerializeField] Button _fourShotButton;
    [SerializeField] Button _armShotButton;
    [SerializeField] Vector3 _shakeAmount = new Vector3 (.015f, .015f, .015f);
    [SerializeField] float _shakeDuration = .3f;
    Vector3 _initialCameraPosition;

    //Init:
    void Awake ()
    {
        _initialCameraPosition = Camera.main.transform.position;
        _handgunButton.onClick.AddListener (HandleHandgunButton);
        _fourShotButton.onClick.AddListener (HandleFourShotButton);
        _armShotButton.onClick.AddListener (HandleArmShotButton);
    }

    //Event Handlers:
    void HandleHandgunButton ()
    {
        Shake ();
        _weaponsStateMachine.ChangeState ("Handgun");
    }

    void HandleFourShotButton ()
    {
        Shake ();
        _weaponsStateMachine.ChangeState ("FourShot");
    }

    void HandleArmShotButton ()
    {
        Shake ();
        _weaponsStateMachine.ChangeState ("ArmShot");
    }

    //Private Methods:
    void Shake ()
    {
        Tween.Shake (Camera.main.transform.transform, _initialCameraPosition, _shakeAmount, _shakeDuration, 0);
    }
}