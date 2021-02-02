using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TestActionInput : MonoBehaviour
{
    private ActionBasedController _controller;
    private bool _isPressed;
    private void Start()
    {
        
        _isPressed = _controller.selectAction.action.ReadValue<bool>();
    }

    private void OnEnable()
    {
        _controller = GetComponent<ActionBasedController>();
        _controller.selectAction.action.performed += OnSelectActionPerformed;
    }
    
    private void OnDisable()
    {
        _controller.selectAction.action.performed -= OnSelectActionPerformed;
    }

    private void OnSelectActionPerformed(InputAction.CallbackContext obj)
    {
        Debug.Log("Select button is pressed");
    }
}
