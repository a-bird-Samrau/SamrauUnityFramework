using Core;
using Player;
using UnityEngine;

namespace Placeables
{
    public class Pickup : Interactive
    {
        [Header("Pickup")]
        
        [SerializeField] private Item _item;
        [SerializeField] private int _amount;
        
        protected override bool OnInteract(IInteraction interaction)
        {
            if (!interaction.TakeItem(_item, _amount))
            {
                return false;
            }
            
            Destroy(gameObject);

            return true;
        }
    }
}