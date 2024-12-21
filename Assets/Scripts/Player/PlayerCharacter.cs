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
        
        public event Action CriticalWeightReached;
        public event Action CriticalWeightRelieved;

        public event Action InventoryOpened;
        public event Action InventoryClosed;

        [SerializeField] private int _inventorySize;
        [SerializeField] private float _criticalWeight;
        
        [SerializeField] private PlayerInteraction _interaction;

        private bool _isInventoryMode;
        private bool _isCriticalWeight;

        private Inventory _inventory;

        private Transform _transform;
        
        private PlayerMotor _motor;
        private PlayerCamera _camera;
        private PlayerPointer _pointer;
        private PlayerBob _bob;
        
        public void Construct()
        {
            _inventory = new Inventory(_inventorySize);
            _inventory.TotalWeightChanged += OnInventoryTotalWeightChanged;

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

        private void OnInventoryTotalWeightChanged(float value)
        {
            var isCriticalWeight = value >= _criticalWeight;

            if (_isCriticalWeight == isCriticalWeight)
            {
                return;
            }
            
            if (isCriticalWeight)
            {
                CriticalWeightReached?.Invoke();
            }
            else
            {
                CriticalWeightRelieved?.Invoke();
            }

            _isCriticalWeight = isCriticalWeight;
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
            if (_isCriticalWeight)
            {
                return;
            }
            
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

        public void StartRunning()
        {
            _motor.StartRunning();
        }

        public void StopRunning()
        {
            _motor.StopRunning();
        }

        public void Interact()
        {
            _interaction.TryInteract(this);
        }

        public void ToggleInventory()
        {
            _isInventoryMode = !_isInventoryMode;
            SetInputMode(_isInventoryMode ? InputMode.UI : InputMode.Game);

            if (_isInventoryMode)
            {
                InventoryOpened?.Invoke();
            }
            else
            {
                InventoryClosed?.Invoke();
            }
        }

        public bool TakeItem(Item item, int count)
        {
            return _inventory.Place(item, count);
        }

        public bool RemoveItem(Item item, int count, bool force, out int remains)
        {
            return _inventory.Remove(item, count, force, out remains);
        }
        
        public bool DropItem(Item item, int count, bool force, out int remains)
        {
            if (!RemoveItem(item, count, force, out remains))
            {
                return false;
            }
            
            var number = count - remains;

            while (number > 0)
            {
                Instantiate(item.Prefab, _transform.position, Quaternion.identity);
                
                number -= 1;
            }

            return true;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _inventory.TotalWeightChanged -= OnInventoryTotalWeightChanged;
            
            _inventory.Clear();
            _inventory = null;
        }
        
        public InputMode CurrentInputMode { get; private set; }
        public float CriticalWeight => _criticalWeight;
        
        public IInventory Inventory => _inventory;
        public IPointer Pointer => _pointer;
    }
}