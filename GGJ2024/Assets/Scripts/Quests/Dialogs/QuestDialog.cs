using System;
using System.Collections;
using GGJ.Infrastructure;
using UnityEngine;

namespace GGJ.Dialogs
{
    public class QuestDialog : IDialog
    {
        private const float DialogWriteRate = 0.07f;
        
        private Coroutine _dialogRoutine;
        private DialogView _dialogView;
        private string _currentDialogVariant;
        
        public event Action DialogStarted;
        public event Action DialogEnded;
        public event Action PhraseStarted;
        public event Action PhraseEnded;

        public void Initialize(GameObject character, DialogView dialogView)
        {
            if (character.TryGetComponent(out DialogInputHandler dialogInputHandler))
                dialogInputHandler.DialogVariantSkipButtonPressed += OnDialogVariantSkip;

            _dialogView = dialogView;
            _dialogView.Initialize(this);
        }
        
        public IEnumerator ShowDialog(ICoroutineRunner coroutineRunner, string title, string[] dialogVariants)
        {
            DialogStarted?.Invoke();
            
            _dialogView.Title = title;

            foreach (string dialogVariant in dialogVariants)
            {
                _dialogRoutine = coroutineRunner.StartCoroutine(WriteDialog(dialogVariant));

                while (_dialogRoutine != null)
                    yield return null;
            }
            _dialogView.Text = String.Empty;
            _dialogView.Title = String.Empty;
            
            DialogEnded?.Invoke();
        }

        public IEnumerator ShowPhrase(ICoroutineRunner coroutineRunner, string title, string phrase)
        {
            PhraseStarted?.Invoke();
            
            _dialogView.gameObject.SetActive(true);
            _dialogView.Title = title;
            
            _dialogRoutine = coroutineRunner.StartCoroutine(WriteDialog(phrase));

            while (_dialogRoutine != null)
                yield return null;
            
            _dialogView.gameObject.SetActive(false);
            
            PhraseEnded?.Invoke();
        }

        private IEnumerator WriteDialog(string text)
        {
            _dialogView.Text = String.Empty;
            _currentDialogVariant = text;
            
            foreach (char textChar in text)
            {
                if (_dialogView.Text == text)
                    break;
                
                _dialogView.Text += textChar;
                yield return new WaitForSeconds(DialogWriteRate);
            }

            _dialogView.Text = text;
        }

        private void OnDialogVariantSkip()
        {
            if (_dialogRoutine == null) return;
            
            if (_currentDialogVariant != _dialogView.Text)
                _dialogView.Text = _currentDialogVariant;
            else if (_currentDialogVariant == _dialogView.Text)
                _dialogRoutine = null;
        }
    }
}