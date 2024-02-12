﻿using System;
using TMPro;
using UnityEngine;

namespace GGJ.Dialogs
{
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
    }
}