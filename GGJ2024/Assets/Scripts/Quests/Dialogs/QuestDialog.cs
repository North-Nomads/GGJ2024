using System;
using System.Collections;
using System.ComponentModel;
using GGJ.Infrastructure;
using UnityEngine;

namespace GGJ.Dialogs
{
    public class QuestDialog : Dialog
    {
        public event Action DialogStarted;
        public event Action DialogEnded;
        
        public new void Initialize(DialogView dialogView) => throw new NotSupportedException();

        public void Initialize(GameObject character, DialogView dialogView)
        {
            base.Initialize(dialogView);
            
            if (character.TryGetComponent(out DialogInputHandler dialogInputHandler))
                dialogInputHandler.DialogVariantSkipButtonPressed += OnDialogVariantSkip;
        }
        
        public IEnumerator ShowDialog(ICoroutineRunner coroutineRunner, string title, string[] dialogVariants)
        {
            DialogStarted?.Invoke();
            
            DialogView.gameObject.SetActive(true);
            DialogView.Title = title;

            foreach (string dialogVariant in dialogVariants)
            {
                DialogRoutine = coroutineRunner.StartCoroutine(WriteDialog(dialogVariant));

                while (DialogRoutine != null)
                    yield return null;
            }
            DialogView.gameObject.SetActive(false);
            
            DialogEnded?.Invoke();
        }

        private void OnDialogVariantSkip()
        {
            if (DialogRoutine == null) return;
            
            if (CurrentDialogVariant != DialogView.Text)
                DialogView.Text = CurrentDialogVariant;
            else if (CurrentDialogVariant == DialogView.Text)
                DialogRoutine = null;
        }
    }
}