using System;
using UnityEngine;

namespace SamrauFramework.Settings
{
    public class SettingsManager
    {
        public static event Action<ISettings> CurrentSettingsChanged;

        private static SamrauFramework.Settings.Settings _currentSettings;
        
        private readonly SamrauFramework.Settings.Settings _defaultSettings;
        private readonly SettingsFormatter _formatter;

        public SettingsManager(SamrauFramework.Settings.Settings defaultSettings)
        {
            _defaultSettings = defaultSettings;
            _formatter = new SettingsFormatter(Application.persistentDataPath + "/Settings.json");
        }

        private void SetCurrentSettings(SamrauFramework.Settings.Settings newSettings)
        {
            _currentSettings = newSettings;
            CurrentSettingsChanged?.Invoke(newSettings);
        }

        public bool Load()
        {
            if (!_formatter.Read(out var newSettings))
            {
                Reset();
                
                Save();
                
                return false;
            }

            SetCurrentSettings(newSettings);

            return true;
        }

        public void Save()
        {
            _formatter.Write(_currentSettings);
        }

        public void Reset()
        {
            SetCurrentSettings(_defaultSettings.Clone());
        }

        public static ISettings GetSettings()
        {
            return _currentSettings;
        }
    }
}