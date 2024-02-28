using System.Collections;
using Logic;
using UnityEngine;

namespace NPC.Components
{
    public class LookAtPlayer : MonoBehaviour
    {
        private const string PlayerTag = "Player";
        
        [Header("Head settings")]
        [SerializeField] private Transform modelHead;
        [SerializeField, Min(0f)] private float maxRotationAngle;
        
        [Header("Vision settings")]
        [SerializeField] private TriggerObserver visionTrigger;
        [SerializeField, Min(0f)] private float minVisionPoint;

        private Transform _playerTransform;

        public bool TryLookAtPlayer(float duration)
        {
            if (_playerTransform != null)
            {
                StartCoroutine(LookAtPlayerRoutine(duration));
                return true;
            }

            return false;
        }

        private void Update() => LookAtPlayerWhenMinVisionPoint();

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

        private void LookAtPlayerWhenMinVisionPoint()
        {
            if (_playerTransform != null &&
                Vector3.Distance(transform.position, _playerTransform.position) < minVisionPoint)
            {
                modelHead.LookAt(_playerTransform);
            }
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

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            DrawVisionSphere();
            DrawClosestVisionPointSphere();
        }

        private void DrawVisionSphere() => 
            Gizmos.DrawWireSphere(transform.position, visionTrigger.Collider.bounds.size.x / 2);

        private void DrawClosestVisionPointSphere() => 
            Gizmos.DrawWireSphere(transform.position, minVisionPoint);
#endif
    }
}