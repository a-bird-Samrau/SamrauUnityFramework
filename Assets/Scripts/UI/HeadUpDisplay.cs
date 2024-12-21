using Player;
using UnityEngine;
using Behaviour = Core.Behaviour;

namespace UI
{
    public class HeadUpDisplay : Behaviour
    {
        public void Construct(PlayerCharacter playerCharacter)
        {
            OnConstruct(playerCharacter);
        }

        protected virtual void OnConstruct(PlayerCharacter playerCharacter)
        {
            
        }

        protected virtual void OnClear()
        {
            
        }

        public void Clear()
        {
            OnClear();
        }
    }
}