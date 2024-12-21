using System;
using System.Collections;
using Player;
using SaveLoad;
using Settings;
using UI;
using Object = UnityEngine.Object;

namespace Engine
{
    public class Game : IDisposable
    {
        private InputHandler _activeInputHandler;
        private UserInterface _activeUserInterface;
        
        private bool _isActive;

        private readonly SaveLoadManager _saveLoadManager;
        private readonly SettingsManager _settingsManager;
        private readonly LevelManager _levelManager;
        private readonly GameConfig _config; 
        
        public Game(GameConfig config)
        {
            _saveLoadManager = new SaveLoadManager();
            _settingsManager = new SettingsManager(config.DefaultSettings);
            
            _levelManager = new LevelManager();
            
            _levelManager.Loading += OnLevelLoading;
            _levelManager.Loaded += OnLevelLoaded;
            
            _config = config;
        }

        private IEnumerator BeginPlayAsync()
        {
            var playerStart = Object.FindFirstObjectByType<PlayerStart>();

            if (playerStart == null)
            {
                throw new NullReferenceException("Player Start отсутствует!");
            }

            var playerStartTransform = playerStart.transform;
            var playerCharacter = Object.Instantiate(_config.PlayerCharacter, playerStartTransform.position,
                playerStartTransform.rotation);
            
            playerCharacter.Construct();

            yield return null;
            
            _activeInputHandler.Control(playerCharacter);
            _activeUserInterface.SendOnPlayerCharacterSpawned(playerCharacter);

            yield return null;
            
            playerCharacter.BeginPlay();
        }

        private void OnLevelLoading()
        {
            if (!_isActive)
            {
                return;
            }
            
            _activeInputHandler.StopControlling();
            _activeUserInterface.SendOnPlayerCharacterDestroyed();
        }
        
        private void OnLevelLoaded(ILevel level)
        {
            Coroutines.Run(BeginPlayAsync());
        }

        public void LoadLevel(ILevel level)
        {
            _levelManager.Load(level);
        }

        public void Run()
        {
            if (_isActive)
            {
                throw new Exception("Игра уже запущена!");
            }
            
            _activeInputHandler = Utilities.CreateGameObjectWithBehaviour<InputHandler>("Input Handler");
            
            _activeUserInterface = Object.Instantiate(_config.UserInterface);
            _activeUserInterface.Construct(this);

            _settingsManager.Load();

            LoadLevel(_config.StartupLevel);
            
            _isActive = true;
        }

        public void Dispose()
        {
            _levelManager.Loading -= OnLevelLoading;
            _levelManager.Loaded -= OnLevelLoaded;
        }
    }
}