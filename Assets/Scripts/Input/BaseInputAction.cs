namespace Input
{
    public abstract class BaseInputAction : IInputBindableAction
    {
        private readonly InputMode _mode;
        
        protected BaseInputAction(string name, InputMode mode)
        {
            Name = name;
            _mode = mode;
        }

        protected abstract void OnUpdate();
        protected abstract void OnClear();

        public void Update(InputMode mode)
        {
            if (mode != _mode)
            {
                if (_mode != InputMode.Both)
                {
                    return;
                }
            }

            OnUpdate();
        }

        public void Clear()
        {
            OnClear();
        }

        public string Name { get; }
    }
}