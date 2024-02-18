using System.Linq;
using UnityEngine;

namespace GGJ.Quests
{
    [CreateAssetMenu(fileName = "New Quest Giver Info", menuName = "Quests/QuestGiver definition")]
    public class QuestGiverInfo : ScriptableObject
    {
        [SerializeField] private string name;
        [SerializeField] private QuestInfo[] quests;
        [Tooltip("Phrase that is used when player approaches npc that doesn't have quest for him")]
        [SerializeField] private string busyPhrase;

        public string Name => name;
        public QuestInfo[] Quests => quests;
        public string BusyPhrase => busyPhrase;

        public bool TryGetNextQuest(QuestInfo currentQuest, out QuestInfo quest)
        {
            quest = null;
            
            try
            {
                quest = quests.Where(quest => quest.Id > currentQuest.Id).Min();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public QuestInfo GetQuestWithID(int id) => quests.FirstOrDefault(x => x.Id == id);
    }
}