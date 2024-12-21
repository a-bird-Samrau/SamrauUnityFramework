namespace SaveLoad
{
    public interface IWritableStorage
    {
        void Write<T>(string key, T value);
    }
}