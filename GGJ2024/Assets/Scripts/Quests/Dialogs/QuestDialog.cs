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

        public void Initialize(GameObject character, DialogView dialogView)
        {
            if (character.TryGetComponent(out DialogInputHandler dialogInputHandler))
                dialogInputHandler.DialogVariantSkipButtonPressed += OnDialogVariantSkip;

            _dialogView = dialogView;
        }
        
        public IEnumerator ShowDialog(ICoroutineRunner coroutineRunner, string title, string[] dialogVariants)
        {
            DialogStarted?.Invoke();

            foreach (string dialogVariant in dialogVariants)
            {
                _dialogRoutine = coroutineRunner.StartCoroutine(WriteDialog(dialogVariant));

                while (_dialogRoutine != null)
                    yield return null;
            }
            _dialogView.Text = String.Empty;
            
            DialogEnded?.Invoke();
        }

        public void ShowPhrase(ICoroutineRunner coroutineRunner, string title, string phrase) => 
            _dialogRoutine = coroutineRunner.StartCoroutine(WriteDialog(phrase));

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
            if (_dialogRoutine != null && _currentDialogVariant != _dialogView.Text)
                _dialogView.Text = _currentDialogVariant;
            else if (_dialogRoutine != null && _currentDialogVariant == _dialogView.Text)
                _dialogRoutine = null;
        }
    }
}