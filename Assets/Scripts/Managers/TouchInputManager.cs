using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Gui;

namespace Managers
{
    public class TouchInputManager : InputManager
    {
        private LeanJoystick _joystick;

        override protected void Awake()
        {
            base.Awake();
            _joystick = FindObjectOfType<LeanJoystick>();
        }

        internal void Start()
        {
            _joystick.OnUp.AddListener(OnJoystickFingerUp);
        }

        override protected void OnDestroy()
        {
            _joystick.OnUp.RemoveListener(OnJoystickFingerUp);
            base.OnDestroy();
        }

        internal void Update()
        {
            var movementValues = _joystick.ScaledValue;
            if (movementValues != Vector2.zero)
            {
                base.Move?.Invoke(movementValues);
            }
            else
            {
                OnJoystickFingerUp();
            }
        }

        private void OnJoystickFingerUp()
        {
            base.JoystickReleaed?.Invoke();
        }
    }
}