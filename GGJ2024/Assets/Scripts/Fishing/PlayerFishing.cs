using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GGJ.Fishing
{
    [RequireComponent(typeof(Animator))]
    public class PlayerFishing : MonoBehaviour
    {
        private Animator _animationController;

        private void Start()
        {
            _animationController = GetComponent<Animator>();
        }

        public void OnFishingCast(InputAction.CallbackContext context)
        {
            _animationController.SetTrigger("Cast");
        }
    }
}