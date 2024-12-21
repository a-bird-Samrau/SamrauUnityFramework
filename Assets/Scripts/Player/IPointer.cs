using System;
using UnityEngine;

namespace Player
{
    public interface IPointer
    {
        event Action<GameObject> Pointing;
        event Action UnPointed;
    }
}