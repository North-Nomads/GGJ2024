using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GGJ.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        private const string IsMoving = "IsMoving";
        private const string IsRunning = "IsRunning";

        [SerializeField] private float moveSpeed;
        [SerializeField] private float sprintSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private CinemachineFreeLook playerCamera;
        [SerializeField] private Transform player;
        [SerializeField] private Animator animator;

        private Vector2 _userInput;
        private Vector3 _lookDirection;
        private float _rotationAngle;
        private float _currentSpeed;

        private void Start()
        {
            _currentSpeed = moveSpeed;
        }

        private Vector3 CameraLookDirection
        {
            get
            {
                _lookDirection = player.position - playerCamera.transform.position;
                return new Vector3(_lookDirection.x, 0, _lookDirection.z).normalized;
            }
        }

        public void HandleUserInput(InputAction.CallbackContext callbackContext)
        {
            _userInput = callbackContext.ReadValue<Vector2>();
        }

        public void HandleUserSprintInput(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.canceled)
            {
                animator.SetBool(IsRunning, false);
                _currentSpeed = moveSpeed;
                return;
            }

            animator.SetBool(IsRunning, true);
            _currentSpeed = sprintSpeed;
        }

        private void FixedUpdate()
        {
            if (_userInput == Vector2.zero)
            {
                animator.SetBool(IsMoving, false);
                return;
            }
                

            animator.SetBool(IsMoving, true);

            _rotationAngle = GetRotationAngle();

            player.rotation = Quaternion.Lerp(player.rotation, GetTargetRotation(), Time.fixedDeltaTime * rotationSpeed);
            transform.position += _currentSpeed * Time.fixedDeltaTime * player.forward;

            Quaternion GetTargetRotation() => Quaternion.Euler(0, _rotationAngle, 0) * Quaternion.LookRotation(CameraLookDirection);
        }

        private float GetRotationAngle()
        {
            if (_userInput.y < 0f)
                return 180f;
            else if (_userInput.x > 0f)
                return 90f;
            else if (_userInput.x < 0f)
                return -90f;
            return 0;
        }
    }
}