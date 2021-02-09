#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class WeaponSelect_State : State
{
    //Private Variables:
    [SerializeField] DisplayObject[] _dependantContent;

    //Flow:
    void OnEnable ()
    {
        foreach (var item in _dependantContent)
        {
            item.SetActive (true); 
        }
    }

    void OnDisable ()
    {
        foreach (var item in _dependantContent)
        {
            item.SetActive (false); 
        }
    }
}