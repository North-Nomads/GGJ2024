using System.Collections.Generic;
using GGJ.Infrastructure.AssetManagement;
using UnityEngine;

namespace Logic
{
    public class ObjectPool<T> where T : Component
    {
        private readonly GameObject _parent;
        
        private int _poolCount;
        private string _prefabPath;
        private List<T> _pool;
        
        private IAssetProvider _assetProvider;

        public ObjectPool(GameObject parent)
        {
            _parent = parent;
        }
        
        public void Construct(IAssetProvider assetProvider) => 
            _assetProvider = assetProvider;

        public void Initialize(string prefabPath, int poolCount)
        {
            _poolCount = poolCount;
            _prefabPath = prefabPath;
            _pool = new List<T>();
        }

        public void Create()
        {
            _pool.Clear();

            for (int i = 0; i < _poolCount; i++)
            {
                T instantiated = _assetProvider.Instantiate(_prefabPath).GetComponent<T>();
                instantiated.gameObject.SetActive(false);
                instantiated.transform.parent = _parent.transform;
                _pool.Add(instantiated);
            }
        }

        public bool TryGetNext(out T obj)
        {
            obj = _pool.Find(obj => !obj.gameObject.activeInHierarchy);
            return obj != null;
        }
    }
}