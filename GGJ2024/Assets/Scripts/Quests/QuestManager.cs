using GGJ.Inventory;
using System;
using UnityEngine;

namespace GGJ.Quests
{
    public class QuestManager : MonoBehaviour
    {
        [SerializeField] private QuestInfo testQuest;

        private QuestInfo[] _allQuests;
        private QuestInfo _currentQuest;

        public QuestInfo CurrentQuest
        {
            get => _currentQuest;
            set
            {
                if (_currentQuest != null)
                    if (value.Id <= _currentQuest.Id)
                        throw new ArgumentException($"Quest must increase. Was: {_currentQuest.Id}; Given: {value.Id}");
                

                _currentQuest = value;
                OnQuestChanged(this, value);
            }
        }

        public event EventHandler<QuestInfo> OnQuestChanged = delegate { };
        public event EventHandler<ItemInfo> OnPlayerInventoryUpdated = delegate { };

        private void Start()
        {
            _allQuests = Resources.LoadAll<QuestInfo>("Quests/");
            print($"Loaded {_allQuests.Length}");
            TakeNewQuest(testQuest);
        }

        public void TakeNewQuest(QuestInfo quest)
        {
            CurrentQuest = quest;
        }

        
    }
}