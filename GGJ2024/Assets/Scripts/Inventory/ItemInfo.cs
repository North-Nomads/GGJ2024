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
        // TODO: Price etc.

        public string Title => title;
        public string Description => description;
        public Sprite Icon => icon;
        public ItemType ItemType => itemType;
    }
}