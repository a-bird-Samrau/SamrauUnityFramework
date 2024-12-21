using Player;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Behaviour = Core.Behaviour;

namespace UI
{
    public class SlotWidget : Behaviour,
        ISelectableSlot,
        IPointerEnterHandler,
        IPointerExitHandler,
        IPointerDownHandler
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _countText;

        [SerializeField] private GameObject _selectedIndicator;
        
        private ISlotSelector _selector;
        
        public void Construct(ISlot target, ISlotSelector selector)
        {
            target.Assigned += OnAssigned;
            target.CountChanged += OnCountChanged;
            target.Cleaned += OnCleaned;

            Target = target;
            _selector = selector;
        }
        
        private void OnAssigned(Item item)
        {
            _image.SetSprite(item.Sprite, Color.white);
        }
        
        private void OnCountChanged(int value)
        {
            _countText.text = value.ToString();
        }

        private void OnCleaned()
        {
            _image.Clear();
            _countText.text = "";
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }

            if (!Target.IsAssigned)
            {
                return;
            }
            
            _selector.Select(this);
            _selectedIndicator.Activate();
        }
        
        public void SendOnDeselected()
        {
            _selectedIndicator.Deactivate();
        }

        public void Clear()
        {
            Target.Assigned += OnAssigned;
            Target.CountChanged += OnCountChanged;
            Target.Cleaned += OnCleaned;
            
            Target = null;
            _selector = null;
        }

        public ISlot Target { get; private set; }
    }
}