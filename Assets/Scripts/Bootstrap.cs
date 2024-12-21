using Engine;
using UnityEngine;
using Behaviour = Core.Behaviour;

public class Bootstrap : Behaviour
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