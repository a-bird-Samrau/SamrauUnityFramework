using SamrauFramework.Player;
using UnityEngine;
using Behaviour = SamrauFramework.Core.Behaviour;

namespace SamrauFramework.Placeables
{
    public abstract class Interactive : Core.Behaviour, 
        IInteractable
    {
        [Header("Default")]
        
        [SerializeField] private bool _interactableInStart;

        protected override void Start()
        {
            base.Start();
            
            SetInteractable(_interactableInStart);
        }

        protected abstract bool OnInteract(PlayerCharacter playerCharacter);

        public bool Interact(PlayerCharacter playerCharacter)
        {
            return OnInteract(playerCharacter);
        }

        public void SetInteractable(bool value)
        {
            IsInteractable = value;
        }

        public bool IsInteractable { get; private set; }
    }
}