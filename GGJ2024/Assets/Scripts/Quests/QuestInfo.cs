using System;
using GGJ.Dialogs;
using GGJ.Inventory;
using UnityEngine;
using UnityEngine.Rendering;

namespace GGJ.Quests
{
    [CreateAssetMenu(fileName = "New Quest Info", menuName = "Quests/Quest definition")]
    public class QuestInfo : ScriptableObject, IComparable<QuestInfo>
    {
        [SerializeField] private int id;
        [Tooltip("Leave null if speach")] [SerializeField] private ItemInfo targetItem;
        [Tooltip("Amount of items to bring")] [SerializeField] private int targetQuantity;
        [Tooltip("Whom to bring items")][SerializeField] private QuestGiverInfo recipient;

        [Header("UI")]
        [SerializeField] private string title;
        [SerializeField] private string description;

        [Header("Quest text")]
        [SerializeField] private string questFinishedText;
        [SerializeField] private string[] speeches;

        // Basic info
        public int Id => id;
        public ItemInfo TargetItem => targetItem;
        public int TargetQuantity => targetQuantity;
        public QuestGiverInfo Recipient => recipient;

        // UI information
        public string Title => title;
        public string Description => description;
        
        // Quests speeches
        public string FinishText => questFinishedText;
        public string[] MonologSpeeches => speeches;
        public int CompareTo(QuestInfo other) => Id.CompareTo(other.Id);
    }
}