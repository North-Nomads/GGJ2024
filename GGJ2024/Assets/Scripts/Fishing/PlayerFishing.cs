using GGJ.Fishing.Minigames;
using GGJ.Inventory;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GGJ.Fishing
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PlayerInventory))]
    [RequireComponent(typeof(LineRenderer))]
    public class PlayerFishing : MonoBehaviour
    {
        [SerializeField] private FishingEventProvider provider;
        [SerializeField] private float fishPullTime;
        [SerializeField] private Transform rodEnd;
        [SerializeField] private FishingMinigame minigame;
        
        private LineRenderer _fishingLineRenderer;
        private Animator _animationController;
        private PlayerInventory _inventory;
        private bool _suppressFishing;

        private void Start()
        {
            _animationController = GetComponent<Animator>();
            _fishingLineRenderer = GetComponent<LineRenderer>();
            _inventory = GetComponent<PlayerInventory>();
        }

        private void Update()
        {
            _fishingLineRenderer.SetPosition(0, rodEnd.position); 
            _fishingLineRenderer.SetPosition(1, provider.transform.position);
        }

        public void OnFishingCast(InputAction.CallbackContext context)
        {
            if (_suppressFishing || !context.performed)
                return;
            _suppressFishing = true;
            StartCoroutine(CastRod());
        }

        private IEnumerator CastRod()
        {
            _animationController.SetTrigger("Cast");
            yield return new WaitForSeconds(2);
            _fishingLineRenderer.enabled = true;
            // Just wait for the fish.
            yield return new WaitForSeconds(Random.Range(3, 13));
            var game = Instantiate(minigame);
            game.OnGameEnded += (s, e) =>
            {
                if (e)
                {
                    var caught = Instantiate(provider.GetRandomCatchable(), provider.transform.position, Quaternion.identity);
                    // Do something, maybe in coroutine.
                    StartCoroutine(PullFish(caught));
                }
                Destroy(game.gameObject);
            };
            _fishingLineRenderer.enabled = false;
            _suppressFishing = false;
        }

        private IEnumerator PullFish(CatchableItemBase fish)
        {
            float time = fishPullTime;
            Vector3 fixedMovement = transform.position - fish.transform.position;
            float speed = fixedMovement.magnitude / time;
            fixedMovement = fixedMovement.normalized * speed;
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