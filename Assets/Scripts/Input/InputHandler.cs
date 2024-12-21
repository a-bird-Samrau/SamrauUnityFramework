using Player;
using UnityEngine;
using Behaviour = Core.Behaviour;

namespace Input
{
    public class InputHandler : Behaviour
    {
        private bool _isEnable;
        private InputMode _currentMode;
        
        private IControllable _controllable;
        private InputComponent _currentComponent;

        public void Control(IControllable controllable)
        {
            var newComponent = new InputComponent();
            
            controllable.InputModeChanged += OnInputModeChanged;
            controllable.InputModeEnableChanged += OnInputModeEnableChanged;
            
            controllable.SetupInputComponent(newComponent);

            _currentComponent = newComponent;
            _controllable = controllable;
        }

        private void OnInputModeEnableChanged(bool value)
        {
            _isEnable = value;
        }

        private void OnInputModeChanged(InputMode mode)
        {
            var isGameMode = mode == InputMode.Game;

            Cursor.visible = !isGameMode;
            Cursor.lockState = isGameMode ? CursorLockMode.Locked : CursorLockMode.None;

            _currentMode = mode;
        }

        protected override void Update()
        {
            base.Update();

            if (_controllable == null)
            {
                return;
            }

            if (!_isEnable)
            {
                return;
            }
            
            _currentComponent.Update(_currentMode);
        }

        public void ClearControl()
        {
            _currentComponent.Clear();
            
            _currentComponent = null;

            _controllable.InputModeChanged -= OnInputModeChanged;
            _controllable.InputModeEnableChanged -= OnInputModeEnableChanged;
            
            _controllable = null;
        }
    }
}