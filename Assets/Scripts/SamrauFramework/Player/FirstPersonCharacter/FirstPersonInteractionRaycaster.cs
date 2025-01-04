using System;
using SamrauFramework.Placeables;
using UnityEngine;

namespace SamrauFramework.Player.FirstPersonCharacter
{
    [Serializable]
    public class FirstPersonInteractionRaycaster
    {
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private float _distance;
        [SerializeField] private LayerMask _layerMask;
        
        public bool RaycastToInteract(PlayerCharacter playerCharacter)
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

            return interactive.IsInteractable && interactive.Interact(playerCharacter);
        }
    }
}