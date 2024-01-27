using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuestWindow : MonoBehaviour
{
    [SerializeField] private GameObject questWindow;
    [SerializeField] private TextMeshProUGUI text;

    private QuestTasks[] _quests;
    private GameObject _fishNameNeeded;
    private bool _isOpend;
    private bool _questStatus;
    private int _carpCount;
    private int _fishNeeded;
    private int _questCount;
    
    public void OnWindowInteraction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_isOpend)
            {
                questWindow.SetActive(false);
                _isOpend = false;
            }
            else
            {
                questWindow.SetActive(true);
                _isOpend = true;
            }
        }
    }

    private void Start()
    {
        _carpCount = 0;
        _questCount = 1;
        _isOpend = true;
        _questStatus = false;
        _quests = Resources.LoadAll<QuestTasks>("Quests");
        TakeQuest(_questCount);
    }

    private void Update()
    {
        if (!_questStatus)
        {
            QuestProgress();
        }
        else
        {
            FinishQuest();
        }
    }

    private void QuestProgress()
    {
        if (_fishNeeded > _carpCount)
            text.text = $"Quest {_questCount}: Take {_fishNeeded} carps. \n{_carpCount}/{_fishNeeded}";
        else
            _questStatus = true;
    }

    public void OnCompleQuest(InputAction.CallbackContext context)
    {
        if (context.performed && _questStatus)
        {
            _questCount += 1;
            _questStatus = false;
            TakeQuest(_questCount);
        }
    }

    private void TakeQuest(int questNum)
    {
        _fishNeeded = _quests[questNum - 1].FishCount;
        //_fishNameNeeded = _quests[questNum - 1].FishName;
    }

    private void FinishQuest()
    {
        text.text = $"Quest {_questCount}: complete.";
    }

    public void OnTakingCarp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _carpCount += 1;
        }
    }
}
