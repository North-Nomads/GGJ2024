using System;

namespace GGJ.Quests.CustomEventArgs
{
    public class QuestStepEventArgs : EventArgs
    {
        public bool IsFinished { get; }

        public QuestStepEventArgs(bool isFinished)
        {
            IsFinished = isFinished;
        }
    }
}