using GGJ.Inventory;
using TMPro;
using UnityEngine;

namespace GGJ.Quests
{
    public class QuestView : MonoBehaviour
    {
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text goalText;
        [SerializeField] private Color finishedQuestFontColor;
        [SerializeField] private Color inProgressQuestFontColor;

        public string Title
        {
            get => title.text;
            set => title.text = value;
        }

        public string GoalText
        {
            get => goalText.text;
            set => goalText.text = value;
        }

        public Color FinishedQuestFontColor => finishedQuestFontColor;
        public Color InProgressQuestFontColor => inProgressQuestFontColor;

        public void SetGoalTextColor(Color value) => 
            goalText.color = value;
    }
}
