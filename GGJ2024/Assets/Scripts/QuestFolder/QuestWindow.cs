using GGJ.Inventory;
using GGJ.Inventory.CustomEventArgs;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuestWindow : MonoBehaviour
{
    [SerializeField] private GameObject questWindow;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private PlayerInventory playerInventory;

    private QuestTasks[] _quests;
    private ItemInfo _fishNameNeeded;
    private bool _isOpend;
    private bool _questStatus;
    private int _fishCountInInv;
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
        _fishCountInInv = 0;
        _questCount = 1;
        _isOpend = true;
        _questStatus = false;
        _quests = Resources.LoadAll<QuestTasks>("Quests");
        TakeQuest(_questCount);
        foreach (InventorySlot slot in playerInventory.Slots)
        {
            slot.OnSlotStatusUpdate += OnInventoryUpdate;
        }
    }

    private void OnInventoryUpdate(object sender, InventoryEventArgs args)
    {
        _fishCountInInv = playerInventory.Slots.Count(x => x.ItemInfo == _fishNameNeeded);
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
        if (_fishNeeded > _fishCountInInv)
            text.text = $"Quest {_questCount}: Take {_fishNeeded} carps. \n{_fishCountInInv}/{_fishNeeded}";
        else
            _questStatus = true;
    }

    public void OnCompleQuest(InputAction.CallbackContext context)
    {
        if (context.performed && _questStatus)
        {
            for (int i = 0; i < _fishNeeded; i++)
            {
                playerInventory.TryRemoveItem(_fishNameNeeded);
            }
            _questCount ++;
            _questStatus = false;
            TakeQuest(_questCount);
        }
    }

    private void TakeQuest(int questNum)
    {
        _fishNeeded = _quests[questNum - 1].FishCount;
        _fishNameNeeded = _quests[questNum - 1].FishName;
    }

    private void FinishQuest()
    {
        text.text = $"Quest {_questCount}: complete.";
    }
}
