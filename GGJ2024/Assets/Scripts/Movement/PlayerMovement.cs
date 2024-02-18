using UnityEngine;
using UnityEngine.InputSystem;

namespace GGJ.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private Transform playerCamera;
        [SerializeField] private Transform player;

        private Vector2 _userInput;

        public void HandleUserInput(InputAction.CallbackContext callbackContext)
        {
            _userInput = callbackContext.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            if (_userInput == Vector2.zero)
                return;

            float angle = 0f;

            if (_userInput.y < 0f)
                angle = 180f;
            else if (_userInput.x > 0f)
                angle = 90f;
            else if (_userInput.x < 0f)
                angle = -90f;


            player.rotation = Quaternion.Euler(0, angle, 0);
            transform.position += new Vector3(_userInput.x, 0, _userInput.y) * moveSpeed * Time.deltaTime;
        }
    }
}