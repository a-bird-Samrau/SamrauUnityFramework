using Core;
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