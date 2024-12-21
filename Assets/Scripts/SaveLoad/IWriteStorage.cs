namespace SaveLoad
{
    public interface IWriteStorage
    {
        void Write<T>(string key, T value);
    }
}