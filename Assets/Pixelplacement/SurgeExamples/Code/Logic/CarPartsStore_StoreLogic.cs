#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class CarPartsStore_StoreLogic : MonoBehaviour
{
    //Private Variables:
    [SerializeField] DisplayObject _spoilerVisual;
    [SerializeField] DisplayObject _superchargerVisual;
    [SerializeField] CarPartsStore_ConfirmationPanel _confirmationPanel;
    [SerializeField] CarPartsStore_StorePanel _storePanel;
    [SerializeField] Transform _carFrame;

    //Init:
    void Awake ()
    {
        _storePanel.OnSpoilerButton += HandleSpoilerButton;
        _storePanel.OnSuperchargerButton += HandleSuperchargerButton;
    }

    //Event Handlers:
    void HandleSpoilerButton ()
    {
        _confirmationPanel.Show ("ARE YOU SURE YOU WANT TO PURCHASE THE SPOILER?", PurchaseSpoiler);
    }

    void HandleSuperchargerButton ()
    {
        _confirmationPanel.Show ("ARE YOU SURE YOU WANT TO PURCHASE THE SUPER CHARGER?", PurchaseSupercharger);
    }

    //Private Methods:
    void PurchaseSpoiler ()
    {
        CarPartsStore_StoreData.Instance.cashOnHand -= CarPartsStore_StoreData.Instance.spoilerCost;
        _storePanel.DisableSpoilerButton ();
        _spoilerVisual.SetActive (true);

        //wobble the car towards the spoiler:
        Tween.LocalRotation (_carFrame, new Vector3 (-3, 0, -2), .65f, 0, Tween.EaseWobble);
    }

    void PurchaseSupercharger ()
    {
        CarPartsStore_StoreData.Instance.cashOnHand -= CarPartsStore_StoreData.Instance.superchargerCost;
        _storePanel.DisableSuperchargerButton ();
        _superchargerVisual.SetActive (true);

        //wobble the car towards the hood:
        Tween.LocalRotation (_carFrame, new Vector3 (3, 0, 2), .65f, 0, Tween.EaseWobble);
    }
}