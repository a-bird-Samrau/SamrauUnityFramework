using UnityEngine;
using Behaviour = Core.Behaviour;

namespace Placeables
{
    public class PointerTarget : Behaviour, 
        IPointerTarget
    {
        [Header("Default")]
        
        [SerializeField] private bool _enableInStart;

        protected override void Start()
        {
            base.Start();

            IsEnable = _enableInStart;
        }

        protected virtual void OnEnter()
        {
            
        }

        protected virtual void OnExit()
        {
            
        }

        public void Enter()
        {
            OnEnter();
        }

        public void Exit()
        {
            OnExit();
        }

        public bool IsEnable { get; private set; }
    }
}