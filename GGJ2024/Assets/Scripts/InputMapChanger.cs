using UnityEngine;
using UnityEngine.InputSystem;

namespace GGJ
{
    public class InputMapChanger : MonoBehaviour
    {
        private readonly string _playerActionMapName = "Player";
        private readonly string _uiActionMapName = "UI";

        [SerializeField] private PlayerInput playerInput;

        public void SetUIActionMap(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.performed)
            {
                playerInput.SwitchCurrentActionMap(_uiActionMapName);
            }
        }

        public void SetPlayerActionMap(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.performed)
            {
                playerInput.SwitchCurrentActionMap(_playerActionMapName);
            }
        }
    }
}