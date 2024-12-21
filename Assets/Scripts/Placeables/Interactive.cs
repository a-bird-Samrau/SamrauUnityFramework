using Core;
using Player;
using UnityEngine;
using Behaviour = Core.Behaviour;

namespace Placeables
{
    public abstract class Interactive : Behaviour, 
        IInteractable
    {
        [Header("Default")]
        [SerializeField] private bool _interactableInStart;

        protected override void Start()
        {
            base.Start();
            
            SetInteractable(_interactableInStart);
        }

        protected abstract bool OnInteract(IInteraction interaction);

        public bool Interact(IInteraction interaction)
        {
            return OnInteract(interaction);
        }

        public void SetInteractable(bool value)
        {
            IsInteractable = value;
        }

        public bool IsInteractable { get; private set; }
    }
}