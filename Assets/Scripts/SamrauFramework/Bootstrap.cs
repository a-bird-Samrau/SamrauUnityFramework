using SamrauFramework.Engine;
using UnityEngine;
using Behaviour = SamrauFramework.Core.Behaviour;

namespace SamrauFramework
{
    public class Bootstrap : Core.Behaviour
    {
        [SerializeField] private GameConfig _gameConfig;

        private Game _activeGame;
    
        protected override void Start()
        {
            base.Start();

            Coroutines.Initialize();
        
            _activeGame = new Game(_gameConfig);
            _activeGame.Run();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        
            _activeGame = null;
        }
    }
}