using System;
using Cinemachine;
using GGJ.Infrastructure.Factories;
using GGJ.Infrastructure.Services.Services.PersistentProgress;
using GGJ.Infrastructure.Services.Services.SaveLoad;
using GGJ.Infrastructure.States.Interfaces;
using GGJ.Movement;
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

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, IPersistentProgressService progressService, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _progressService = progressService;
            _gameFactory = gameFactory;

            Cursor.visible = false;
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
            GameObject character = _gameFactory.CreateCharacter(GameObject.FindGameObjectWithTag(InitialCharacterPointTag).transform.position);
            GameObject camera = _gameFactory.CreateCamera();
            // TODO: Make instantiate method generic to return given object type
            // TODO: Get rid of getcomponent in set up camera values and make character variable of PlayerMovement type
            SetUpCameraValues(character.GetComponent<PlayerMovement>(), camera.transform); 

            
            QuestView questView = _gameFactory.CreateQuestCanvas().GetComponentInChildren<QuestView>();
            //_gameFactory.CreateInventoryCanvas();

            InitQuestGivers(character);
            InitHud(character, questView);
        }

        private void SetUpCameraValues(PlayerMovement player, Transform camera)
        {
            player.PlayerCamera = camera;
            var cinemachineCamera = camera.GetComponent<CinemachineFreeLook>();
            cinemachineCamera.Follow = player.transform;
            cinemachineCamera.LookAt = player.CameraTargetPoint;
        }

        private void InitHud(GameObject character, QuestView questView)
        {
            if (character.TryGetComponent(out QuestManager questManager)) 
                questManager.Construct(questView);
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