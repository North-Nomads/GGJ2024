using System.Linq;
using UnityEngine;

namespace GGJ.Quests
{
    [CreateAssetMenu(fileName = "New Quest Giver Info", menuName = "Quests/QuestGiver definition")]
    public class QuestGiverInfo : ScriptableObject
    {
        [SerializeField] private QuestInfo[] _quests;
        [Tooltip("Phrase that is used when player approaches npc that doesn't have quest for him")]
        [SerializeField] private string busyPhrase;
        
        public string BusyPhrase => busyPhrase;

        public bool CanPlayerSpeakToThisNPC(QuestInfo currentQuest) => _quests.Any(quest => quest.Id == currentQuest.Id);

        public QuestInfo GetQuestWithID(int id) => _quests.FirstOrDefault(x => x.Id == id);
    }
}