using System.IO;
using UnityEngine;

namespace SaveLoad
{
    public class SaveLoadManager : 
        IReadableStorage, 
        IWritableStorage
    {
        private readonly SaveLoadFormatter _formatter;
        private readonly string _path;

        private SaveLoadData _data;

        public SaveLoadManager()
        {
            _path = Application.persistentDataPath + "/GameSave.dat";
            _formatter = new SaveLoadFormatter();

            Reset();
        }
        
        public object Read<T>(string key, T valueDefault)
        {
            return _data.GetValue(key, valueDefault);
        }

        public void Write<T>(string key, T value)
        {
            _data.AddValue(key, value);
        }

        public void Load()
        {
            if (!File.Exists(_path))
            {
                Reset();
                
                Save();
            }

            var file = File.Open(_path, FileMode.Open);
            _data = _formatter.Deserialize(file);
            
            file.Close();
        }

        public void Save()
        {
            var file = File.Create(_path);
            _formatter.Serialize(file, _data);
            
            file.Close();
        }

        public void Reset()
        {
            _data = new SaveLoadData();
        }
    }
}