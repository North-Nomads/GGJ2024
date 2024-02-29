using System;
using UnityEngine;

namespace Logic
{
    [RequireComponent(typeof(Collider))]
    public class TriggerObserver : MonoBehaviour
    {
        public Collider Collider { get; private set; }
        
        public event Action<Collider> TriggerEntered;
        public event Action<Collider> TriggerExited;
        public event Action<Collider> TriggerStayed;

        private void Awake() => Collider = GetComponent<Collider>();

        private void OnTriggerEnter(Collider other) => TriggerEntered?.Invoke(other);

        private void OnTriggerExit(Collider other) => TriggerExited?.Invoke(other);

        private void OnTriggerStay(Collider other) => TriggerStayed?.Invoke(other);
    }
}