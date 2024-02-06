using GGJ.Infrastructure.Factories;
using GGJ.Infrastructure.Services.Services.PersistentProgress;
using GGJ.Infrastructure.Services.Services.SaveLoad;
using GGJ.Infrastructure.States.Interfaces;
using UnityEngine;

namespace GGJ.Infrastructure.States
{
    public class LoadLevelState : IPayloadState
    {
        private const string InitialCharacterPointTag = "InitialPlayerPoint";
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IPersistentProgressService _progressService;
        private readonly IGameFactory _gameFactory;


        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, IPersistentProgressService progressService, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _progressService = progressService;
            _gameFactory = gameFactory;
        }

        public void Enter<TPayload>(TPayload payload) => 
            _sceneLoader.Load(_progressService.Progress.WorldData.PositionOnLevel.Level, OnLoaded);

        public void Exit() { }

        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressReaders();
            
            _stateMachine.EnterIn<GameLoopState>();
        }

        private void InitGameWorld()
        {
            _gameFactory.CleanUp();
            _gameFactory.CreateCharacter(GameObject.FindGameObjectWithTag(InitialCharacterPointTag).transform.position);
            _gameFactory.CreateHud();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.Load(_progressService.Progress);
            }
        }
    }
}