using System;
using System.Collections;
using System.Linq;
using GGJ.Dialogs;
using GGJ.Infrastructure;
using UnityEngine;
using UnityEngine.Serialization;

namespace GGJ.Quests
{
    /// <summary>
    /// A class that represents the in-game NPC object (talkable, quest-giver)
    /// </summary>
    public class QuestGiverPrefab : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private QuestGiverInfo questGiverInfo;
        [SerializeField] private DialogView dialogView;

        private static bool _isDialogContinues = false;
        
        private QuestManager _questManager;
        private IDialog _questDialog;
        private QuestInfo _currentQuest;

        private bool IsPlayerAlreadyHaveQuest => _questManager.CurrentQuest != null;
        private bool HaveAvailableQuests => questGiverInfo.Quests.Any(quest => quest.Id > _questManager.LastCompletedQuest.Id);
        private bool IsPlayerCompletedQuest => _questManager.IsCurrentQuestCompleted && questGiverInfo.GetQuestWithID(_questManager.CurrentQuest.Id);
        
        public void Construct(GameObject character)
        {
            if (character.TryGetComponent(out QuestManager questManager)) 
                _questManager = questManager;

            if (character.TryGetComponent(out DialogInputHandler dialogInputHandler))
                dialogInputHandler.DialogStartButtonPressed += OnDialogStartButtonPressed;

            _questDialog = new QuestDialog();
            _questDialog.Initialize(character, dialogView);
            _questDialog.DialogStarted += OnQuestDialogStarted;
            _questDialog.DialogEnded += OnQuestDialogEnded;
        }

        private void OnDialogStartButtonPressed()
        {
            if (_isDialogContinues) return;
            
            if (IsPlayerAlreadyHaveQuest || !HaveAvailableQuests)
                ShowBusyPhrase();
            else if (IsPlayerCompletedQuest)
                CompleteQuest();
            else
                StartQuest();
        }

        private void ShowBusyPhrase() => 
            _questDialog.ShowPhrase(this, questGiverInfo.name, questGiverInfo.BusyPhrase);

        private void ShowQuestDialog() => 
            StartCoroutine(_questDialog.ShowDialog(this, questGiverInfo.name, _currentQuest.MonologSpeeches));

        private void ShowCompletedPhrase() => 
            _questDialog.ShowPhrase(this, questGiverInfo.name, _currentQuest.FinishText);

        private void StartQuest()
        {
            if (questGiverInfo.TryGetNextQuest(_questManager.LastCompletedQuest, out _currentQuest))
            {
                _questManager.CurrentQuest = _currentQuest;
                ShowQuestDialog();
            }
            else
                ShowBusyPhrase();
        }

        private void CompleteQuest()
        {
            _questManager.SubmitCurrentQuest();
            
            _currentQuest = null;
            
            ShowCompletedPhrase();
        }

        private void OnQuestDialogStarted() => _isDialogContinues = true;

        private void OnQuestDialogEnded() => _isDialogContinues = false;
    }
}