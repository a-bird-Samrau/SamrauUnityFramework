namespace SamrauFramework.Input
{
    public interface IInputBindableAction
    {
        void Update(InputMode mode);
        void Clear();
        
        string Name { get; }
    }
}