using System.IO;
using UnityEngine;

namespace Settings
{
    public class SettingsFormatter
    {
        private readonly string _path;

        public SettingsFormatter(string path)
        {
            _path = path;
        }
        
        public bool Read(out global::Settings.Settings settings)
        {
            settings = null;
            
            if (!File.Exists(_path))
            {
                return false;
            }

            var content = File.ReadAllText(_path);

            if (content == string.Empty)
            {
                return false;
            }

            var loadedSettings = JsonUtility.FromJson<global::Settings.Settings>(content);

            if (loadedSettings == null)
            {
                return false;
            }

            settings = loadedSettings;

            return true;
        }

        public void Write(global::Settings.Settings settings)
        {
            var content = JsonUtility.ToJson(settings);
            
            File.WriteAllText(_path, content);
        }
    }
}