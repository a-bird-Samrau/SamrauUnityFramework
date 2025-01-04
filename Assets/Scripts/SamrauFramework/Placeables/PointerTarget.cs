using UnityEngine;
using Behaviour = SamrauFramework.Core.Behaviour;

namespace SamrauFramework.Placeables
{
    public class PointerTarget : Core.Behaviour, 
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