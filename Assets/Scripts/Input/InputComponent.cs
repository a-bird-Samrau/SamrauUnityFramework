using System;
using System.Collections.Generic;

namespace Input
{
    public class InputComponent : IInputComponent
    {
        private readonly List<IInputBindableAction> _actions;

        public InputComponent()
        {
            _actions = new List<IInputBindableAction>();
        }

        public void BindButton(string name, InputButtonMode buttonMode, Action action, InputMode mode = InputMode.Game)
        {
            var instance = new InputButton(name, buttonMode, action, mode);
            
            _actions.Add(instance);
        }

        public void BindAxis(string name, bool isRaw, Action<float> action, float scale = 1f, InputMode mode = InputMode.Game)
        {
            var instance = new InputAxis(name, isRaw, action, scale, mode);
            
            _actions.Add(instance);
        }
        
        public void Update(InputMode mode)
        {
            foreach (var action in _actions)
            {
                action.Update(mode);
            }
        }

        public void Clear()
        {
            foreach (var action in _actions)
            {
                action.Clear();
            }
            
            _actions.Clear();
        }
    }
}