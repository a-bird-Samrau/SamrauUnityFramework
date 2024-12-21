using System;
using Placeables;
using Player.FirstPersonCharacter;
using UnityEngine;
using Behaviour = Core.Behaviour;

namespace Player
{
    public class FirstPersonPointer : Behaviour,
        IPointer
    {
        public event Action<GameObject> Pointing;
        public event Action UnPointed;

        [SerializeField] private float _distance;

        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private LayerMask _layerMask;

        private IPointerTarget _currentPointerTarget;
        private GameObject _currentPointerTargetGameObject;

        protected override void Update()
        {
            base.Update();
            
            var ray = new Ray(_cameraTransform.position, _cameraTransform.forward);

            if (!Physics.Raycast(ray, out var hit, _distance, _layerMask))
            {
                ClearTarget();
    
                return;
            }

            if (!hit.collider.TryGetComponent<IPointerTarget>(out var pointerTarget))
            {
                ClearTarget();
    
                return;
            }

            if (_currentPointerTarget != null)
            {
                if (_currentPointerTarget == pointerTarget)
                {
                    if (_currentPointerTarget.IsEnable)
                    {
                        return;
                    }
                }
    
                ClearTarget();
            }

            if (!pointerTarget.IsEnable)
            {
                return;
            }

            var hitGameObject = hit.collider.gameObject;

            Pointing?.Invoke(hitGameObject);

            pointerTarget.Enter();

            _currentPointerTarget = pointerTarget;
            _currentPointerTargetGameObject = hitGameObject;
        }

        private void ClearTarget()
        {
            if (_currentPointerTarget == null)
            {
                return;
            }
            
            UnPointed?.Invoke();

            if (_currentPointerTargetGameObject != null)
            {
                _currentPointerTarget.Exit();
            }
                
            _currentPointerTarget = null;
            _currentPointerTargetGameObject = null;
        }
    }
}