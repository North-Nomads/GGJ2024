using GGJ.Inventory;
using System;
using GGJ.Data;
using GGJ.Dialogs;
using GGJ.Infrastructure.Services.Services.SaveLoad;
using UnityEngine;
using UnityEngine.Serialization;

namespace GGJ.Quests
{
    public class QuestManager : MonoBehaviour, ISavedProgressWriter
    {
        [FormerlySerializedAs("inventory")] [SerializeField] private PlayerInventory playerInventory;
        [SerializeField] private QuestInfo initialQuest;
        
        private QuestView _questView;

        private QuestInfo _currentQuest;
        private QuestProgressChecker _questProgressChecker;

        public QuestInfo CurrentQuest
        {
            get => _currentQuest;
            set
            {
                if (_currentQuest != null && value != null)
                    if (value.Id <= _currentQuest.Id)
                        throw new ArgumentException($"Quest must increase. Was: {_currentQuest.Id}; Given: {value.Id}");
                
                _currentQuest = value;
                QuestChanged?.Invoke(this, value);
            }
        }

        public QuestInfo LastCompletedQuest { get; private set; }

        public bool IsCurrentQuestCompleted => _questProgressChecker.IsQuestCompleted;

        public event EventHandler<QuestInfo> QuestChanged = delegate { };
        public event EventHandler<QuestInfo> QuestCompleted = delegate { };

        public void Construct(QuestView questView)
        {
            _questView = questView;
            _questProgressChecker = new QuestProgressChecker(this, _questView, playerInventory);
        }

        public void SubmitCurrentQuest(bool needToRemoveItems)
        {
            if (needToRemoveItems && _currentQuest.TargetItem != null)
                playerInventory.TryRemoveItem(_currentQuest.TargetItem, _currentQuest.TargetQuantity);
            
            LastCompletedQuest = CurrentQuest;
            CurrentQuest = null;
        }

        public void Load(PlayerProgress progress)
        {
            if (progress.QuestData.LastCompletedQuest == null)
                LastCompletedQuest = initialQuest;
            else
                LastCompletedQuest = progress.QuestData.LastCompletedQuest;
            
            CurrentQuest = progress.QuestData.CurrentQuest;
        }

        public void Save(PlayerProgress progress)
        {
            progress.QuestData.LastCompletedQuest = LastCompletedQuest;
            progress.QuestData.CurrentQuest = CurrentQuest;
        }

        private void Awake()
        {
            LastCompletedQuest = initialQuest;
        }
    }
}