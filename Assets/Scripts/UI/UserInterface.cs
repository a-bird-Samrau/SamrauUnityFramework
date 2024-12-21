using Engine;
using Player;
using UnityEngine;
using Behaviour = Core.Behaviour;

namespace UI
{
    public class UserInterface : Behaviour
    {
        [SerializeField] private HeadUpDisplay _headUpDisplay;
        [SerializeField] private InventoryWidget _inventoryWidget;
        
        private IPlayableCharacter _playableCharacter;
        private Game _game;

        public void Construct(Game game)
        {
            _game = game;
        }

        private void OnInventoryOpened()
        {
            _inventoryWidget.Show();
        }
        
        private void OnInventoryClosed()
        {
            _inventoryWidget.Hide();
        }

        public void SendOnPlayerCharacterSpawned(IPlayableCharacter playableCharacter)
        {
            _headUpDisplay.Construct(playableCharacter);
            
            _inventoryWidget.Construct(playableCharacter);
            _inventoryWidget.Hide();
            
            playableCharacter.InventoryOpened += OnInventoryOpened;
            playableCharacter.InventoryClosed += OnInventoryClosed;

            _playableCharacter = playableCharacter;
        }

        public void SendOnPlayerCharacterDestroyed()
        {
            _headUpDisplay.Clear();
            _inventoryWidget.Clear();

            _playableCharacter.InventoryOpened -= OnInventoryOpened;
            _playableCharacter.InventoryClosed -= OnInventoryClosed;

            _playableCharacter = null;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _game = null;
        }
    }
}