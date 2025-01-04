using SamrauFramework.Player;
using UnityEngine;

namespace SamrauFramework.Placeables
{
    public class TestInteractive : Interactive
    {
        protected override bool OnInteract(PlayerCharacter playerCharacter)
        {
            Debug.Log("Test Interactive!!!");
            
            return true;
        }
    }
}