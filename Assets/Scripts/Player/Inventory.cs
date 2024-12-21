using System;
using UnityEngine;

namespace Player
{
    public class Inventory : IInventory
    {
        public event Action<float> TotalWeightChanged;
        
        private int _totalWeight;
        
        private readonly Slot[] _slots;
        private readonly int _size;

        public Inventory(int size)
        {
            _slots = new Slot[size];

            for (var i = 0; i < size; i++)
            {
                _slots[i] = new Slot();
            }

            _size = size;
        }

        private void SetTotalWeight(float value)
        {
            TotalWeight = value;
            TotalWeightChanged?.Invoke(value);
        }

        private void AddTotalWeight(Item item, int count)
        {
            SetTotalWeight(TotalWeight + item.Weight * count);
        }

        private void RemoveTotalWeight(Item item, int count)
        {
            if (count == 0)
            {
                return;
            }

            SetTotalWeight(TotalWeight - item.Weight * count);
        }

        public bool Place(Item item, int count)
        {
            Slot firstFreeSlot = null;

            foreach (var slot in _slots)
            {
                if (!slot.IsAssigned)
                {
                    firstFreeSlot ??= slot;
                    
                    continue;
                }

                if (slot.AssignedItem != item)
                {
                    continue;
                }

                if (!slot.Add(count))
                {
                    return false;
                }
                
                AddTotalWeight(item, count);

                return true;
            }

            if (firstFreeSlot == null)
            {
                return false;
            }
            
            firstFreeSlot.Assign(item);
            firstFreeSlot.Add(count);
            
            AddTotalWeight(item, count);

            return true;
        }

        public bool Remove(Item item, int count, bool force, out int remains)
        {
            remains = count;
            
            foreach (var slot in _slots)
            {
                if (!slot.IsAssigned)
                {
                    continue;
                }

                if (slot.AssignedItem != item)
                {
                    continue;
                }

                if (!slot.Remove(count, force, out remains))
                {
                    return false;
                }
                
                RemoveTotalWeight(item, count - remains);
                    
                return true;
            }

            return false;
        }

        public void Clear()
        {
            for (var i = 0; i < _size; i++)
            {
                _slots[i].ClearWithoutNotify();
                _slots[i] = null;
            }
        }

        public ISlot[] Get()
        {
            var slots = new ISlot[_size];

            for (var i = 0; i < _size; i++)
            {
                slots[i] = _slots[i];
            }

            return slots;
        }

        public float TotalWeight { get; private set; }
    }
}