using SamrauFramework.Player;
using Behaviour = SamrauFramework.Core.Behaviour;

namespace SamrauFramework.UI
{
    public class HeadUpDisplay : Core.Behaviour
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