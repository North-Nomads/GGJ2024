using UnityEngine;

namespace NPC.Settings
{
    [CreateAssetMenu(fileName = "New NPC Settings", menuName = "NPC/NPC definition")]
    public class NpcSettings : ScriptableObject
    {
        [Header("Speed settings")]
        [SerializeField, Min(0f)] private float maxWalkSpeed;
        [SerializeField, Min(0f)] private float maxRunSpeed;
        
        [Header("Speeches")]
        [Tooltip("Appears when player collides with NPC")]
        [SerializeField] private string[] knockOutSpeeches;
        
        [Tooltip("Appears randomly when NPC walks around")]
        [SerializeField] private string[] randomWalkSpeeches;
        [SerializeField] private float minWalkSpeechAppearTime;
        [SerializeField] private float maxWalkSpeechAppearTime;
        
        [SerializeField] private bool canRun;
        [SerializeField] private bool isShouldStay;
        [SerializeField] private float knockOutCooldown;

        public float MaxWalkSpeed => maxWalkSpeed;
        public float MaxRunSpeed => maxRunSpeed;

        public string[] KnockOutSpeeches => knockOutSpeeches;
        public string[] RandomWalkSpeeches => randomWalkSpeeches;
        public float MinWalkSpeechAppearTime => minWalkSpeechAppearTime;
        public float MaxWalkSpeechAppearTime => maxWalkSpeechAppearTime;

        public bool CanRun => canRun;
        public bool IsShouldStay => isShouldStay;
        public float KnockOutCooldown => knockOutCooldown;
    }
}