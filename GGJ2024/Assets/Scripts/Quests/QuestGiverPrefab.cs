using System.Linq;
using GGJ.Dialogs;
using GGJ.Infrastructure;
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
        private IDialog _questDialog;

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
            StartCoroutine(_questDialog.ShowDialog(this, questGiverInfo.name, new [] {questGiverInfo.BusyPhrase}, DialogType.Busy));

        private void ShowQuestDialog() => 
            StartCoroutine(_questDialog.ShowDialog(this, questGiverInfo.name, _currentQuest.MonologSpeeches, DialogType.Quest));

        private void ShowCompletedPhrase() => 
            StartCoroutine(_questDialog.ShowDialog(this, questGiverInfo.name, new[] {_questManager.CurrentQuest.QuestGiverFinishText}, DialogType.Complete));

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
            
            _questManager.SubmitCurrentQuest();
            
            _currentQuest = null;
        }

        private void OnQuestDialogStarted(DialogType dialogType)
        {
            _isDialogContinues = true;
            talkHint.gameObject.SetActive(false);
        }


        private void OnQuestDialogEnded(DialogType dialogType)
        {
            _isDialogContinues = false;
            talkHint.gameObject.SetActive(true);
            
            if (dialogType == DialogType.Quest)
                _questManager.CurrentQuest = _currentQuest;
        }

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