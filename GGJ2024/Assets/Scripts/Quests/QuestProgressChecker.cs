using GGJ.Inventory;
using UnityEngine;

namespace GGJ.Quests
{
    public class QuestProgressChecker
    {
        private readonly QuestView _questView;
        private readonly PlayerInventory _playerInventory;

        private QuestInfo _currentQuest;
        private int _currentValue;
        private int _goalValue;

        public QuestInfo CurrentQuest => _currentQuest;
        public bool IsQuestCompleted => CurrentQuest != null && _currentValue >= _goalValue;

        public QuestProgressChecker(QuestManager questManager, QuestView questView, PlayerInventory playerInventory)
        {
            _questView = questView;
            _playerInventory = playerInventory;

            //questManager.QuestChanged += SetNewQuest;
            playerInventory.OnPlayerInventoryUpdated += OnPlayerGotNewItem;
        }

        private void OnPlayerGotNewItem(object sender, ItemInfo itemInfo)
        {
            if (itemInfo == _currentQuest.TargetItem || itemInfo is null)
                CheckQuestProgress(CountAllOccurenciesOfType(itemInfo));
        }

        private void CheckQuestProgress(int newValue)
        {
            _currentValue = newValue;
            
            if (IsQuestCompleted)
            {
                _questView.GoalText = _currentQuest.FinishText;
                _questView.SetGoalTextColor(_questView.FinishedQuestFontColor);
            }
            else
            {
                _questView.GoalText = $"{_currentQuest.TargetItem.Title} {_currentValue}/{_goalValue}";
            }
        }

        private void SetNewQuest(object sender, QuestInfo quest)
        {
            Debug.Log($"Set new quest {quest.Title}");
            _currentQuest = quest;
            _questView.GoalText = _currentQuest.TargetQuantity.ToString();
            _questView.Title = _currentQuest.Title;

            if (quest.TargetQuantity == 0)
            {
                CheckQuestProgress(0);
                return;
            }

            _questView.GoalText = $"{_currentQuest.TargetItem.Title} {_currentValue}/{_goalValue}";
            _questView.SetGoalTextColor(_questView.InProgressQuestFontColor);
            CheckQuestProgress(0);
        }

        private int CountAllOccurenciesOfType(ItemInfo item)
        {
            int correctItemCount = 0;
            foreach (var slot in _playerInventory.Slots)
            {
                if (slot.ItemInfo == null) 
                    continue;

                if (slot.ItemInfo == item)
                    correctItemCount++;
                
                Debug.Log($"{item.name}");
            }

            return correctItemCount;
        }
    }
}