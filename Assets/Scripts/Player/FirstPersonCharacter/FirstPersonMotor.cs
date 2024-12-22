using UnityEngine;
using Behaviour = Core.Behaviour;

namespace Player.FirstPersonCharacter
{
    [RequireComponent(typeof(CharacterController))]
    public class FirstPersonMotor : Behaviour
    {
        [SerializeField] private float _gravity;
        
        [SerializeField] private float _walkSpeed;
        [SerializeField] private float _runningSpeedMultiplier;

        [SerializeField] private float _jumpForce;
        [SerializeField] private float _jumpMaxHoldTime;

        [SerializeField] private bool _canJump;

        private float _jumpHoldTime;

        private bool _isJumping;
        private bool _inputToJumping;

        private bool _isRunning;

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
            CurrentSpeed = _walkSpeed;

            if (_isRunning)
            {
                CurrentSpeed *= _runningSpeedMultiplier;
            }
        }

        protected void CalculateGroundMovement()
        {
            _moveDirection = _transform.TransformDirection(_inputDirection);
            _moveDirection *= CurrentSpeed;
        }

        protected void CalculateAirMovement()
        {
            var direction = _transform.TransformDirection(_inputDirection);

            _moveDirection.x = direction.x * CurrentSpeed;
            _moveDirection.y -= _gravity * Time.deltaTime;
            _moveDirection.z = direction.z * CurrentSpeed;
        }

        private void UpdateMovement()
        {
            var deltaTime = Time.deltaTime;
            
            if (_characterController.isGrounded)
            {
                CalculateGroundMovement();

                if (_isJumping && !_inputToJumping)
                {
                    _isJumping = false;
                }
            }
            else
            {
                CalculateAirMovement();
            }
            
            if (_inputToJumping)
            {
                _moveDirection.y = _jumpForce;

                if (_jumpHoldTime < _jumpMaxHoldTime)
                {
                    _jumpHoldTime += deltaTime;
                }
                else
                {
                    StopJumping();
                }
            }
            
            _characterController.Move(_moveDirection * deltaTime);
        }

        protected override void Update()
        {
            base.Update();
            
            UpdateSpeed();
            UpdateMovement();

            if (_inputDirection != Vector3.zero)
            {
                LastInputDirection = _inputDirection;
            }
            
            _inputDirection = Vector3.zero;
        }

        public void MoveForward(float value)
        {
            _inputDirection.z = value;
        }

        public void MoveRight(float value)
        {
            _inputDirection.x = value;
        }

        public void StartRunning()
        {
            _isRunning = true;
        }

        public void StopRunning()
        {
            _isRunning = false;
        }

        public void Jump()
        {
            if (!_canJump)
            {
                return;
            }
            
            if (_isJumping)
            {
                return;
            }
            
            _inputToJumping = true;
            _isJumping = true;
        }

        public void StopJumping()
        {
            _inputToJumping = false;
            _jumpHoldTime = 0f;
        }

        public Vector3 LastInputDirection { get; private set; }
        public Vector3 Velocity => _moveDirection;
        public float CurrentSpeed { get; private set; }
    }
}