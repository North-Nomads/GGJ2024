using System;
using UnityEngine;

namespace Logic
{
    [RequireComponent(typeof(Collider))]
    public class CollisionObserver : MonoBehaviour
    {
        public event Action<Collision> CollisionEntered;
        public event Action<Collision> CollisionExited;

        private void OnCollisionEnter(Collision other) => CollisionEntered?.Invoke(other);
        private void OnCollisionExit(Collision other) => CollisionExited?.Invoke(other);
    }
}