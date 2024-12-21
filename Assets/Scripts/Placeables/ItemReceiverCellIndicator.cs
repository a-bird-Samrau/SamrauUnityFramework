using Player;
using TMPro;
using UnityEngine;
using Behaviour = Core.Behaviour;

namespace Placeables
{
    public class ItemReceiverCellIndicator : Behaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private TextMeshPro _countText;

        private ItemReceiver.IReadableCell _cell;
            
        public void Construct(ItemReceiver.IReadableCell cell)
        {
            cell.RequiredCountChanged += OnRequiredCountChanged;
            cell.Cleaned += OnCleaned;
                
            _cell = cell;

            _spriteRenderer.sprite = cell.AssignedItem.Sprite;
            _spriteRenderer.color = Color.yellow;

            _countText.text = cell.RequiredCount.ToString();
            _countText.color = Color.yellow;
        }

        private void OnRequiredCountChanged(int value)
        {
            _countText.text = value.ToString();
        }

        private void OnCleaned()
        {
            _spriteRenderer.color = Color.green;
            _countText.color = Color.green;
        }

        public void Clear()
        {
            _cell.RequiredCountChanged -= OnRequiredCountChanged;
            _cell.Cleaned -= OnCleaned;

            _cell = null;
        }
    }

}