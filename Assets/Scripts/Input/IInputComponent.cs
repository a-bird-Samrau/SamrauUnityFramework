using System;

namespace Input
{
    public interface IInputComponent
    {
        void BindButton(string name, InputButtonMode buttonMode, Action action, InputMode mode = InputMode.Game);
        void BindAxis(string name, bool isRaw, Action<float> action, float scale = 1f, InputMode mode = InputMode.Game);
    }
}