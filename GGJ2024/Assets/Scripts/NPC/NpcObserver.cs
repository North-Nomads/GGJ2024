using System;
using System.Collections;
using System.Collections.Generic;
using GGJ.Infrastructure.AssetManagement;
using NPC.Components;
using NPC.Routes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NPC
{
    public class NpcObserver : MonoBehaviour
    {
        private const float RefreshRate = 2.5f; 
        
        [SerializeField] private NpcFactory npcFactory;
        [SerializeField] private List<Route> routes;

        private List<WalkableNpc> _activeNpc = new();
        private List<Route> _availableRoutes;
        
        public void Initialize(IAssetProvider assetProvider)
        {
            npcFactory.Initialize(assetProvider);
            
            routes.ForEach(route => route.Initialize());
            //routes.ForEach(route => route.RootPointCameraVisibleChanged += OnRootPointCameraVisibleChanged);
            _availableRoutes = new List<Route>(routes);

            StartCoroutine(StartObserve());
        }

        private IEnumerator StartObserve()
        {
            while (true)
            {
                while (_availableRoutes.Count != 0)
                {
                    SpawnRandomNpcAtRandomRoute();
                    yield return null;
                }

                yield return new WaitForSeconds(RefreshRate);
            }
        }

        private void OnDestinationPointReached(object sender, EventArgs args) => 
            DespawnNpc((sender as RouteProvider)?.WalkableNpc);

        /*private void OnRootPointCameraVisibleChanged(object sender, bool isVisible)
        {
            // TODO: Think about that
            Route route = sender as Route;

            if (_activeNpc.Exists(npc => npc.Route == route))
                return;

            bool isRouteAvailable = _availableRoutes.Contains(route);

            if (isRouteAvailable && isVisible)
                _availableRoutes.Remove(route);
            else if (!isRouteAvailable && !isVisible)
                _availableRoutes.Add(route);
        }*/

        private void SpawnRandomNpcAtRandomRoute()
        {
            if (npcFactory.TryGetRandomNpc(out WalkableNpc npc))
                if (_availableRoutes.Count != 0)
                    SpawnNpc(npc, _availableRoutes[Random.Range(0, routes.Count)]);
        }

        private void SpawnNpc(WalkableNpc npc, Route route)
        {
            npc.RouteProvider.DestinationPointReached += OnDestinationPointReached;
                
            npc.transform.position = route.RootPoint.transform.position;
            npc.gameObject.SetActive(true);
            
            npc.RouteProvider.ChangeRoute(route);
            
            _activeNpc.Add(npc);
            _availableRoutes.Remove(route);
        }

        private void DespawnNpc(WalkableNpc npc)
        {
            if (npc == null) return;
            
            npc.RouteProvider.DestinationPointReached -= OnDestinationPointReached;
            
            _activeNpc.Remove(npc);
            _availableRoutes.Add(npc.RouteProvider.Route);
            
            npc.gameObject.SetActive(false);
        }

        private void DespawnAllNpc()
        {
            WalkableNpc[] activeNpc = _activeNpc.ToArray();
            foreach (WalkableNpc npc in activeNpc) 
                DespawnNpc(npc);
        }
    }
}