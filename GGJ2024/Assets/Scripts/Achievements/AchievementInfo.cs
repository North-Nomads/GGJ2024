using System;
using UnityEngine;

namespace GGJ.Achievements
{
    [CreateAssetMenu(fileName = "New Achievement", menuName = "Achievement")]
	public class AchievementInfo : ScriptableObject
	{
		[SerializeField] private bool isHidden;
		[SerializeField] private string id;
		[SerializeField] private string title;
		[SerializeField] [TextArea(2, 4)] private string description;
		[SerializeField] private Sprite icon;
		[SerializeField] private Sprite background;
		[SerializeField] private int requiredAmount;
		[SerializeField] private AchievementRarity rarity;
        private int currentProgress;

        public bool IsHidden => isHidden;

		public Guid Id => new(id);

		public string Title => title;
		
		public string Description => description;

		public AchievementRarity Rarity => rarity;

		public Sprite Icon => icon;

		public Sprite Background => background;

		public int RequiredAmount => requiredAmount;

        public int CurrentProgress
        {
            get => currentProgress;
            set
            {
                currentProgress = value;
				if (Completed)
				{
					AchievementCompleted?.Invoke(this, EventArgs.Empty);
				}
            }
        }

        public bool Completed => CurrentProgress >= requiredAmount;

		public event EventHandler AchievementCompleted;

        private void Reset()
        {
            id = Guid.NewGuid().ToString();
        }

		[Serializable]
		public enum AchievementRarity
		{
			Common,
			Rare,
			Legendary,
		}
    }
}