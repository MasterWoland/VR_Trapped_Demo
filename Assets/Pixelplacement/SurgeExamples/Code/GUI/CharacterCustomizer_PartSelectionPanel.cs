#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Pixelplacement;

public class CharacterCustomizer_PartSelectionPanel : State
{
    //Events:
    public event Action OnPreviousButton;
    public event Action OnNextButton;
    public event Action OnCloseButton;

    //Private Variables:
    [SerializeField] Button _previousButton;
    [SerializeField] Button _nextButton;
    [SerializeField] Button _closeButton;

    //Flow:
    void OnEnable ()
    {
        Tween.CanvasGroupAlpha (GetComponent<CanvasGroup> (), 0, 1, .5f, 0);
    }

    //Init:
    void Awake ()
    {
        _previousButton.onClick.AddListener (HandlePreviousButton);
        _nextButton.onClick.AddListener (HandleNextButton);
        _closeButton.onClick.AddListener (HandleCloseButton);
    }

    //Event Handlers:
    void HandlePreviousButton ()
    {
        if (OnPreviousButton != null) OnPreviousButton ();
    }

    void HandleNextButton ()
    {
        if (OnNextButton != null) OnNextButton ();
    }

    void HandleCloseButton ()
    {
        if (OnCloseButton != null) OnCloseButton ();
    }
}
