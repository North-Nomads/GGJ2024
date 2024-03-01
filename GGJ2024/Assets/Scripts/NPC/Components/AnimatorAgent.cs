using UnityEngine;
using UnityEngine.AI;

namespace NPC.Components
{
    public class AnimatorAgent : MonoBehaviour
    {
        private static readonly int WalkHash = Animator.StringToHash("IsWalking");
        private static readonly int KnockOutHash = Animator.StringToHash("IsKnockedOut");
        private static readonly int StandUpHash = Animator.StringToHash("IsStandingUp");
        private static readonly int SpeakHash = Animator.StringToHash("IsSpeaking");
        
        [SerializeField] private Animator animator;
        [SerializeField] private NavMeshAgent navMeshAgent;

        public void PlayKnockOut() => animator.SetTrigger(KnockOutHash);

        public void PlayStandUp() => animator.SetTrigger(StandUpHash);

        public void PlaySpeak() => animator.SetTrigger(SpeakHash);

        private void Update() => 
            animator.SetBool(WalkHash, navMeshAgent.velocity.magnitude / navMeshAgent.speed > 0);
    }
}