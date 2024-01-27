using UnityEngine;

namespace GGJ.Quests
{
    public class QuestGiver : MonoBehaviour
    {
        [SerializeField] private Quest quest;

        public void TakeQuest()
        {
            
        }

        public void PassQuest(Quest questToPass)
        {
            if (questToPass.QuestInfo == quest.QuestInfo && questToPass.IsCompleted)
            {
                
            }
        }
    }
}