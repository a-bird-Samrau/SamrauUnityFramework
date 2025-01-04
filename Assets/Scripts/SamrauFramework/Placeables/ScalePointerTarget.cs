using UnityEngine;

namespace SamrauFramework.Placeables
{
    public class ScalePointerTarget : PointerTarget
    {
        [Header("Scale")]
        
        [SerializeField] private float _amount;
        [SerializeField] private Transform _target;

        protected override void OnEnter()
        {
            base.OnEnter();
            
            _target.Scale(_amount);
        }

        protected override void OnExit()
        {
            base.OnExit();
            
            _target.Scale(1f);
        }
    }
}