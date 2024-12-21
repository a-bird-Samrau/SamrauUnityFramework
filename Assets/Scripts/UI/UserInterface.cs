using Engine;
using Player;
using UnityEngine;
using Behaviour = Core.Behaviour;

namespace UI
{
    public class UserInterface : Behaviour
    {
        [SerializeField] private HeadUpDisplay _headUpDisplay;

        private IPlayableCharacter _playableCharacter;
        private Game _game;

        public void Construct(Game game)
        {
            _game = game;
        }

        public void SendOnPlayerCharacterSpawned(IPlayableCharacter playableCharacter)
        {
            _headUpDisplay.Construct(playableCharacter);
            
            _playableCharacter = playableCharacter;
        }

        public void SendOnPlayerCharacterDestroyed()
        {
            _headUpDisplay.Clear();
            
            _playableCharacter = null;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _game = null;
        }
    }
}