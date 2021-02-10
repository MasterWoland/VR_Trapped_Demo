using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using System;

namespace Pixelplacement
{
    public class InputManagerPP : Singleton<InputManagerPP>
    {
        //Events:
        public event Action OnLeftPressed;
        public event Action OnRightPressed;
        public event Action OnUpPressed;
        public event Action OnDownPressed;

        //Private Variables:
        float _previousGamepadHorizontal;
        float _previousGamepadVertical;

        //Loops:
        void Update ()
        {
            bool gamepadInputReceived = false;
            float horizontal = Input.GetAxis ("Horizontal");
            float vertical = Input.GetAxis ("Vertical");

            //gamepad:
            if (_previousGamepadVertical < 1 && vertical == 1 && OnUpPressed != null)
            {
                OnUpPressed (); 
                gamepadInputReceived = true;
            }

            if (_previousGamepadVertical > -1 && vertical == -1 && OnDownPressed != null)
            {
                OnDownPressed (); 
                gamepadInputReceived = true;
            }

            if (_previousGamepadHorizontal < 1 && horizontal == 1 && OnRightPressed != null)
            {
                OnRightPressed (); 
                gamepadInputReceived = true;
            }

            if (_previousGamepadHorizontal > -1 && horizontal == -1 && OnLeftPressed != null)
            {
                OnLeftPressed (); 
                gamepadInputReceived = true;
            }

            //cache gamepad axis:
            _previousGamepadVertical = vertical;
            _previousGamepadHorizontal = horizontal;

            //do not process keyboard input if a gamepad was used to avoid double events:
            if (gamepadInputReceived) return;
    
            //keyboard:
            if (Input.GetKeyDown (KeyCode.LeftArrow) && OnLeftPressed != null)
            {
                OnLeftPressed ();
            }

            if (Input.GetKeyDown (KeyCode.RightArrow) && OnRightPressed != null)
            {
                OnRightPressed ();
            }

            if (Input.GetKeyDown (KeyCode.UpArrow) && OnUpPressed != null)
            {
                OnUpPressed ();
            }

            if (Input.GetKeyDown (KeyCode.DownArrow) && OnDownPressed != null)
            {
                OnDownPressed ();
            }
        }
    }
}