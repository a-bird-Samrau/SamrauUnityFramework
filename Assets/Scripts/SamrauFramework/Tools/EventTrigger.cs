using SamrauFramework.Player;
using UnityEngine;
using UnityEngine.Events;

namespace SamrauFramework.Tools
{
    public class EventTrigger : Trigger
    {
        [Header("Event")] 
        
        [SerializeField] private UnityEvent<PlayerCharacter> _onTriggerEvent;
        [SerializeField] private UnityEvent<PlayerCharacter> _onUnTriggerEvent;
        
        protected override void OnTrigger(PlayerCharacter playerCharacter)
        {
            _onTriggerEvent.Invoke(playerCharacter);
        }

        protected override void OnUnTrigger(PlayerCharacter playerCharacter)
        {
            base.OnUnTrigger(playerCharacter);
            
            _onUnTriggerEvent.Invoke(playerCharacter);
        }
    }
}