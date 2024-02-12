using System;
using GGJ.Dialogs;
using GGJ.Infrastructure.AssetManagement;
using GGJ.Infrastructure.Factories;
using GGJ.Infrastructure.Services.Services.PersistentProgress;
using GGJ.Infrastructure.Services.Services.SaveLoad;
using GGJ.Infrastructure.States.Interfaces;
using GGJ.Quests;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GGJ.Infrastructure.States
{
    public class LoadLevelState : IPayloadState
    {
        private const string InitialCharacterPointTag = "InitialPlayerPoint";
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IPersistentProgressService _progressService;
        private readonly IGameFactory _gameFactory;
        private readonly IAssetProvider _assetProvider;


        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, IPersistentProgressService progressService, IGameFactory gameFactory, IAssetProvider assetProvider)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _progressService = progressService;
            _gameFactory = gameFactory;
            _assetProvider = assetProvider;
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
            GameObject character = _gameFactory.CreateCharacter(GameObject.FindGameObjectWithTag(InitialCharacterPointTag).transform.position).transform.GetChild(0).gameObject;
            //_gameFactory.CreateHud();

            InitQuestGivers(character);
        }

        private void InitQuestGivers(GameObject character)
        {
            QuestGiverPrefab[] questGivers =
                Object.FindObjectsByType<QuestGiverPrefab>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            
            Array.ForEach(questGivers, e => e.Construct(character));
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