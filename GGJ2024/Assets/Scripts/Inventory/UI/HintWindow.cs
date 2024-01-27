using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GGJ.Inventory.UI
{
    public class HintWindow : MonoBehaviour
    {
        [SerializeField] private Image panelImage;
        [SerializeField] private Image image; 
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text description;

        public Sprite ItemSprite
        {
            get => image.sprite;
            private set => image.sprite = value;
        }

        public string Title
        {
            get => title.text;
            private set => title.text = value;
        }
        
        public string Description
        {
            get => description.text;
            private set => description.text = value;
        }

        public void UpdateValues(Sprite itemIcon, string itemTitle, string itemDescription)
        {
            if (itemIcon == null || itemTitle == string.Empty || itemDescription == string.Empty)
            {
                HideWindowComponents(true);
                return;
            }
            
            ItemSprite = itemIcon;
            Title = itemTitle;
            Description = itemDescription;
            HideWindowComponents(false);
        }

        public void HideWindowComponents(bool state)
        {
            foreach (Transform child in transform)
            {
                if (child == panelImage.transform)
                {
                    continue;
                }
                child.gameObject.SetActive(!state);
            }
        }
    }
}