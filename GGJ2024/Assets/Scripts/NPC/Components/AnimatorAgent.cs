using System;
using Logic;
using UnityEngine;
using UnityEngine.AI;

namespace NPC.Components
{
    public class AnimatorAgent : MonoBehaviour, IAnimationStateReader
    {
        private static readonly int WalkHash = Animator.StringToHash("IsWalking");
        private static readonly int KnockOutHash = Animator.StringToHash("IsKnockedOut");
        private static readonly int StandUpHash = Animator.StringToHash("IsStandingUp");
        private static readonly int SpeakHash = Animator.StringToHash("IsSpeaking");

        private static readonly int IdleStateHash = Animator.StringToHash("Idle");
        private static readonly int WalkingStateHash = Animator.StringToHash("Walking");
        private static readonly int KnockOutStateHash = Animator.StringToHash("KnockOut");
        
        [SerializeField] private Animator animator;
        [SerializeField] private NavMeshAgent navMeshAgent;
        
        public AnimatorState State { get; private set; }

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public void PlayKnockOut() => animator.SetTrigger(KnockOutHash);

        public void PlayStandUp() => animator.SetTrigger(StandUpHash);

        public void PlaySpeak() => animator.SetTrigger(SpeakHash);

        private void Update()
        {
            Debug.Log(State);
            animator.SetBool(WalkHash, navMeshAgent.velocity.magnitude / navMeshAgent.speed > 0);
        }

        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }

        public void ExitedState(int stateHash) =>
            StateExited?.Invoke(StateFor(stateHash));
    
        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;
            if (stateHash == IdleStateHash)
                state = AnimatorState.Idle;
            else if (stateHash == WalkingStateHash)
                state = AnimatorState.Walking;
            else if (stateHash == KnockOutStateHash)
                state = AnimatorState.KnockOut;
            else
                state = AnimatorState.Unknown;
      
            return state;
        }
    }
}