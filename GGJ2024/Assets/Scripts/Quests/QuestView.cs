using GGJ.Inventory;
using TMPro;
using UnityEngine;

namespace GGJ.Quests
{
    public class QuestView : MonoBehaviour
    {
        [SerializeField] private QuestManager questManager;
        [SerializeField] private PlayerInventory playerInventory;

        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text goalText;
        [SerializeField] private Color finishedQuestFontColor;
        [SerializeField] private Color inProgressQuestFontColor;

        private QuestInfo _quest;
        private int _goal;

        private void Start()
        {
            questManager.OnQuestChanged += SetNewQuest;
            playerInventory.OnPlayerInventoryUpdated += OnPlayerGotNewItem;
        }

        public void SetNewQuest(object sender, QuestInfo quest)
        {
            print("Set new quest");
            _quest = quest;
            _goal = _quest.TargetQuantity;
            title.text = _quest.Title;

            if (quest.TargetQuantity == 0)
            {
                CheckQuestProgress(0);
                return;
            }

            goalText.text = $"{_quest.TargetItem.Title} 0/{_goal}";
            goalText.color = inProgressQuestFontColor;
            CheckQuestProgress(0);
        }

        public void CheckQuestProgress(int newValue)
        {
            print($"Check progress {newValue}");
            if (newValue >= _goal)
            {
                goalText.text = _quest.FinishText;
                goalText.color = finishedQuestFontColor;
            }
            else
            {
                goalText.text = $"{_quest.TargetItem.Title} {newValue}/{_goal}";
            }
        }

        public void OnPlayerGotNewItem(object sender, ItemInfo newItem)
        {
            print($"New item by {sender}");
            if (newItem == _quest.TargetItem)
                CheckQuestProgress(CountAllOccurenciesOfType(newItem));
        }

        private int CountAllOccurenciesOfType(ItemInfo item)
        {
            int c = 0;
            int i = 0;
            foreach (var slot in playerInventory.Slots)
            {
                if (slot.ItemInfo == null) continue;

                print($"{i}: {slot.ItemInfo.name}");
                print($"{ item.name}");

                if (slot.ItemInfo == item)
                    c++;
            }
            print($"Total: {i}");
            return c;
        }
    }
}
