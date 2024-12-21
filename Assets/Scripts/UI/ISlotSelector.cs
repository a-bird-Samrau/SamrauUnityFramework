using UnityEngine.UI;

namespace UI
{
    public interface ISlotSelector
    {
        void Select(ISelectableSlot slot);
        void Deselect();
    }
}