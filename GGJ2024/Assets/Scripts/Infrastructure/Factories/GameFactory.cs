using System;
using System.Collections.Generic;
using Cinemachine;
using GGJ.Infrastructure.AssetManagement;
using GGJ.Infrastructure.Services.Services.SaveLoad;
using UnityEngine;

namespace GGJ.Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;

        public List<ISavedProgressWriter> ProgressWriters { get; } = new();
        public List<ISavedProgressReader> ProgressReaders { get; } = new();
        
        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public GameObject CreateCharacter(Vector3 at) => 
            _assetProvider.Instantiate(AssetPath.CharacterPath, at);

        public GameObject CreateQuestCanvas() => 
            InstantiateRegistered(AssetPath.QuestCanvasPath);

        public GameObject CreateInventoryCanvas() => 
            InstantiateRegistered(AssetPath.InventoryCanvasPath);

        public GameObject CreateCamera()
            => _assetProvider.Instantiate(AssetPath.PlayerCamera);

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private GameObject InstantiateRegistered(string path)
        {
            GameObject gameObject = _assetProvider.Instantiate(path);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgressWriter progressWriter) 
                ProgressWriters.Add(progressWriter);
            
            ProgressReaders.Add(progressReader);
        }

    }
}