namespace SamrauFramework.SaveLoad
{
    public interface IReadStorage
    {
        object Read<T>(string key, T valueDefault);
    }
}