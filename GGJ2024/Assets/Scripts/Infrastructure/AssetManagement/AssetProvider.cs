using UnityEngine;

namespace GGJ.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        public GameObject Instantiate(string path, Vector3 at)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }

        public TAsset[] LoadAllResources<TAsset>(string path) where TAsset : Object => 
            Resources.LoadAll<TAsset>(path);

        public TAsset LoadResources<TAsset>(string path) where TAsset : Object => 
            Resources.Load<TAsset>(path);
    }
}