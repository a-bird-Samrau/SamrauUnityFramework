using Player;

namespace UI
{
    public interface ISelectableSlot
    {
        void SendOnDeselected();
        
        ISlot Target { get; }
    }
}