using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GGJ.Inventory.UI.Slots
{
    public abstract class SlotUI : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] protected Image slotImage;
        [SerializeField] protected List<RectTransform> canvasesToDisableOnEmpty;

        private bool _isEmpty;

        public bool IsEmpty
        {
            get => _isEmpty;
            protected set
            {
                if (_isEmpty != value)
                {
                    foreach (RectTransform canvas in canvasesToDisableOnEmpty)
                    {
                        canvas.gameObject.SetActive(!value);
                    }
                }
                _isEmpty = value;

                if (value)
                {
                    SlotIsEmpty?.Invoke();
                }
            }
        }
        
        protected Sprite SlotIcon
        {
            get => slotImage.sprite;
            set => slotImage.sprite = value;
        }
        
        public event EventHandler SlotSelected;
        public event Action SlotIsEmpty;

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (IsEmpty) return;
            
            SlotSelected?.Invoke(this, EventArgs.Empty);
        }

        private void Awake() => IsEmpty = true;
    }
}