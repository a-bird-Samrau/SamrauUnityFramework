using System;

namespace Input
{
    public class InputAxis : BaseInputAction
    {
        private Action<float> _action;

        private readonly float _scale;
        private readonly bool _isRaw;
        
        public InputAxis(string name, bool isRaw, Action<float> action, float scale, InputMode mode) : base(name, mode)
        {
            _isRaw = isRaw;
            _scale = scale;
            _action = action;
        }

        protected override void OnUpdate()
        {
            _action.Invoke((_isRaw ? UnityEngine.Input.GetAxisRaw(Name) : UnityEngine.Input.GetAxis(Name)) * _scale);
        }

        protected override void OnClear()
        {
            _action = null;
        }
    }
}