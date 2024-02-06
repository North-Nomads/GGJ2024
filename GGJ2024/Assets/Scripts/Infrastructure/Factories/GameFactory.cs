using System.Collections.Generic;
using GGJ.Infrastructure.AssetManagement;
using GGJ.Infrastructure.Services.Services.SaveLoad;
using UnityEngine;

namespace GGJ.Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;

        public List<ISavedProgressWriter> ProgressWriters { get; } = new List<ISavedProgressWriter>();
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        
        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public GameObject CreateCharacter(Vector3 at) => 
            _assetProvider.Instantiate(AssetPath.CharacterPath, at);

        public GameObject CreateHud() => 
            _assetProvider.Instantiate(AssetPath.HudPath);

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