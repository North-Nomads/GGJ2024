using GGJ;
using System.Collections;
using System.Collections.Generic;
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

            gameObject.SetActive(false);
        }

        public void OpenCloseWindow(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.canceled)
                return;

            gameObject.SetActive(!gameObject.activeInHierarchy);
        }
    }
}
