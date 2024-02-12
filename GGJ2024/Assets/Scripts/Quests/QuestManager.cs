using GGJ.Inventory;
using System;
using System.Collections;
using GGJ.Data;
using GGJ.Dialogs;
using GGJ.Infrastructure.Services.Services.SaveLoad;
using UnityEngine;
using UnityEngine.Serialization;

namespace GGJ.Quests
{
    public class QuestManager : MonoBehaviour, ISavedProgressWriter
    {
        [SerializeField] private DialogInputHandler dialogInputHandler;
        [FormerlySerializedAs("inventory")] [SerializeField] private PlayerInventory playerInventory;
        [SerializeField] private QuestInfo initialQuest;

        private readonly QuestView _questView;

        private QuestInfo _currentQuest;
        private QuestProgressChecker _questProgressChecker;

        public QuestInfo CurrentQuest
        {
            get => _currentQuest;
            set
            {
                if (_currentQuest != null)
                    if (value.Id <= _currentQuest.Id)
                        throw new ArgumentException($"Quest must increase. Was: {_currentQuest.Id}; Given: {value.Id}");
                
                _currentQuest = value;
                QuestChanged?.Invoke(this, value);
            }
        }

        public QuestInfo LastCompletedQuest { get; private set; }

        public bool IsCurrentQuestCompleted => _questProgressChecker.IsQuestCompleted;

        public event EventHandler<QuestInfo> QuestChanged = delegate { };

        public void SubmitCurrentQuest() => 
            playerInventory.TryRemoveItem(_currentQuest.TargetItem, _currentQuest.TargetQuantity);

        public void Load(PlayerProgress progress)
        {
            
        }

        public void Save(PlayerProgress progress)
        {
            
        }

        private void Awake()
        {
            LastCompletedQuest = initialQuest;
            _questProgressChecker = new QuestProgressChecker(this, _questView, playerInventory);
        }
    }
}