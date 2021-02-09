#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using UnityEngine.UI;
using System;

public class CarPartsStore_ConfirmationPanel : DisplayObject
{
    //Private Variables:
    [SerializeField] Text _text;
    [SerializeField] Button _yesButton;
    [SerializeField] Button _noButton;
    Action _yesHandler;

    //Init:
    void Awake ()
    {
        _yesButton.onClick.AddListener (HandleYesButton);
        _noButton.onClick.AddListener (HandleNoButton);
    }

    //Public Methods:
    public void Show (string text, Action yesHandler)
    {
        //populate the text:
        _text.text = text;

        //turn panel on:
        SetActive (true);

        //cache yes handler:
        _yesHandler = yesHandler;

        //ensure this panel is above everything else:
        transform.SetAsLastSibling ();
    }

    //Event Handlers:
    void HandleYesButton ()
    {
        _yesHandler ();
        SetActive (false);
    }

    void HandleNoButton ()
    {
        SetActive (false);
    }
}