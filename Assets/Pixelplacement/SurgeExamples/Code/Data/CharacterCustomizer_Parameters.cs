#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class CharacterCustomizer_Parameters : MonoBehaviour
{
    //Private Variables:
    [SerializeField] StateMachine _weaponStateMachine;
    [SerializeField] StateMachine _helmetStateMachine;
    [SerializeField] StateMachine _shoulderPadsStateMachine;

    //Public Properties:
    public static string SelectedWeapon
    {
        get
        {
            return PlayerPrefs.GetString ("CharacterCustomizer_Weapon", "Empty");
        }

        set
        {
            PlayerPrefs.SetString ("CharacterCustomizer_Weapon", value);
        }
    }

    public static string SelectedHelmet
    {
        get
        {
            return PlayerPrefs.GetString ("CharacterCustomizer_Helmet", "Empty");
        }

        set
        {
            PlayerPrefs.SetString ("CharacterCustomizer_Helmet", value);
        }
    }

    public static string SelectedShoulderPads
    {
        get
        {
            return PlayerPrefs.GetString ("CharacterCustomizer_ShoulderPads", "Empty");
        }

        set
        {
            PlayerPrefs.SetString ("CharacterCustomizer_ShoulderPads", value);
        }
    }

    //Event Handlers:
    void OnApplicationQuit ()
    {
        //save current choices or null if nothing has been selected:
        SelectedWeapon = _weaponStateMachine.currentState.name;
        SelectedHelmet = _helmetStateMachine.currentState.name;
        SelectedShoulderPads = _shoulderPadsStateMachine.currentState.name;
    }
}