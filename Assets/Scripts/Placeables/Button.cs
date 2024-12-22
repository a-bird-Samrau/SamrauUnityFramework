using Player;
using Tools;
using UnityEngine;

namespace Placeables
{
    public class Button : Interactive
    {
        [Header("Button")]
        
        [SerializeField] private float _duration;
        
        [SerializeField] private Transform _transform;
        
        [SerializeField] private Vector3 _axis;
        [SerializeField] private float _offset;
        
        private ButtonAnimation _animation;

        protected override void Start()
        {
            base.Start();

            _animation = new ButtonAnimation(_duration, _offset, _axis, _transform);
        }

        protected override bool OnInteract(PlayerCharacter playerCharacter)
        {
            _animation.Run(true);
            
            return true;
        }
    }
}