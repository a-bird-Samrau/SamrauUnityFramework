using SamrauFramework.Player;
using SamrauFramework.Tools;
using UnityEngine;

namespace SamrauFramework.Placeables
{
    public abstract class Button : Interactive
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

        protected abstract bool OnPressed(PlayerCharacter playerCharacter);

        protected override bool OnInteract(PlayerCharacter playerCharacter)
        {
            _animation.Run(true);

            return OnPressed(playerCharacter);
        }
    }
}