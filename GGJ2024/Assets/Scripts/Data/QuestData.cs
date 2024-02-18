using System;
using GGJ.Quests;

namespace GGJ.Data
{
    [Serializable]
    public class QuestData
    {
        public QuestInfo LastCompletedQuest { get; set; }
        public QuestInfo CurrentQuest { get; set; }

        public QuestData(QuestInfo lastCompletedQuest, QuestInfo currentQuest)
        {
            LastCompletedQuest = lastCompletedQuest;
            CurrentQuest = currentQuest;
        }
    }
}