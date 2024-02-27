using System;
using System.Collections;
using GGJ.Infrastructure;
using NPC.Routes;
using UnityEngine;

namespace NPC.Components
{
    public class RouteProvider
    {
        private readonly ICoroutineRunner _coroutineRunner;
        
        private Route _route;
        private RoutePoint _currentRoutePoint;
        private Coroutine _routeWalkingRoutine;

        public Route Route => _route;
        public RoutePoint CurrentRoutePoint => _currentRoutePoint;

        public event EventHandler<WalkableNpc> DestinationPointReached;

        public RouteProvider(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void ChangeRoute(Route nextRoute)
        {
            _route = nextRoute;
            
            _currentRoutePoint = _route.RootPoint;
            _currentRoutePoint.TriggerObserver.TriggerEntered += OnPointTriggerEntered;
            
            _routeWalkingRoutine = _coroutineRunner.StartCoroutine(StartRouteWalking());
        }
        
        public IEnumerator StartRouteWalking()
        {
            while (_currentRoutePoint != _route.DestinationPoint)
            {
                yield return new WaitForFixedUpdate();
            }
            
            //DestinationPointReached?.Invoke(this, this);

            _routeWalkingRoutine = null;
        }

        private void OnPointTriggerEntered(Collider other)
        {
            _currentRoutePoint.TriggerObserver.TriggerEntered -= OnPointTriggerEntered;

            if (_currentRoutePoint != _route.DestinationPoint)
            {
                _currentRoutePoint = _currentRoutePoint.NextPoint;
                _currentRoutePoint.TriggerObserver.TriggerEntered += OnPointTriggerEntered;
            }
        }
    }
}