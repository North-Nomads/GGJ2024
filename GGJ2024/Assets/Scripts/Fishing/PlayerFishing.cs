using GGJ.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GGJ.Fishing
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PlayerInventory))]
    public class PlayerFishing : MonoBehaviour
    {
        [SerializeField] private FishingEventProvider provider;
        [SerializeField] private float fishPullTime;

        private Animator _animationController;
        private PlayerInventory _inventory;

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
                    StartCoroutine(PullFish(caught));
                }
                else
                {
                    // Do something on catch end.
                    Debug.Log("Player is noob so he couldn't catch the fish.");
                }
            }
        }

        private IEnumerator PullFish(CatchableItemBase fish)
        {
            float time = fishPullTime;
            Vector3 fixedMovement = transform.position - fish.transform.position;
            float speed = fixedMovement.magnitude / time;
            fixedMovement *= speed;
            while (time > 0)
            {
                yield return new WaitForEndOfFrame();
                time -= Time.deltaTime;
                fish.transform.position += fixedMovement * Time.deltaTime;
            }
            // Catch the fish
            fish.OnCatchEnd(this);
            if (fish.Item != null)
            {
                _inventory.TryAddItem(fish.Item);
            }
        }
    }
}