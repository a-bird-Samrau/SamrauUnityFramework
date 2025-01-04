namespace SamrauFramework.Settings
{
    public interface ISettings
    {
        bool IsFullscreen { get; }

        float FieldOfView { get; }
        
        float VerticalSensitive { get; }
        float HorizontalSensitive { get; }

        float MusicVolume { get; }
        float SoundVolume { get; }
    }
}