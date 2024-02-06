using GGJ.Infrastructure.Services;
using GGJ.Infrastructure.States;

namespace GGJ.Infrastructure
{
    public class Game
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public Game(ICoroutineRunner coroutineRunner)
        {
            _sceneLoader = new SceneLoader(coroutineRunner);
            _stateMachine = new GameStateMachine(_sceneLoader, AllServices.Container);
        }
    }
}