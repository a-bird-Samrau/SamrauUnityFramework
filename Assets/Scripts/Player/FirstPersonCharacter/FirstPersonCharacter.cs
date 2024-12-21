using Input;
using Settings;
using UnityEngine;

namespace Player.FirstPersonCharacter
{
    [RequireComponent(typeof(FirstPersonMotor))]
    [RequireComponent(typeof(FirstPersonCamera))]
    [RequireComponent(typeof(FirstPersonPointer))]
    [RequireComponent(typeof(FirstPersonBob))]
    public class FirstPersonCharacter : PlayerCharacter
    {
        [SerializeField] private FirstPersonInteractionRaycaster _interactionRaycaster;

        private FirstPersonMotor _motor;
        private FirstPersonCamera _camera;
        private FirstPersonPointer _pointer;
        private FirstPersonBob _bob;

        protected override void OnConstruct()
        {
            base.OnConstruct();
            
            _motor = GetComponent<FirstPersonMotor>();
            _motor.Construct();
            
            _camera = GetComponent<FirstPersonCamera>();
            _camera.Construct();

            _pointer = GetComponent<FirstPersonPointer>();

            _bob = GetComponent<FirstPersonBob>();
        }

        protected override void OnBeginPlay()
        {
            base.OnBeginPlay();
            
            SetInputModeEnable(true);
            SetInputMode(InputMode.Game);
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

        private void MoveForward(float value)
        {
            _motor.MoveForward(value);
        }

        private void MoveRight(float value)
        {
            _motor.MoveRight(value);
        }

        private void LookAt(float value)
        {
            _camera.LookAt(value * SettingsManager.GetSettings().VerticalSensitive);
        }

        private void Turn(float value)
        {
            _camera.Turn(value * SettingsManager.GetSettings().HorizontalSensitive);
        }

        private void StartRunning()
        {
            _motor.StartRunning();
        }

        private void StopRunning()
        {
            _motor.StopRunning();
        }

        private void Interact()
        {
            _interactionRaycaster.RaycastToInteract(this);
        }

        public override void SetupInputComponent(IInputComponent inputComponent)
        {
            inputComponent.BindAxis("Vertical", false, MoveForward);
            inputComponent.BindAxis("Horizontal", false, MoveRight);
            
            inputComponent.BindAxis("Mouse X", false, Turn);
            inputComponent.BindAxis("Mouse Y", false, LookAt, -1f);
            
            inputComponent.BindButton("Sprint", InputButtonMode.Pressed, StartRunning);
            inputComponent.BindButton("Sprint", InputButtonMode.Released, StopRunning);
            
            inputComponent.BindButton("Fire1", InputButtonMode.Pressed, Interact);
        }
        
        public IPointer Pointer => _pointer;
    }
}