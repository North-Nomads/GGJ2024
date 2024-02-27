using System.Collections;
using Logic;
using UnityEngine;

namespace NPC.Components
{
    public class LookAtPlayer : MonoBehaviour
    {
        private const string PlayerTag = "Player";
        
        [SerializeField] private Transform modelHead;
        [SerializeField] private TriggerObserver visionTrigger;

        private Transform _playerTransform;
        
        private void OnEnable()
        {
            visionTrigger.TriggerEntered += OnPlayerBecameVisible;
            visionTrigger.TriggerExited += OnPlayerBecameInvisible;
        }

        private void OnDisable()
        {
            visionTrigger.TriggerEntered -= OnPlayerBecameVisible;
            visionTrigger.TriggerExited -= OnPlayerBecameInvisible;
        }
        
        private void OnPlayerBecameVisible(Collider other)
        {
            if (other.transform.CompareTag(PlayerTag))
                _playerTransform = other.transform;
        }

        private void OnPlayerBecameInvisible(Collider other)
        {
            if (other.transform.CompareTag(PlayerTag))
                _playerTransform = null;
        }

        public bool TryLookAtPlayer(float duration)
        {
            if (_playerTransform != null)
            {
                StartCoroutine(LookAtPlayerRoutine(duration));
                return true;
            }

            return false;
        }

        private IEnumerator LookAtPlayerRoutine(float duration)
        {
            float lookAtPlayerTimer = 0f;
            
            while (_playerTransform != null && lookAtPlayerTimer < duration)
            {
                modelHead.LookAt(_playerTransform);
                lookAtPlayerTimer += Time.deltaTime;
                yield return null;
            }
        }
    }
}