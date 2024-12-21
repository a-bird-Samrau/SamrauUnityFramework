using System;
using Settings;
using UnityEngine;
using Behaviour = Core.Behaviour;

namespace Player
{
    public class InputHandler : Behaviour
    {
        private IControllable _target;
        
        public void Control(IControllable target)
        {
            target.InputModeChanged += OnInputModeChanged;
            
            _target = target;
        }

        private void OnInputModeChanged(InputMode mode)
        {
            var isGameMode = mode == InputMode.Game;
            
            Cursor.visible = !isGameMode;
            Cursor.lockState = isGameMode ? CursorLockMode.Locked : CursorLockMode.None;
        }

        protected override void Update()
        {
            base.Update();

            if (_target == null)
            {
                return;
            }

            var currentInputMode = _target.CurrentInputMode;

            switch (currentInputMode)
            {
                case InputMode.Game:
                {
                    var horizontalSensitive = SettingsManager.GetSettings().HorizontalSensitive;
                    var verticalSensitive = SettingsManager.GetSettings().VerticalSensitive;
                    
                    _target.Move(new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")));

                    _target.Turn(Input.GetAxis("Mouse X") * horizontalSensitive);
                    _target.LookAt(-Input.GetAxis("Mouse Y") * verticalSensitive);

                    if (Input.GetButtonDown("Fire1"))
                    {
                        _target.Interact();
                    }

                    break;
                }
                case InputMode.UI:
                    //..
                    break;
                case InputMode.Both:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void StopControlling()
        {
            _target.InputModeChanged -= OnInputModeChanged;
            _target = null;
        }
    }
}