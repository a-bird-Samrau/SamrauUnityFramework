using SamrauFramework.Engine;
using SamrauFramework.Player;
using UnityEngine;
using Behaviour = SamrauFramework.Core.Behaviour;

namespace SamrauFramework.UI
{
    public class UserInterface : Core.Behaviour
    {
        [SerializeField] private HeadUpDisplay _headUpDisplay;

        private PlayerCharacter _playerCharacter;
        private Game _game;

        public void Construct(Game game)
        {
            OnConstruct(game);
        }

        protected virtual void OnConstruct(Game game)
        {
            
        }

        protected virtual void OnPlayerCharacterSpawned(PlayerCharacter playerCharacter)
        {
            
        }

        protected virtual void OnPlayerCharacterDestroy()
        {
            
        }

        public void SendOnPlayerCharacterSpawned(PlayerCharacter playerCharacter)
        {
            _headUpDisplay.Construct(_playerCharacter);
            
            OnPlayerCharacterSpawned(playerCharacter);
        }

        public void SendOnPlayerCharacterDestroy()
        {
            _headUpDisplay.Clear();
            
            OnPlayerCharacterDestroy();
        }
    }
}