using System.Collections;
using Logic;
using UnityEngine;

namespace NPC.Components
{
    public class Vision : MonoBehaviour
    {
        private const string PlayerTag = "Player";
        
        [Header("Head settings")]
        [SerializeField] private Transform modelHead;
        [SerializeField, Min(0f)] private float maxRotationAngle;
        
        [Header("Vision settings")]
        [SerializeField] private TriggerObserver visionTrigger;
        [SerializeField, Min(0f)] private float minVisionPoint;

        private Transform _playerTransform;
        private Quaternion _initialRotation;

        public bool PlayerInVisionRadius => _playerTransform != null;

        public bool TryLookAtPlayer(float duration)
        {
            if (_playerTransform != null)
            {
                StartCoroutine(LookAtPlayerRoutine(duration));
                return true;
            }

            return false;
        }
        
        private void Awake()
        {
            _initialRotation = modelHead.rotation;
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

        private bool ObjectInMinVisionRadius(Vector3 position) => 
            Vector3.Distance(transform.position, position) < minVisionPoint;

        private void LookAtPlayerWhenMinVisionPoint()
        {
            if (PlayerInVisionRadius && ObjectInMinVisionRadius(_playerTransform.position))
                RotateHeadTowards(_playerTransform.position);
            else
                RotateHeadTowards(transform.TransformDirection(transform.forward));
        }

        private IEnumerator LookAtPlayerRoutine(float duration)
        {
            float lookAtPlayerTimer = 0f;
            
            while (PlayerInVisionRadius && lookAtPlayerTimer < duration)
            {
                RotateHeadTowards(_playerTransform.position);
                lookAtPlayerTimer += Time.deltaTime;
                yield return null;
            }
        }

        private void RotateHeadTowards(Vector3 targetPosition)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetPosition - modelHead.position);
            targetRotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);
            
            float angleDifference = Quaternion.Angle(_initialRotation, targetRotation);
            float allowedRotationAngle = Mathf.Min(maxRotationAngle, angleDifference);

            modelHead.rotation = Quaternion.Lerp(modelHead.rotation, Quaternion.RotateTowards(_initialRotation, targetRotation, allowedRotationAngle), Time.deltaTime * 3f);
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (Application.isPlaying)
            {
                DrawVisionSphere();
                DrawClosestVisionPointSphere();
            }
        }

        private void DrawVisionSphere() => 
            Gizmos.DrawWireSphere(transform.position, visionTrigger.Collider.bounds.size.x / 2);

        private void DrawClosestVisionPointSphere() => 
            Gizmos.DrawWireSphere(transform.position, minVisionPoint);
#endif
    }
}