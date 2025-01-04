using System;
using SamrauFramework.Input;

namespace SamrauFramework.Player
{
    public interface IControllable
    {
        event Action<InputMode> InputModeChanged;
        event Action<bool> InputModeEnableChanged;

        void SetupInputComponent(IInputComponent inputComponent);

        InputMode CurrentInputMode { get; }
    }
}