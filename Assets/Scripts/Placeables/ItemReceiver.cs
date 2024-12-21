using System;
using Core;
using Player;
using UnityEngine;

namespace Placeables
{
    public class ItemReceiver : Interactive
    {
        public interface IReadableCell
        {
            event Action<int> RequiredCountChanged;
            event Action Cleaned;
            
            Item AssignedItem { get; }
            int RequiredCount { get; }
        }
        
        [Serializable]
        public class Cell : IReadableCell
        {
            public event Action<int> RequiredCountChanged;
            public event Action Cleaned;

            [SerializeField] private Item _item;
            [SerializeField] private int _maxRequiredCount;

            public void Initialize()
            {
                SetRequiredCount(_maxRequiredCount);
            }
            
            public void SetRequiredCount(int value)
            {
                RequiredCount = value;
                RequiredCountChanged?.Invoke(value);
            }

            public void SendOnCleaned()
            {
                Cleaned?.Invoke();
            }

            public Item AssignedItem => _item;
            public int RequiredCount { get; private set; }
        }

        public event Action Received;

        [Header("Receiver")] 
        
        [SerializeField] private Cell[] _cells;

        protected override void Start()
        {
            base.Start();

            foreach (var cell in _cells)
            {
                cell.Initialize();
            }
        }

        protected override bool OnInteract(IInteraction interaction)
        {
            var isAllItemReceived = true;

            foreach (var cell in _cells)
            {
                if (cell.RequiredCount == 0)
                {
                    continue;
                }
                
                if (!interaction.RemoveItem(cell.AssignedItem, cell.RequiredCount, true, out var remains))
                {
                    isAllItemReceived = false;
                    
                    continue;
                }
                
                cell.SetRequiredCount(remains);

                if (remains == 0)
                {
                    cell.SendOnCleaned();
                    
                    continue;
                }

                isAllItemReceived = false;
            }

            if (!isAllItemReceived)
            {
                return false;
            }

            Received?.Invoke();
            SetInteractable(false);
                
            return true;
        }

        public IReadableCell[] GetCells()
        {
            var cells = new IReadableCell[_cells.Length];

            for (var i = 0; i < _cells.Length; i++)
            {
                cells[i] = _cells[i];
            }

            return cells;
        }
    }
}