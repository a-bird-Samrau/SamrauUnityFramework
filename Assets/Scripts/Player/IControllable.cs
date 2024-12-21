using System;
using Input;
using UnityEngine;

namespace Player
{
    public interface IControllable
    {
        event Action<InputMode> InputModeChanged;
        event Action<bool> InputModeEnableChanged;

        void SetupInputComponent(IInputComponent inputComponent);

        InputMode CurrentInputMode { get; }
    }
}