using GGJ.Infrastructure.States;
using UnityEngine;

namespace GGJ.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private Game _game;

        private void Awake()
        {
            _game = new Game(this);
            _game.StateMachine.EnterIn<BootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}