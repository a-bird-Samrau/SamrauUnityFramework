using Core;
using Player;
using UnityEngine;

namespace Placeables
{
    public class TestInteractive : Interactive
    {
        protected override bool OnInteract(IInteraction interaction)
        {
            Debug.Log("Test Interactive!!!");
            
            return true;
        }
    }
}