#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class CharacterCustomizer_CategorySelectionLogic : State
{
    //Private Variables:
    [SerializeField] StateMachine _partStateMachine;
    [SerializeField] CharacterCustomizer_PartSelectionPanel _guiPanel;

    //Flow:
    void OnEnable ()
    {
        _guiPanel.ChangeState (_guiPanel.name);
    }

    //Init:
    void Awake ()
    {
        _guiPanel.OnNextButton += HandleNextButton;
        _guiPanel.OnPreviousButton += HandlePreviousButton;
        _guiPanel.OnCloseButton += HandleCloseButton;
    }

    //Event Handlers:
    void HandleNextButton ()
    {
        _partStateMachine.Next ();
    }

    void HandlePreviousButton ()
    {
        _partStateMachine.Previous ();
    }

    void HandleCloseButton ()
    {
        ChangeState ("Idle");
    }
}