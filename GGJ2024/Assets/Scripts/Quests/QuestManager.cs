using System;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ.Quests
{
    public class QuestManager : MonoBehaviour
    {
        [SerializeField] private List<Quest> allQuests;
        [SerializeField] private Quest initialQuest;
        [SerializeField] private QuestView questView;

        private Quest _currentQuest;
        private int _currentQuestId;

        public Quest CurrentQuest
        {
            get => _currentQuest;
            set
            {
                if (value.QuestInfo.Id <= _currentQuestId)
                {
                    throw new ArgumentException("This quest already completed");
                }

                _currentQuest = value;
                _currentQuest.Initialize(gameObject, questView, 0); // why gameobject?
                _currentQuest.OnQuestCompleted += OnQuestCompleted;
                questView.Title = _currentQuest.QuestInfo.Title;
            }
        }

        private void Awake() => Initialize();

        private void Initialize()
        {
            CurrentQuest = initialQuest;
        }

        private void OnQuestCompleted()
        {
            Debug.Log("Quest completed");
        }
    }
}