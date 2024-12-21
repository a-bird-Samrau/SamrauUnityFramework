using System;
using UnityEngine;
using Behaviour = Core.Behaviour;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMotor : Behaviour
    {
        [SerializeField] private float _gravity;
        
        [SerializeField] private float _walkSpeed;
        [SerializeField] private float _runningSpeedMultiplier;

        [SerializeField] private float _slopeRayLength;
        [SerializeField] private LayerMask _slopeRayLayerMask;
        
        [SerializeField] private float _slopeForce;

        private Vector3 _slopeNormal;

        private bool _isRunning;

        private float _currentSpeed;

        private Vector3 _moveDirection;
        private Vector3 _inputDirection;

        private Transform _transform;
        
        private CharacterController _characterController;

        public void Construct()
        {
            _transform = transform;
            _characterController = GetComponent<CharacterController>();
        }

        private void UpdateSpeed()
        {
            _currentSpeed = _walkSpeed;

            if (_isRunning)
            {
                _currentSpeed *= _runningSpeedMultiplier;
            }
        }

        private void UpdateMovement()
        {
            var deltaTime = Time.deltaTime;
            
            if (_characterController.isGrounded)
            {
                _moveDirection = _transform.TransformDirection(_inputDirection);
                _moveDirection *= _currentSpeed;
            }

            _moveDirection.y -= _gravity * deltaTime;
            _characterController.Move(_moveDirection * deltaTime);
        }

        private bool CheckOnSlope(out Vector3 normal)
        {
            normal = Vector3.up;
            
            if (!Physics.Raycast(new Ray(_transform.position, Vector3.down), out var hit,
                    _characterController.height / 2 * _slopeRayLength, _slopeRayLayerMask))
            {
                return false;
            }

            normal = hit.normal;

            return hit.normal != Vector3.up;
        }

        private void UpdateSlopeMovement()
        {
            if (!CheckOnSlope(out var normal))
            {
                if (_slopeNormal == Vector3.zero)
                {
                    return;
                }

                normal = _slopeNormal;
            }

            var deltaTime = Time.deltaTime;
            var isSliding = Vector3.Angle(normal, Vector3.up) > _characterController.slopeLimit;

            if (isSliding)
            {
                var cross = Vector3.Cross(normal, Vector3.up);
                var slideDirection = Vector3.Cross(cross, normal);

                _characterController.Move(-slideDirection * (_slopeForce * deltaTime));
            }
            else
            {
                if (_inputDirection.magnitude == 0f)
                {
                    return;
                }
                
                _characterController.Move(Vector3.down * _characterController.height / 2f * (_slopeForce * deltaTime));
            }
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.controller.collisionFlags != CollisionFlags.Below)
            {
                return;
            }

            var normal = hit.normal;

            if (normal == Vector3.up)
            {
                return;
            }

            _slopeNormal = normal;
        }

        protected override void Update()
        {
            base.Update();
            
            UpdateSpeed();
            UpdateMovement();
            UpdateSlopeMovement();

            _inputDirection = Vector3.zero;
        }

        public void Move(Vector3 direction)
        {
            if (direction != Vector3.zero)
            {
                LastInputDirection = direction;
            }
            
            _inputDirection = direction;
        }

        public void StartRunning()
        {
            _isRunning = true;
        }

        public void StopRunning()
        {
            _isRunning = false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawLine(Vector3.zero, Vector3.down * _slopeRayLength);
        }

        public Vector3 LastInputDirection { get; private set; }
        public Vector3 Velocity => _moveDirection;
    }
}