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
        
        private ICoroutineRunner coroutineRunner; 
        
        private Dialog _dialog;
        private Vision _vision;
        
        private string[] _randomWalkSpeeches;
        private string[] _knockOutSpeeches;

        private float _knockOutSpeechCooldown;
        private float _knockOutSpeechTimer;
        
        private float _minWalkSpeechAppearTime;
        private float _maxWalkSpeechAppearTime;
        private float _speechAppearTimer;

        private Coroutine _randomWalkSpeakRoutine;

        public void Initialize(NpcSettings settings, DialogView dialogView, Vision vision, WalkableNpc npc)
        {
            _dialog = new Dialog();
            _dialog.Initialize(dialogView);

            _vision = vision;

            _randomWalkSpeeches = settings.RandomWalkSpeeches;
            _knockOutSpeeches = settings.KnockOutSpeeches;
            _knockOutSpeechCooldown = settings.KnockOutSpeechCooldown;
            _minWalkSpeechAppearTime = settings.MinWalkSpeechAppearTime;
            _maxWalkSpeechAppearTime = settings.MaxWalkSpeechAppearTime;

            npc.PlayerCollided += OnPlayerCollided;
            coroutineRunner = npc;
        }

        public void Tick()
        {
            ToggleSpeakRoutine();
            UpdateTimers();
        }

        private void ToggleSpeakRoutine()
        {
            if (_randomWalkSpeakRoutine == null && _vision.PlayerInVisionRadius)
            {
                _randomWalkSpeakRoutine = coroutineRunner.StartCoroutine(SpeakRandomWalkSpeech());
            }
            else if (_randomWalkSpeakRoutine != null && !_vision.PlayerInVisionRadius)
            {
                coroutineRunner.StopCoroutine(_randomWalkSpeakRoutine);
                _randomWalkSpeakRoutine = null;
                
                _speechAppearTimer = 0f;
            }
        }

        private void UpdateTimers()
        {
            _knockOutSpeechTimer += Time.deltaTime;
            _speechAppearTimer += Time.deltaTime;
        }

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

        private void OnPlayerCollided()
        {
            if (_knockOutSpeechTimer < _knockOutSpeechCooldown) return;
            
            _knockOutSpeechCooldown = 0f;
            _speechAppearTimer = 0f;
            SpeakRandomSpeech(_knockOutSpeeches);
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
            _dialog.ShowPhrase(coroutineRunner, String.Empty, GetRandomSpeech(speeches));
        }
    }
}