using System;
using System.Collections.Generic;
using GGJ.Infrastructure.AssetManagement;
using Logic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NPC
{
    public class NpcFactory : MonoBehaviour
    {
        private const string WalkableNpcPath = "Npc/Walkable/";
        
        [SerializeField] private List<ObjectPoolSettings> prefabSettings;

        private Dictionary<NpcType, ObjectPool<WalkableNpc>> _pools = new();
        
        public void Initialize(IAssetProvider assetProvider)
        {
            foreach (ObjectPoolSettings settings in prefabSettings)
            {
                ObjectPool<WalkableNpc> pool = new ObjectPool<WalkableNpc>(gameObject);
                
                pool.Construct(assetProvider);
                pool.Initialize(WalkableNpcPath + settings.Prefab.name, settings.PrefabCount);
                pool.Create();
                
                _pools[settings.Type] = pool;
            }
        }

        public bool TryGetNpcByType(NpcType type, out WalkableNpc npc)
        {
            npc = null;
            
            if (_pools.TryGetValue(type, out ObjectPool<WalkableNpc> pool))
                if (pool.TryGetNext(out npc))
                    return true;

            return false;
        }

        public bool TryGetRandomNpc(out WalkableNpc npc)
        {
            NpcType randomType = GetRandomNpcType();

            if (TryGetNpcByType(randomType, out npc))
                return true;

            return false;
        }

        private NpcType GetRandomNpcType()
        {
            Array npcTypeValues = Enum.GetValues(typeof(NpcType));
            int randomNpcTypeValue = Random.Range(0, npcTypeValues.Length);
            return (NpcType)npcTypeValues.GetValue(randomNpcTypeValue);
        }

        [Serializable]
        private struct ObjectPoolSettings
        {
            [SerializeField] private WalkableNpc prefab;
            [SerializeField] private int prefabCount;
            [SerializeField] private NpcType type;

            public WalkableNpc Prefab => prefab;
            public int PrefabCount => prefabCount;
            public NpcType Type => type;
        }
    }
}