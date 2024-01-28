using System.Collections;
using UnityEngine;

namespace GGJ.Quests
{
    /// <summary>
    /// A class that represents the in-game NPC object (talkable, quest-giver)
    /// </summary>
    public class QuestGiverPrefab : MonoBehaviour
    {
        [SerializeField] private QuestGiverInfo questGiverInfo;
        [SerializeField] private GameObject speechBubble;
        
        public QuestGiverInfo QuestGiverInfo => questGiverInfo;

        private const float ReadingTime = 2f;

        public IEnumerator StartMonologForQuest(int questID)
        {
            var quest = questGiverInfo.GetQuestWithID(questID);
            for (int i = 0; i < quest.MonologSpeeches.Length; i++)
            {
                DisplayPhrase(quest.MonologSpeeches[i]);
                yield return new WaitForSeconds(ReadingTime);
            }
        }

        private void DisplayPhrase(string phrase)
        {
            // speechBubble.text = phrase
            // waitforseconds
            // display new phrase
        }
    }
}