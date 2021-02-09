#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using UnityEngine.UI;
using System;

[RequireComponent (typeof (CanvasGroup))]
public class CharacterCustomizer_CategorySelectionPanel : State
{
    //Events:
    public event Action OnHelmetButton;
    public event Action OnSwordButton;
    public event Action OnShouldersButton;

    //Private Variables:
    [SerializeField] Button _helmetButton;
    [SerializeField] Button _swordButton;
    [SerializeField] Button _shouldersButton;

    //Flow:
    void OnEnable ()
    {
        Tween.CanvasGroupAlpha (GetComponent<CanvasGroup> (), 0, 1, .5f, 0);
    }

    //Init:
    void Awake ()
    {
        _helmetButton.onClick.AddListener (HandleHelmetButton);
        _swordButton.onClick.AddListener (HandleSwordButton);
        _shouldersButton.onClick.AddListener (HandleShouldersButton);
    }

    //Event Handlers:
    void HandleHelmetButton ()
    {
        if (OnHelmetButton != null) OnHelmetButton ();
    }

    void HandleSwordButton ()
    {
        if (OnSwordButton != null) OnSwordButton ();
    }

    void HandleShouldersButton ()
    {
        if (OnShouldersButton != null) OnShouldersButton ();
    }
}
