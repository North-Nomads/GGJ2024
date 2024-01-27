using GGJ.Inventory.UI.Slots;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GGJ.UI.Masks
{
    public class HoveringMask: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image hoverImage;
        [SerializeField] private Color hoverColor;
        [SerializeField] private float enterCrossFadeTime;
        [SerializeField] private float exitCrossFadeTime;

        private SlotUI _slot;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_slot.IsEmpty) return;
            
            hoverImage.CrossFadeColor(hoverColor, enterCrossFadeTime, true, true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_slot.IsEmpty) return;
            
            hoverImage.CrossFadeAlpha(0f, exitCrossFadeTime, true);
        }

        public void UpdateFade(float fadeRate)
        {
            hoverImage.CrossFadeAlpha(fadeRate, 0f, true);
        }

        private void Awake()
        {
            hoverImage.CrossFadeAlpha(0f, 0f, true);
            _slot = GetComponentInParent<SlotUI>();
        }

        private void OnDisable()
        {
            UpdateFade(0f);
        }
    }
}