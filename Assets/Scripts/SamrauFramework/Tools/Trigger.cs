using SamrauFramework.Player;
using UnityEngine;
using Behaviour = SamrauFramework.Core.Behaviour;

namespace SamrauFramework.Tools
{
    [RequireComponent(typeof(BoxCollider))]
    public abstract class Trigger : Core.Behaviour
    {
        [Header("Default")]
        
        [SerializeField] private bool _onlyOnce;
        [SerializeField] private TriggerMode _mode;

        [Header("Gizmos")] 
        
        [SerializeField] private bool _overrideColor;
        [SerializeField] private Color _color;
        
        private BoxCollider _boxCollider;

        protected override void Start()
        {
            base.Start();

            _boxCollider = GetComponent<BoxCollider>();
            _boxCollider.isTrigger = true;
        }

        protected abstract void OnTrigger(PlayerCharacter playerCharacter);

        protected virtual void OnUnTrigger(PlayerCharacter playerCharacter)
        {
            
        }

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);

            var canCheckOnlyOnce = true;

            if (_mode != TriggerMode.OnlyEnter)
            {
                if (_mode != TriggerMode.Both)
                {
                    return;
                }

                canCheckOnlyOnce = false;
            }

            if (!other.TryGetComponent<PlayerCharacter>(out var playerCharacter))
            {
                return;
            }
            
            OnTrigger(playerCharacter);

            if (!canCheckOnlyOnce)
            {
                return;
            }
            
            if (_onlyOnce)
            {
                Destroy(gameObject);
            }
        }

        protected override void OnTriggerExit(Collider other)
        {
            base.OnTriggerExit(other);
            
            if (_mode != TriggerMode.OnlyExit)
            {
                if (_mode != TriggerMode.Both)
                {
                    return;
                }
            }

            if (!other.TryGetComponent<PlayerCharacter>(out var playerCharacter))
            {
                return;
            }
            
            OnUnTrigger(playerCharacter);

            if (_onlyOnce)
            {
                Destroy(gameObject);
            }
        }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();

            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = _overrideColor ? _color : Preferences.TriggerDefaultColor;
            
            Gizmos.DrawCube(Vector3.zero, Vector3.one);
        }
    }
}