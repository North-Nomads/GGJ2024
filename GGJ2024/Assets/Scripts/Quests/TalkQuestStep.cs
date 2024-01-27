using UnityEngine;

namespace GGJ.Quests
{
    public class TalkQuestStep : QuestStep
    {
        // TODO: Change to NPC character type
        [SerializeField] private GameObject talkTo;
        
        public override void Initialize(GameObject playerInstance, QuestView questView) { }
    }
}