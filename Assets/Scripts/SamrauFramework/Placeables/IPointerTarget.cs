namespace SamrauFramework.Placeables
{
    public interface IPointerTarget
    {
        void Enter();
        void Exit();
        
        bool IsEnable { get; }
    }
}