namespace SaveLoad
{
    public interface IReadableStorage
    {
        object Read<T>(string key, T valueDefault);
    }
}