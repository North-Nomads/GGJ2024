using System;
using JetBrains.Annotations;
using Logic;
using UnityEngine;

namespace NPC.Routes
{
    [RequireComponent(typeof(Collider))]
    public class RoutePoint : MonoBehaviour
    {
        [SerializeField] private TriggerObserver triggerObserver;
        
        public TriggerObserver TriggerObserver => triggerObserver;
        [CanBeNull] public RoutePoint NextPoint { get; set; }
        [CanBeNull] public RoutePoint PreviousPoint { get; set; }

        public bool IsRoot => PreviousPoint is null;
        public bool IsLast => NextPoint is null;

        public event EventHandler<bool> VisibleChanged;

        private void OnBecameVisible()
        {
            if (IsRoot) 
                VisibleChanged?.Invoke(this, true);
        }

        private void OnBecameInvisible()
        {
            if (IsRoot) 
                VisibleChanged?.Invoke(this, false);
        }
    }
}