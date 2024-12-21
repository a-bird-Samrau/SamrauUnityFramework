using Core;
using Player;

namespace Placeables
{
    public interface IInteractable
    {
        bool Interact(IInteraction interaction);
        bool IsInteractable { get; }
    }
}