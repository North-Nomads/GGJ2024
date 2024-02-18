using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GGJ.Dialogs
{
    public class DialogInputHandler : MonoBehaviour
    {
        public event Action DialogVariantSkipButtonPressed;
        public event Action DialogStartButtonPressed;

        public void OnDialogVariantSkipButtonPressed(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.performed)
                DialogVariantSkipButtonPressed?.Invoke();
        }

        public void OnDialogStartButtonPressed(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.performed)
                DialogStartButtonPressed?.Invoke();
        }
    }
}