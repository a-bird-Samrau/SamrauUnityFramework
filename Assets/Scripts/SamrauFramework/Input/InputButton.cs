using System;

namespace SamrauFramework.Input
{
    public class InputButton : BaseInputAction
    {
        private Action _action;
        
        private readonly InputButtonMode _buttonMode;

        public InputButton(string name, InputButtonMode buttonMode, Action action, InputMode mode) :
            base(name, mode)
        {
            _buttonMode = buttonMode;
            _action = action;
        }

        protected override void OnUpdate()
        {
            if (UnityEngine.Input.GetButtonDown(Name))
            {
                if (_buttonMode == InputButtonMode.Pressed)
                {
                    _action.Invoke();
                }
            }

            if (UnityEngine.Input.GetButtonUp(Name))
            {
                if (_buttonMode == InputButtonMode.Released)
                {
                    _action.Invoke();
                }
            }
        }

        protected override void OnClear()
        {
            _action = null;
        }
    }
}