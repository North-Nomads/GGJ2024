using UnityEngine;
using UnityEngine.UI;

namespace GGJ.UI.Masks
{
    public class SelectingMask : MonoBehaviour
    {
        [Header("GameObjects")]
        [SerializeField] private Image maskImage;
        [SerializeField] private Image border;

        [SerializeField] private Color maskImageColor;
        [SerializeField] private Color borderColor;

        public bool IsSelected => gameObject.activeInHierarchy;

        public void Select() => gameObject.SetActive(true);

        public void Deselect() => gameObject.SetActive(false);
    }
}