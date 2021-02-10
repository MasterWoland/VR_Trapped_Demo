using System;
using System.Collections;
using System.Collections.Generic;
using nl.allon.events;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace nl.allon.managers
{
    public class InputManager : MonoBehaviour
    {
        public enum Hand
        {
            LEFT,
            RIGHT
        }
        public Hand CurrentHand = default;

        [SerializeField] private HandSimpleInputEvent _activatePerformedEvent;
        [SerializeField] private ActionBasedController _controller = default;
        
        #region EVENTS
        private void OnEnable()
        {
            // _controller.activateAction.action.started += OnActivateStarted;
            _controller.activateAction.action.performed += OnActivatePerformed;
        }
        private void OnDisable()
        {
            // _controller.activateAction.action.started -= OnActivateStarted;
            _controller.activateAction.action.performed -= OnActivatePerformed;
        }

        private void OnActivatePerformed(InputAction.CallbackContext obj)
        {
            
            _activatePerformedEvent?.Dispatch(CurrentHand);
        }

        // private void OnActivateStarted(InputAction.CallbackContext obj)
        // {
        //     Debug.Log("[INPUT] Activate Started");
        // }
        #endregion
    }
}