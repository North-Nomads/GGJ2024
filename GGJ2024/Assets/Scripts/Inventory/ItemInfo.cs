using UnityEngine;

namespace GGJ.Inventory
{
    [CreateAssetMenu(fileName = "New Item Info", menuName = "Items/Item definition")]
    public class ItemInfo : ScriptableObject
    {
        [SerializeField] private string title;
        [SerializeField] private string description;
        [SerializeField] private Sprite icon;
        [SerializeField] private ItemType itemType;
        [SerializeField] private RarityType rarityType;

        public string Title => title;
        public string Description => description;
        public Sprite Icon => icon;
        public ItemType ItemType => itemType;
        public RarityType RarityType => rarityType;

        public string GetItemTypeTranslated()
        {
            switch (itemType)
            {
                case ItemType.Fish:
                    return "Рыба";
                case ItemType.Quest:
                    return "Квестовый";
                case ItemType.Other:
                    return "Другое";
                default:
                    return "Неизвестно";
            }
        }

        public string GetRarityTypeTranslated()
        {
            switch (RarityType)
            {
                case RarityType.Poor:
                    return "Мусор";
                case RarityType.Common:
                    return "Обычная";
                case RarityType.Mythical:
                    return "Мифическая";
                case RarityType.Legendary:
                    return "Легендарная";
                case RarityType.Quest:
                    return "Квест";
                default:
                    return "Неизвестно";
            }
        }
    }
}