using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GGJ.Achievements
{
    public class AchievementCard : MonoBehaviour
    {
        private static Sprite defaultAchievementIcon;

        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text description;
        [SerializeField] private Image progress;
        [SerializeField] private TMP_Text status;
        [SerializeField] private Image cover;

        private void Start()
        {
            defaultAchievementIcon ??= Resources.Load<Sprite>("Achievements/Textures/DefaultAchievement.png");
        }

        public void SetAchievementInfo(AchievementInfo info)
        {
            progress.fillAmount = Mathf.Clamp01(info.CurrentProgress / (float)info.RequiredAmount);
            status.text = $"{info.CurrentProgress}/{info.RequiredAmount}";
            if (info.Completed)
            {
                icon.sprite = info.Icon;
                title.text = info.Title;
                title.color = GetAchievementColor(info.Rarity);
                description.text = info.Description;
                if (info.Background != null)
                {
                    cover.sprite = info.Background;
                }
            }
            else
            {
                icon.sprite = defaultAchievementIcon;
                title.color = Color.gray;
                description.color = new Color(0.3f, 0.3f, 0.3f);
                if (info.IsHidden)
                {
                    title.text = "Скрытое достижение";
                    description.text = "Получите достижение своими силами (или напишите разработчику, можем подсказать)";
                }
                else
                {
                    title.text = info.Title;
                    description.text = info.Description;
                }
            }
        }

        private Color GetAchievementColor(AchievementInfo.AchievementRarity rarity) => rarity switch
        {
            AchievementInfo.AchievementRarity.Common => Color.white,
            AchievementInfo.AchievementRarity.Rare => Color.blue,
            AchievementInfo.AchievementRarity.Legendary => Color.yellow,
            _ => Color.black,
        };
    }
}