using System;
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
        public bool IsQuestCompleted => CurrentQuest != null && _currentValue >= CurrentQuest.TargetQuantity;

        public QuestProgressChecker(QuestManager questManager, QuestView questView, PlayerInventory playerInventory)
        {
            _questView = questView;
            _playerInventory = playerInventory;

            questManager.QuestChanged += SetNewQuest;
            playerInventory.OnPlayerInventoryUpdated += OnPlayerGotNewItem;
        }

        private void OnPlayerGotNewItem(object sender, ItemInfo itemInfo)
        {
            if (_currentQuest is null) return;
            
            if (itemInfo is null || itemInfo == _currentQuest.TargetItem)
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
                _questView.GoalText = $"{_currentQuest.TargetItem.Title} {_currentValue}/{_currentQuest.TargetQuantity}";
            }
        }

        private void SetNewQuest(object sender, QuestInfo quest)
        {
            _currentQuest = quest;

            if (_currentQuest == null)
            {
                _questView.gameObject.SetActive(false);
                return;
            }
            
            _questView.gameObject.SetActive(true);
            _questView.GoalText = _currentQuest.TargetQuantity.ToString();
            _questView.Title = _currentQuest.Title;

            if (quest.TargetQuantity == 0)
            {
                CheckQuestProgress(0);
                return;
            }
            
            _questView.GoalText = $"{_currentQuest.TargetItem.Title} {_currentValue}/{_currentQuest.TargetQuantity}";
            _questView.SetGoalTextColor(_questView.InProgressQuestFontColor);
            CheckQuestProgress(CountAllOccurenciesOfType(_currentQuest.TargetItem));
        }

        private int CountAllOccurenciesOfType(ItemInfo item)
        {
            int correctItemCount = 0;
            foreach (var slot in _playerInventory.Slots)
            {
                if (slot.ItemInfo == null) 
                    continue;

                if (slot.ItemInfo.name == item.name)
                    correctItemCount++;
                
                Debug.Log($"{item.name}");
            }

            return correctItemCount;
        }
    }
}