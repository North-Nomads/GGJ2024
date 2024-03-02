using System;
using System.Collections;
using GGJ.Dialogs;
using GGJ.Infrastructure;
using NPC.Settings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NPC.Components
{
    public class DialogSpeaker
    {
        private const float DialogLookingDuration = 2f;
        
        private ICoroutineRunner _coroutineRunner; 
        
        private Dialog _dialog;
        private Vision _vision;
        
        private string[] _randomWalkSpeeches;
        private string[] _knockOutSpeeches;
        
        private float _minWalkSpeechAppearTime;
        private float _maxWalkSpeechAppearTime;
        private float _speechAppearTimer;

        private Coroutine _randomWalkSpeakRoutine;

        public void Initialize(NpcSettings settings, DialogView dialogView, Vision vision, ICoroutineRunner coroutineRunner)
        {
            _dialog = new Dialog();
            _dialog.Initialize(dialogView);

            _vision = vision;

            _randomWalkSpeeches = settings.RandomWalkSpeeches;
            _knockOutSpeeches = settings.KnockOutSpeeches;
            _minWalkSpeechAppearTime = settings.MinWalkSpeechAppearTime;
            _maxWalkSpeechAppearTime = settings.MaxWalkSpeechAppearTime;
            
            _coroutineRunner = coroutineRunner;
        }

        public void Tick()
        {
            ToggleSpeakRoutine();
            UpdateTimers();
        }

        public void OnPlayerCollided()
        {
            _speechAppearTimer = 0f;
            SpeakRandomSpeech(_knockOutSpeeches);
        }

        private void ToggleSpeakRoutine()
        {
            if (_randomWalkSpeakRoutine == null && _vision.PlayerInVisionRadius)
            {
                _randomWalkSpeakRoutine = _coroutineRunner.StartCoroutine(SpeakRandomWalkSpeech());
            }
            else if (_randomWalkSpeakRoutine != null && !_vision.PlayerInVisionRadius)
            {
                _coroutineRunner.StopCoroutine(_randomWalkSpeakRoutine);
                _randomWalkSpeakRoutine = null;
                
                _speechAppearTimer = 0f;
            }
        }

        private void UpdateTimers() => _speechAppearTimer += Time.deltaTime;

        private IEnumerator SpeakRandomWalkSpeech()
        {
            while (true)
            {
                float speechAppearTime = Random.Range(_minWalkSpeechAppearTime, _maxWalkSpeechAppearTime);

                while (_speechAppearTimer < speechAppearTime)
                    yield return null;

                _speechAppearTimer = 0f;
                SpeakRandomSpeech(_randomWalkSpeeches);
            }
        }

        private string GetRandomSpeech(string[] speeches)
        {
            if (speeches.Length == 0)
                return null;

            return speeches[Random.Range(0, speeches.Length)];
        }

        private void SpeakRandomSpeech(string[] speeches)
        {
            _vision.TryLookAtPlayer(DialogLookingDuration);
            _dialog.ShowPhrase(_coroutineRunner, String.Empty, GetRandomSpeech(speeches));
        }
    }
}