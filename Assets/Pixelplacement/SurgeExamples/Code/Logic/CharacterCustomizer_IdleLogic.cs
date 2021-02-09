#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class CharacterCustomizer_IdleLogic : State
{
    //Private Variables:
    [SerializeField] CharacterCustomizer_CategorySelectionPanel _guiPanel;

    //Init:
    void Awake ()
    {
        _guiPanel.OnHelmetButton += HandleHelmetButton;
        _guiPanel.OnSwordButton += HandleSwordButton;
        _guiPanel.OnShouldersButton += HandleShouldersButton;
    }

    //Flow:
    void OnEnable ()
    {
        _guiPanel.ChangeState (_guiPanel.name);
    }

    //Event Handlers:
    void HandleHelmetButton ()
    {
        ChangeState ("Helmet");
    }

    void HandleSwordButton ()
    {
        ChangeState ("Sword");
    }

    void HandleShouldersButton ()
    {
        ChangeState ("Shoulders");
    }
}