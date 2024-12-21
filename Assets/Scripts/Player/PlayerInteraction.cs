using System;
using Core;
using Placeables;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class PlayerInteraction
    {
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private float _distance;
        [SerializeField] private LayerMask _layerMask;
        
        public bool TryInteract(IInteraction interaction)
        {
            var ray = new Ray(_cameraTransform.position, _cameraTransform.forward);

            if (!Physics.Raycast(ray, out var hit, _distance, _layerMask))
            {
                return false;
            }

            if (!hit.collider.TryGetComponent<IInteractable>(out var interactive))
            {
                return false;
            }

            return interactive.IsInteractable && interactive.Interact(interaction);
        }
    }
}