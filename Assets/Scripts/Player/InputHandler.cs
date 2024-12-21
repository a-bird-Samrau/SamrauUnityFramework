using System;
using Engine;
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

            if (Input.GetButtonDown("Inventory"))
            {
                _target.ToggleInventory();
            }
            
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

                    if (Input.GetButtonDown("Sprint"))
                    {
                        _target.StartRunning();
                    }

                    if (Input.GetButtonUp("Sprint"))
                    {
                        _target.StopRunning();
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