using Core;

namespace Placeables
{
    public interface IInteractable
    {
        bool Interact(IInteraction interaction);
        bool IsInteractable { get; }
    }
}