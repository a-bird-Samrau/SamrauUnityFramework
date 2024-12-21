using System;
using UnityEngine;

namespace Player
{
    public interface IControllable
    {
        event Action<InputMode> InputModeChanged;
        
        void Move(Vector3 direction);
        
        void LookAt(float value);
        void Turn(float value);

        void Interact();

        InputMode CurrentInputMode { get; }
    }
}