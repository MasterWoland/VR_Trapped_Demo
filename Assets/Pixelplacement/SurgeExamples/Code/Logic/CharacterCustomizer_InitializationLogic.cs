#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class CharacterCustomizer_InitializationLogic : State
{
    //Private Variables:
    [SerializeField] StateMachine _weaponStateMachine;
    [SerializeField] StateMachine _helmetStateMachine;
    [SerializeField] StateMachine _shoulderPadsStateMachine;

    //Flow:
    void OnEnable ()
    {
        //reload previous user choice:
        _weaponStateMachine.ChangeState (CharacterCustomizer_Parameters.SelectedWeapon);
        _helmetStateMachine.ChangeState (CharacterCustomizer_Parameters.SelectedHelmet);
        _shoulderPadsStateMachine.ChangeState (CharacterCustomizer_Parameters.SelectedShoulderPads);

        Debug.Log ("Previous configuration loaded.");
        ChangeState ("Intro");
    }
}
