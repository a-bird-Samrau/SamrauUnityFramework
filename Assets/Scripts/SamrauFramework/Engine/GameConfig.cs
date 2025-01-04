using SamrauFramework.Player;
using SamrauFramework.UI;
using UnityEngine;

namespace SamrauFramework.Engine
{
    [CreateAssetMenu(fileName = "New Game Config", menuName = "Create Game Config")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private PlayerCharacter _playerCharacter;
        [SerializeField] private UserInterface _userInterface;
        [SerializeField] private Level _startupLevel;
        [SerializeField] private SamrauFramework.Settings.Settings _defaultSettings;

        public PlayerCharacter PlayerCharacter => _playerCharacter;
        public UserInterface UserInterface => _userInterface;
        public Level StartupLevel => _startupLevel;
        public SamrauFramework.Settings.Settings DefaultSettings => _defaultSettings;
    }
}