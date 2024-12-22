using Player;
using UnityEngine;
using UnityEngine.Events;

namespace Placeables
{
    public class EventButton : Button
    {
        [Header("Event")] 
        
        [SerializeField] private UnityEvent<PlayerCharacter> _onPressedEvent;
        
        protected override bool OnPressed(PlayerCharacter playerCharacter)
        {
            _onPressedEvent.Invoke(playerCharacter);
            
            return true;
        }
    }
}