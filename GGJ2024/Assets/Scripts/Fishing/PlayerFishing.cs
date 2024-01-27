using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GGJ.Fishing
{
    [RequireComponent(typeof(Animator))]
    public class PlayerFishing : MonoBehaviour
    {
        [SerializeField] private FishingEventProvider provider;
        private Animator _animationController;

        private void Start()
        {
            _animationController = GetComponent<Animator>();
        }

        public async void OnFishingCast(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _animationController.SetTrigger("Cast");
                var minigame = Instantiate(provider.GetRandomGame());
                bool win = await minigame.StartGameAsync();
                if (win)
                {
                    var caught = Instantiate(provider.GetRandomCatchable());
                    // Do something, maybe in coroutine.
                }
                else
                {
                    // Do something on catch end.
                }
            }
        }
    }
}