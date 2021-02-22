using System;
using System.Collections;
using System.Collections.Generic;
using nl.allon.events;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace nl.allon.managers
{
    public enum Hand
    {
        LEFT,
        RIGHT
    }

    public class InputManager : MonoBehaviour
    {
        public Hand CurrentHand = default;

        [SerializeField] private SimpleInputEvent _selectStartedEvent;
        [SerializeField] private SimpleInputEvent _selectCanceledEvent;
        [SerializeField] private SimpleInputEvent _activatePerformedEvent;
        [SerializeField] private ActionBasedController _controller = default;

        #region EVENTS
        private void OnEnable()
        {
            _controller.selectAction.action.started += OnSelectStarted;
            _controller.selectAction.action.canceled += OnSelectCanceled;
            // _controller.activateAction.action.started += OnActivateStarted;
            _controller.activateAction.action.performed += OnActivatePerformed;
        }

        private void OnDisable()
        {
            _controller.selectAction.action.started -= OnSelectStarted;
            _controller.selectAction.action.canceled -= OnSelectCanceled;
            // _controller.activateAction.action.started -= OnActivateStarted;
            _controller.activateAction.action.performed -= OnActivatePerformed;
        }

        private void OnSelectCanceled(InputAction.CallbackContext context)
        {
            _selectCanceledEvent?.Dispatch(CurrentHand);
        }

        private void OnSelectStarted(InputAction.CallbackContext context)
        {
            _selectStartedEvent?.Dispatch(CurrentHand);
        }

        private void OnActivatePerformed(InputAction.CallbackContext context)
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