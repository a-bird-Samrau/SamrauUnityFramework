using Player;

namespace Core
{
    public interface IInteraction
    {
        bool TakeItem(Item item, int count);
        bool RemoveItem(Item item, int count, bool force, out int remains);
    }
}