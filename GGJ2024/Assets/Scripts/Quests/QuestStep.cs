using System;
using GGJ.Quests.CustomEventArgs;
using UnityEngine;

namespace GGJ.Quests
{
    public abstract class QuestStep : MonoBehaviour
    {
        public bool IsFinished { get; set; }
        public QuestStepGoalString QuestStepGoalString { get; protected set; } = new();
        
        public event EventHandler<QuestStepEventArgs> OnCompleteQuestStep;

        public abstract void Initialize(GameObject playerInstance, QuestView questView);

        protected void CompleteQuestStep()
        {
            OnCompleteQuestStep?.Invoke(this, new QuestStepEventArgs(IsFinished));
        }
    }
}