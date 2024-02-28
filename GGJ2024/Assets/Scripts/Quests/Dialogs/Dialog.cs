using System;
using System.Collections;
using GGJ.Infrastructure;
using UnityEngine;

namespace GGJ.Dialogs
{
    public class Dialog
    {
        private const float DialogWriteRate = 0.07f;
        
        protected Coroutine DialogRoutine;
        protected DialogView DialogView;
        
        protected string CurrentDialogVariant;
        
        public event Action PhraseStarted;
        public event Action PhraseEnded;
        
        public void Initialize(DialogView dialogView) => DialogView = dialogView;

        public IEnumerator ShowPhrase(ICoroutineRunner coroutineRunner, string title, string phrase)
        {
            PhraseStarted?.Invoke();
            
            DialogView.gameObject.SetActive(true);
            DialogView.Title = title;
            
            DialogRoutine = coroutineRunner.StartCoroutine(WriteDialog(phrase));

            while (DialogRoutine != null)
                yield return null;
            
            DialogView.gameObject.SetActive(false);
            
            PhraseEnded?.Invoke();
        }

        protected IEnumerator WriteDialog(string text)
        {
            DialogView.Text = String.Empty;
            CurrentDialogVariant = text;
            
            foreach (char textChar in text)
            {
                if (DialogView.Text == text)
                    break;
                
                DialogView.Text += textChar;
                yield return new WaitForSeconds(DialogWriteRate);
            }

            DialogView.Text = text;
        }
    }
}