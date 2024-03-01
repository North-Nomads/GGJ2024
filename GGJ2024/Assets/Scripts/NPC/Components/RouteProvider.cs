using System;
using NPC.Routes;
using UnityEngine;

namespace NPC.Components
{
    public class RouteProvider
    {
        private Route _route;
        private RoutePoint _currentRoutePoint;
        private Coroutine _routeWalkingRoutine;

        public Route Route => _route;
        public RoutePoint CurrentRoutePoint => _currentRoutePoint;
        public WalkableNpc WalkableNpc { get; private set; }

        public event EventHandler DestinationPointReached;
        public event Action CurrentPointChanged;

        public RouteProvider(WalkableNpc walkableNpc)
        {
            WalkableNpc = walkableNpc;
        }

        public void ChangeRoute(Route nextRoute)
        {
            _route = nextRoute;
            
            _currentRoutePoint = _route.RootPoint;
            _currentRoutePoint.TriggerObserver.TriggerEntered += OnPointTriggerEntered;
            CurrentPointChanged?.Invoke();
        }

        private void OnPointTriggerEntered(Collider other)
        {
            _currentRoutePoint.TriggerObserver.TriggerEntered -= OnPointTriggerEntered;

            if (_currentRoutePoint != _route.DestinationPoint)
            {
                _currentRoutePoint = _currentRoutePoint.NextPoint;
                _currentRoutePoint.TriggerObserver.TriggerEntered += OnPointTriggerEntered;
                CurrentPointChanged?.Invoke();
            }
            else
                DestinationPointReached?.Invoke(this, null);
        }
    }
}