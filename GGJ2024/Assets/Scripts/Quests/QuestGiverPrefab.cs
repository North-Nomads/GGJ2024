using System.Linq;
using GGJ.Dialogs;
using GGJ.Infrastructure;
using Logic;
using UnityEngine;

namespace GGJ.Quests
{
    /// <summary>
    /// A class that represents the in-game NPC object (talkable, quest-giver)
    /// </summary>
    public class QuestGiverPrefab : MonoBehaviour, ICoroutineRunner
    {
        private const string PlayerTag = "Player";
        
        private static bool _isDialogContinues;

        [SerializeField] private QuestGiverInfo questGiverInfo;
        [SerializeField] private DialogView dialogView;
        [SerializeField] private Canvas talkHint;
        [SerializeField] private TriggerObserver triggerObserver;

        private QuestManager _questManager;
        private QuestInfo _currentQuest;
        private QuestDialog _questDialog;

        private bool _isPlayerInTalkRadius;

        private bool IsPlayerAlreadyHaveQuest => _questManager.CurrentQuest != null;
        private bool HaveAvailableQuests => questGiverInfo.Quests.Any(quest => quest.Id > _questManager.LastCompletedQuest.Id);
        private bool IsQuestGiverRecipient => IsPlayerAlreadyHaveQuest && _questManager.CurrentQuest.Recipient == questGiverInfo;
        private bool IsPlayerCompletedQuest => _questManager.IsCurrentQuestCompleted && questGiverInfo.GetQuestWithID(_questManager.CurrentQuest.Id);
        private bool IsPlayerInTalkRadius
        {
            set
            {
                _isPlayerInTalkRadius = value;
                talkHint.gameObject.SetActive(value);
            }
        }

        private bool IsDialogContinues
        {
            set
            {
                _isDialogContinues = value;
                talkHint.gameObject.SetActive(!value);
            }
        }

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
            _questDialog.PhraseStarted += OnPhraseStarted;
            _questDialog.PhraseEnded += OnPhraseEnded;
        }

        private void OnEnable()
        {
            triggerObserver.TriggerEntered += TriggerEntered;
            triggerObserver.TriggerExited += TriggerExited;
        }

        private void OnDisable()
        {
            triggerObserver.TriggerEntered -= TriggerEntered;
            triggerObserver.TriggerExited -= TriggerExited;
        }

        private void OnDialogStartButtonPressed()
        {
            if (!_isPlayerInTalkRadius || _isDialogContinues) return;
            
            if (IsPlayerCompletedQuest && IsQuestGiverRecipient)
                CompleteQuest();
            else if (IsPlayerAlreadyHaveQuest || !HaveAvailableQuests)
                ShowBusyPhrase();
            else
                StartQuest();
        }

        private void ShowBusyPhrase() =>
            StartCoroutine(_questDialog.ShowPhraseRoutine(this,
                questGiverInfo.name, questGiverInfo.BusyPhrase));

        private void ShowQuestDialog() => 
            StartCoroutine(_questDialog.ShowDialog(this, 
                questGiverInfo.name, _currentQuest.MonologSpeeches));

        private void ShowCompletedPhrase() =>
            StartCoroutine(_questDialog.ShowPhraseRoutine(this,
                questGiverInfo.name, _questManager.CurrentQuest.QuestGiverFinishText));

        private void StartQuest()
        {
            bool isNextQuestGot = 
                questGiverInfo.TryGetNextQuest(_questManager.LastCompletedQuest, out _currentQuest);
            
            if (isNextQuestGot)
                ShowQuestDialog();
            else
                ShowBusyPhrase();
        }

        private void CompleteQuest()
        {
            ShowCompletedPhrase();
            
            _questManager.SubmitCurrentQuest(_questManager.CurrentQuest.NeedToRemoveItems);
            
            _currentQuest = null;
        }

        private void OnQuestDialogStarted() => IsDialogContinues = true;

        private void OnQuestDialogEnded()
        {
            IsDialogContinues = false;
            _questManager.CurrentQuest = _currentQuest;
        }

        private void OnPhraseStarted() => IsDialogContinues = true;
        private void OnPhraseEnded() => IsDialogContinues = false;

        private void TriggerEntered(Collider other)
        {
            if (other.CompareTag(PlayerTag)) 
                IsPlayerInTalkRadius = true;
        }

        private void TriggerExited(Collider other)
        {
            if (other.CompareTag(PlayerTag))
                IsPlayerInTalkRadius = false;
        }
    }
}