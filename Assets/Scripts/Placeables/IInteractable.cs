using Player;

namespace Placeables
{
    public interface IInteractable
    {
        bool Interact(PlayerCharacter playerCharacter);
        bool IsInteractable { get; }
    }
}