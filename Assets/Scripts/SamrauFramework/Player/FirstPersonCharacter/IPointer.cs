using System;
using UnityEngine;

namespace SamrauFramework.Player.FirstPersonCharacter
{
    public interface IPointer
    {
        event Action<GameObject> Pointing;
        event Action UnPointed;
    }
}