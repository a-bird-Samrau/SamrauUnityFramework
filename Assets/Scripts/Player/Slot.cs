using System;

namespace Player
{
    public class Slot : ISlot
    {
        public event Action<Item> Assigned;
        public event Action<int> CountChanged;
        public event Action Cleaned;
        
        public void Assign(Item item)
        {
            AssignedItem = item;
            IsAssigned = true;
            
            Assigned?.Invoke(item);
        }

        private void SetCount(int value)
        {
            Count = value;
            
            CountChanged?.Invoke(value);
        }

        public bool Add(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            SetCount(Count + amount);

            return true;
        }

        public bool Remove(int amount, bool force, out int remains)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            remains = amount;

            if (Count == 0)
            {
                return false;
            }

            var value = Count - amount;

            switch (value)
            {
                case 0:
                    remains = 0;
                    Clear();
                    
                    return true;
                case < 0 when !force:
                    return false;
                case < 0:
                    remains = Math.Abs(value);
                    Clear();
                    
                    return true;
            }

            remains = 0;
            SetCount(value);

            return true;
        }

        public void Clear()
        {
            AssignedItem = null;
            IsAssigned = false;
            
            SetCount(0);
            
            Cleaned?.Invoke();
        }

        public void ClearWithoutNotify()
        {
            AssignedItem = null;
            IsAssigned = false;
        }

        public float GetWeight()
        {
            return AssignedItem.Weight * Count;
        }

        public Item AssignedItem { get; private set; }
        public bool IsAssigned { get; private set; }
        public int Count { get; private set; }
    }
}