using UnityEngine;

namespace Tools
{
    public class ButtonAnimation : CoroutineOperation
    {
        private Vector3 _position;
        
        private readonly float _offset;
        private readonly Vector3 _axis;
        
        private readonly Transform _transform;
        
        public ButtonAnimation(float duration, float offset, Vector3 axis, Transform transform) : base(duration, TimeType.Default)
        {
            _offset = offset;
            _axis = axis;
            
            _transform = transform;
        }
        
        protected override void OnValueChanged(float value)
        {
            var offset = Mathf.PingPong(value * 2f, 1f) * _offset;

            _position = _axis * offset;
            _transform.localPosition = _position;
        }
    }
}