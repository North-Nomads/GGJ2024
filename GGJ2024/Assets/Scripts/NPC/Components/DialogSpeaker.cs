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
        private ICoroutineRunner coroutineRunner; 
        
        private Dialog _dialog;
        
        private string[] _randomWalkSpeeches;
        private string[] _knockOutSpeeches;

        private float _knockOutSpeechCooldown;
        private float _knockOutSpeechTimer;
        
        private float _minWalkSpeechAppearTime;
        private float _maxWalkSpeechAppearTime;
        private float _speechAppearTimer;

        private Coroutine _randomWalkSpeakRoutine;

        public void Initialize(NpcSettings settings, DialogView dialogView, WalkableNpc npc)
        {
            _dialog = new Dialog();
            _dialog.Initialize(dialogView);

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
            if (_randomWalkSpeakRoutine == null)
                _randomWalkSpeakRoutine = coroutineRunner.StartCoroutine(SpeakRandomWalkSpeech());
            
            _knockOutSpeechTimer += Time.deltaTime;
        }

        private IEnumerator SpeakRandomWalkSpeech()
        {
            while (true)
            {
                float speechAppearTime = Random.Range(_minWalkSpeechAppearTime, _maxWalkSpeechAppearTime);

                while (_speechAppearTimer < speechAppearTime)
                    yield return null;
                
                SpeakRandomSpeech(_randomWalkSpeeches);
            }
        }

        private string GetRandomSpeech(string[] speeches)
        {
            if (speeches.Length == 0)
                return null;

            return speeches[Random.Range(0, speeches.Length)];
        }

        private void OnPlayerCollided()
        {
            if (_knockOutSpeechTimer < _knockOutSpeechCooldown) return;
            
            _knockOutSpeechCooldown = 0f;
            _speechAppearTimer = 0f;
            SpeakRandomSpeech(_knockOutSpeeches);
        }

        private void SpeakRandomSpeech(string[] speeches) =>
            coroutineRunner.StartCoroutine(
                _dialog.ShowPhrase(coroutineRunner, String.Empty, GetRandomSpeech(speeches)));
    }
}