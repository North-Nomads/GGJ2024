using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace GGJ.Achievements
{
    public class AchievementsListUI : MonoBehaviour
    {
        [SerializeField] private AchievementsManager achievementsManager;
        [SerializeField] private AchievementCard cardPrefab;
        [SerializeField] private GridLayoutGroup viewport;

        private GameObject _parent;

        private void Start()
        {
            // HACK: made here for test.
            achievementsManager.Load();

            // Initialize all cards (sort by completed, then by not secret).
            foreach (var achievement in achievementsManager.Achievements.OrderBy(x => x switch
            {
                { Completed: true } => 0,
                { IsHidden: false } => 1,
                _ => 2
            }))
            {
                var card = Instantiate(cardPrefab, viewport.transform);
                card.SetAchievementInfo(achievement);
            }

            _parent = transform.parent.gameObject;
            _parent.SetActive(false);

        }

        public void OpenCloseWindow(InputAction.CallbackContext callbackContext)
        {
            if (!callbackContext.started)
                return;

            _parent.SetActive(!_parent.activeInHierarchy);
        }

        public void CloseWindow(InputAction.CallbackContext callbackContext)
        {
            if (!gameObject.activeSelf)
                return;

            _parent.SetActive(false);
        }
    }
}
