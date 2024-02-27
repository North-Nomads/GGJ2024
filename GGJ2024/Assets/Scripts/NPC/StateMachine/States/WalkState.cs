using UnityEngine;

namespace NPC.StateMachine.States
{
    public sealed class WalkState : BaseState
    {
        public WalkState(NpcStateMachine stateMachine) : base(stateMachine) { }

        public override void InitializeTransitions()
        {
            Transitions.AddRange(new[]
            {
                new Transition(StateMachine.GetState<IdleState>(), () => true),
            });
        }
    }
}