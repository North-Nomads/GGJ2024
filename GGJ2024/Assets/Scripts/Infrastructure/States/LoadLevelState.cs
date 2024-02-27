using System;
using Cinemachine;
using GGJ.Dialogs;
using GGJ.Infrastructure.AssetManagement;
using GGJ.Infrastructure.Factories;
using GGJ.Infrastructure.Services.Services.PersistentProgress;
using GGJ.Infrastructure.Services.Services.SaveLoad;
using GGJ.Infrastructure.States.Interfaces;
using GGJ.Movement;
using GGJ.Quests;
using NPC;
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
            QuestView questView = _gameFactory.CreateQuestCanvas().GetComponentInChildren<QuestView>();

            InitCamera(character, camera.transform); 
            //InitHud(character, questView);
            InitNpc();
            InitQuestGivers(character);
        }

        private void InitCamera(GameObject character, Transform camera)
        {
            if (character.TryGetComponent(out PlayerMovement movement))
            {
                movement.PlayerCamera = camera;
                CinemachineFreeLook cinemachineCamera = camera.GetComponent<CinemachineFreeLook>();

                cinemachineCamera.Follow = movement.transform;
                cinemachineCamera.LookAt = movement.CameraTargetPoint;
            }
            else
                throw new NullReferenceException($"PlayerMovement not found on {character.name} {character.GetType()}");
        }

        private void InitNpc()
        {
            NpcObserver observer = Object.FindObjectOfType<NpcObserver>();

            if (observer != null) 
                observer.Initialize(_assetProvider);
        }

        private void InitHud(GameObject character, QuestView questView)
        {
            if (character.TryGetComponent(out QuestManager questManager))
                questManager.Construct(questView);
            else
                throw new NullReferenceException($"Quest Manager not found on {character.name} {character.GetType()}");
        }

        private void InitQuestGivers(GameObject character)
        {
            if (character.TryGetComponent(out DialogInputHandler dialogInputHandler))
            {
                QuestGiverPrefab[] questGivers =
                    Object.FindObjectsByType<QuestGiverPrefab>(FindObjectsInactive.Include, FindObjectsSortMode.None);

                Array.ForEach(questGivers, e => e.Construct(character));
            }
            else
                throw new NullReferenceException(
                    $"Dialog Input Handler not found on {character.name} {character.GetType()}");
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