using UnityEngine;

namespace GGJ.Quests
{
    [CreateAssetMenu(fileName = "New Quest Info", menuName = "Quests/Quest definition")]
    public class QuestInfo : ScriptableObject
    {
        [SerializeField] private int id;
        [SerializeField] private string title;
        [SerializeField] private string description;
    
        public int Id => id;
        public string Title => title;
        public string Description => description;
    }
}