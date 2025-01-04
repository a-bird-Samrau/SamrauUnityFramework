using SamrauFramework.Player;

namespace SamrauFramework.Placeables
{
    public interface IInteractable
    {
        bool Interact(PlayerCharacter playerCharacter);
        bool IsInteractable { get; }
    }
}