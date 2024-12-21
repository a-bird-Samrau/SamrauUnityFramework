using System;
using UnityEngine;

namespace Player.FirstPersonCharacter
{
    public interface IPointer
    {
        event Action<GameObject> Pointing;
        event Action UnPointed;
    }
}