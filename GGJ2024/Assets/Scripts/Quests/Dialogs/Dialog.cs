using System;
using System.Collections;
using GGJ.Infrastructure;
using UnityEngine;

namespace GGJ.Dialogs
{
    public class Dialog
    {
        private const float DialogWriteRate = 0.07f;
        private const float PhraseDisappearTime = 4f;
        
        protected Coroutine DialogRoutine;
        protected Coroutine WriteDialogRoutine;
        protected DialogView DialogView;
        
        protected string CurrentDialogVariant;
        
        public event Action PhraseStarted;
        public event Action PhraseEnded;
        
        public void Initialize(DialogView dialogView) => DialogView = dialogView;

        public void ShowPhrase(ICoroutineRunner coroutineRunner, string title, string phrase)
        {
            if (DialogRoutine != null)
            {
                coroutineRunner.StopCoroutine(WriteDialogRoutine);
                coroutineRunner.StopCoroutine(DialogRoutine);
                WriteDialogRoutine = null;
                DialogRoutine = null;
            }
            
            DialogRoutine = coroutineRunner.StartCoroutine(ShowPhraseRoutine(coroutineRunner, title, phrase));
        }

        public IEnumerator ShowPhraseRoutine(ICoroutineRunner coroutineRunner, string title, string phrase)
        {
            PhraseStarted?.Invoke();
            
            DialogView.gameObject.SetActive(true);
            DialogView.Title = title;
            
            WriteDialogRoutine = coroutineRunner.StartCoroutine(WriteDialog(phrase));
            
            float overallPhraseDisappearTime = PhraseDisappearTime + phrase.Length * DialogWriteRate;
            yield return new WaitForSeconds(overallPhraseDisappearTime);
            
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