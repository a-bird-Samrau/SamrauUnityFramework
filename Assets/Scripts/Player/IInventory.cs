using System;

namespace Player
{
    public interface IInventory
    {
        event Action<float> TotalWeightChanged;

        ISlot[] Get();

        float TotalWeight { get; }
    }
}