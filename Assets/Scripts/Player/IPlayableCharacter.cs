using System;

namespace Player
{
    public interface IPlayableCharacter
    {
        event Action CriticalWeightReached;
        event Action CriticalWeightRelieved;
        
        event Action InventoryOpened;
        event Action InventoryClosed;

        bool DropItem(Item item, int count, bool force, out int remains);

        float CriticalWeight { get; }
        
        IInventory Inventory { get; }
        IPointer Pointer { get; }
    }
}