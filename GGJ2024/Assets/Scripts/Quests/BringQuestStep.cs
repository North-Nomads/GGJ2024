using System.Linq;
using GGJ.Inventory;
using GGJ.Inventory.CustomEventArgs;
using UnityEngine;

namespace GGJ.Quests
{
    public class BringQuestStep : QuestStep
    {
        [SerializeField] private ItemInfo itemInfo;
        [SerializeField] private int itemCount;

        private PlayerInventory _playerInventory;
        private QuestView _questView;
        private int _currentItemCount;
        
        public ItemInfo ItemInfo => itemInfo;
        public int ItemCount => itemCount;
        public int CurrentItemCount
        {
            get => _currentItemCount;
            private set
            {
                _currentItemCount = value;
                UpdateView();
            }
        }
        public int GoalItemCount => itemCount;

        public override void Initialize(GameObject playerInstance, QuestView questView)
        {
            _questView = questView;
            QuestStepGoalString.Verb = string.Empty;
            QuestStepGoalString.Goal = itemInfo.Title;
            QuestStepGoalString.CurrentCount = "0";
            QuestStepGoalString.GoalCount = $"{itemCount}";
            
            CurrentItemCount = 0;
            Debug.Log(CurrentItemCount);
            Debug.Log(_currentItemCount);
            
            if (playerInstance.TryGetComponent(out PlayerInventory playerInventory))
            {
                _playerInventory = playerInventory;
                foreach (InventorySlot slot in playerInventory.Slots)
                {
                    slot.OnSlotStatusUpdate += OnSlotStatusUpdate;
                    
                    if (slot.ItemInfo != null && slot.ItemInfo.name == itemInfo.name)
                    {
                        CurrentItemCount++;
                    }
                }
            }
        }

        private void UpdateView()
        {
            _questView.UpdateView(QuestStepGoalString.Result);
        }

        private void TryFinishQuestStep()
        {
            if (CurrentItemCount >= itemCount)
                IsFinished = true;
            else
                IsFinished = false;
            
            CompleteQuestStep();
        }
        
        private void OnSlotStatusUpdate(object sender, InventoryEventArgs args)
        {
            CurrentItemCount = _playerInventory.Slots
                .Count(e => e.ItemInfo != null && e.ItemInfo.name == itemInfo.name);
            QuestStepGoalString.CurrentCount = $"{CurrentItemCount}";

            TryFinishQuestStep();
        }
    }
}