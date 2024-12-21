using System;
using Engine;
using UnityEngine;

namespace Settings
{
    [Serializable]
    public class Settings : ISettings
    {
        [SerializeField] private bool _isFullscreen;

        [SerializeField] private float _fieldOfView;
        
        [SerializeField] private float _verticalSensitive;
        [SerializeField] private float _horizontalSensitive;

        [SerializeField] private float _musicVolume;
        [SerializeField] private float _soundVolume;

        public bool IsFullscreen => _isFullscreen;
        public float FieldOfView => _fieldOfView;

        public float VerticalSensitive => _verticalSensitive;
        public float HorizontalSensitive => _horizontalSensitive;

        public float MusicVolume => _musicVolume;
        public float SoundVolume => _soundVolume;

        public Settings Clone()
        {
            return (Settings)MemberwiseClone();
        }
    }
}