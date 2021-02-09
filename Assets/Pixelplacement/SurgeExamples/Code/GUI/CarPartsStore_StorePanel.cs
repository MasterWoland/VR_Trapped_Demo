#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Pixelplacement;

public class CarPartsStore_StorePanel : MonoBehaviour
{
    //Events:
    public event Action OnSuperchargerButton;
    public event Action OnSpoilerButton;

    //Private Variables:
    [SerializeField] Button _superchargerButton;
    [SerializeField] Button _spoilerButton;
    [SerializeField] Text _superchargetText;
    [SerializeField] Text _spoilerText;
    [SerializeField] Text _cashOnHandText;
    int _previousCashOnHandAmount;

    //Init:
    void Awake ()
    {
        //register for button clicks:
        _superchargerButton.onClick.AddListener (HandleSuperchargerButton);
        _spoilerButton.onClick.AddListener (HandleSpoilerButton);

        //set static visual values:
        _superchargetText.text += " $" + CarPartsStore_StoreData.Instance.superchargerCost;
        _spoilerText.text += " $" + CarPartsStore_StoreData.Instance.spoilerCost;

        //match cash cache (see what I did there?) so we aren't counting up at the start:
        _previousCashOnHandAmount = CarPartsStore_StoreData.Instance.cashOnHand;
        _cashOnHandText.text = "$" + _previousCashOnHandAmount;
    }

    //Loops:
    void Update ()
    {
        //synchronize cash on hand display with a little flare:
        if (_previousCashOnHandAmount != CarPartsStore_StoreData.Instance.cashOnHand)
        {
            Tween.Value (_previousCashOnHandAmount, CarPartsStore_StoreData.Instance.cashOnHand, HandleCashDisplayUpdate, 1f, 0);
            _previousCashOnHandAmount = CarPartsStore_StoreData.Instance.cashOnHand;
        }
    }

    //Public Methods:
    public void DisableSuperchargerButton ()
    {
        _superchargerButton.interactable = false;
    }

    public void DisableSpoilerButton ()
    {
        _spoilerButton.interactable = false;
    }

    //Event Handlers:
    void HandleCashDisplayUpdate (int cashUpdate)
    {
        _cashOnHandText.text = "$" + cashUpdate;

        //if cash on hand goes negative color the text red:
        if (cashUpdate < 0) _cashOnHandText.color = Color.red;
    }

    void HandleSuperchargerButton ()
    {
        if (OnSuperchargerButton != null) OnSuperchargerButton ();
    }

    void HandleSpoilerButton ()
    {
        if (OnSpoilerButton != null) OnSpoilerButton ();
    }
}