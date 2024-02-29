using System;
using TMPro;
using UnityEngine;

namespace GGJ.Dialogs
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(CanvasLookToCamera))]
    public class DialogView : MonoBehaviour
    {
        [SerializeField] private TMP_Text titleField;
        [SerializeField] private TMP_Text textField;

        public string Title
        {
            get => titleField.text;
            set => titleField.text = value;
        }

        public string Text
        {
            get => textField.text;
            set => textField.text = value;
        }

        private void OnDisable() => CleanUp();

        private void CleanUp()
        {
            titleField.text = String.Empty;
            textField.text = String.Empty;
        }
    }
}