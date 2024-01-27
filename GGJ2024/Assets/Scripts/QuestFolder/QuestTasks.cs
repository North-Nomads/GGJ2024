using GGJ.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quests", order = 51)]
public class QuestTasks : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private ItemInfo fishName;
    [SerializeField] private int fishCount;
    public int Id => id;
    public ItemInfo FishName => fishName;
    public int FishCount => fishCount;
}
