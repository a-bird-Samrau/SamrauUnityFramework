using System;
using Core;
using UnityEngine;
using Behaviour = Core.Behaviour;

namespace Player
{
    [RequireComponent(typeof(PlayerMotor))]
    [RequireComponent(typeof(PlayerCamera))]
    [RequireComponent(typeof(PlayerPointer))]
    [RequireComponent(typeof(PlayerBob))]
    public class PlayerCharacter : Behaviour,
        IControllable,
        IInteraction,
        IPlayableCharacter
    {
        public event Action<InputMode> InputModeChanged;

        [SerializeField] private PlayerInteraction _interaction;

        private Transform _transform;
        
        private PlayerMotor _motor;
        private PlayerCamera _camera;
        private PlayerPointer _pointer;
        private PlayerBob _bob;
        
        public void Construct()
        {
            _transform = transform;

            _motor = GetComponent<PlayerMotor>();
            _motor.Construct();
            
            _camera = GetComponent<PlayerCamera>();
            _camera.Construct();

            _pointer = GetComponent<PlayerPointer>();

            _bob = GetComponent<PlayerBob>();
        }

        public void BeginPlay()
        {
            SetInputMode(InputMode.Game);
        }
        
        private void SetInputMode(InputMode mode)
        {
            CurrentInputMode = mode;
            InputModeChanged?.Invoke(mode);
        }

        protected override void Update()
        {
            base.Update();

            var velocity = _motor.Velocity;
            velocity.y = 0f;

            var velocityMagnitude = velocity.magnitude;
            var lastInputDirection = _motor.LastInputDirection;

            if (velocityMagnitude > 0.01f)
            {
                _bob.Bob(lastInputDirection * velocityMagnitude);
            }
            else
            {
                if (_bob.IsBobing)
                {
                    _bob.StopBobing();
                }
            }
        }

        public void Move(Vector3 direction)
        {
            _motor.Move(direction);
        }

        public void LookAt(float value)
        {
            _camera.LookAt(value);
        }

        public void Turn(float value)
        {
            _camera.Turn(value);
        }

        public void Interact()
        {
            _interaction.TryInteract(this);
        }

        public InputMode CurrentInputMode { get; private set; }
        public IPointer Pointer => _pointer;
    }
}