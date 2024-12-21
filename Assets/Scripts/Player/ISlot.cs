using System;
using Core;

namespace Player
{
    public interface ISlot
    {
        event Action<Item> Assigned;
        event Action<int> CountChanged;
        event Action Cleaned;

        float GetWeight();
        
        Item AssignedItem { get; }
        bool IsAssigned { get; }
        int Count { get; }
    }
}