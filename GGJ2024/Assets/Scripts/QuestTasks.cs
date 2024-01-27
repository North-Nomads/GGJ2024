using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quests", order = 51)]
public class QuestTasks : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private string fishName;
    [SerializeField] private int fishCount;
    public int Id => id;
    public string FishName => fishName;
    public int FishCount => fishCount;
}
