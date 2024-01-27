using UnityEngine;

namespace GGJ.Quests
{
    /*public class Quest : 
    {
        [SerializeField] privte 


        */
    
    /*[SerializeField] private QuestInfo questInfo;
        [SerializeField] private List<QuestStep> questSteps;

        private int _currentQuestStepIndex;
        private QuestStep _currentQuestStep;
        private QuestView _questView;

        public QuestInfo QuestInfo => questInfo;
        public List<QuestStep> QuestSteps => questSteps;
        public QuestStep CurrentQuestStep => _currentQuestStep;
        public bool IsCompleted { get; private set; }

        public Action OnQuestCompleted;

        public void Initialize(GameObject playerInstance, QuestView questView, int nextQuestStepIndex)
        {
            if (nextQuestStepIndex < 0 || nextQuestStepIndex >= questSteps.Count)
            {
                throw new ArgumentOutOfRangeException("nextQuestStepIndex out of range");
            }

            foreach (QuestStep step in questSteps)
            {
                step.Initialize(playerInstance, questView);
                step.OnCompleteQuestStep += OnQuestStepCompleted;
            }
            
            _currentQuestStepIndex = nextQuestStepIndex;
            _currentQuestStep = questSteps[nextQuestStepIndex];
            Debug.Log("Quest initialized");
        }
        
        private void Awake()
        {
            if (questSteps.Count == 0)
            {
                throw new NullReferenceException("Quest steps count can't be zero");
            }
        }

        private void OnQuestStepCompleted(object sender, QuestStepEventArgs args)
        {
            if (!args.IsFinished && CurrentQuestStep != (QuestStep)sender)
            {
                int unfinishedQuestStepIndex = questSteps.IndexOf((QuestStep)sender);
                int currentQuestStepIndex = questSteps.IndexOf(CurrentQuestStep);

                for (int i = currentQuestStepIndex; i >= unfinishedQuestStepIndex; i--)
                {
                    questSteps[i].IsFinished = false;
                }
            }

            if (!args.IsFinished)
            {
                return;
            }
            
            if (_currentQuestStepIndex == questSteps.Count - 1)
            {
                OnQuestCompleted?.Invoke();
                IsCompleted = true;
                return;
            }
            
            _currentQuestStep = questSteps[_currentQuestStepIndex + 1];
        }*//*
}*/
}