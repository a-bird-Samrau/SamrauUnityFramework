using UnityEngine;
using Behaviour = SamrauFramework.Core.Behaviour;

namespace SamrauFramework.Player.FirstPersonCharacter
{
    public class FirstPersonCamera : Core.Behaviour
    {
        [SerializeField] private Camera _camera;

        [SerializeField] private float _minVerticalAngle;
        [SerializeField] private float _maxVerticalAngle;

        private float _verticalAngle;
        private float _horizontalAngle;

        private Quaternion _cameraOriginRotation;
        private Quaternion _originRotation;

        private Transform _cameraTransform;
        private Transform _transform;

        public void Construct()
        {
            _cameraTransform = _camera.transform;
            _transform = transform;

            _cameraOriginRotation = _cameraTransform.localRotation;
            _originRotation = _transform.rotation;
        }

        protected override void Update()
        {
            base.Update();

            _cameraTransform.localRotation =
                _cameraOriginRotation * Quaternion.AngleAxis(_verticalAngle, Vector3.right);
            _transform.rotation = _originRotation * Quaternion.AngleAxis(_horizontalAngle, Vector3.up);
        }

        public void Turn(float value)
        {
            _horizontalAngle += value;
        }

        public void LookAt(float value)
        {
            _verticalAngle += value;
            _verticalAngle = Mathf.Clamp(_verticalAngle, _minVerticalAngle, _maxVerticalAngle);
        }
    }
}