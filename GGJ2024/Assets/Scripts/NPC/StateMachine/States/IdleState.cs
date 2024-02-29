using GGJ.Infrastructure;

namespace NPC.StateMachine.States
{
    public sealed class IdleState : BaseState
    {
        public IdleState(NpcStateMachine stateMachine, ICoroutineRunner coroutineRunner) : base(stateMachine, coroutineRunner) { }
        
        public override void InitializeTransitions()
        {
            /*Transitions.AddRange(new[]
            {
                new Transition(StateMachine.GetState<WalkState>(), () => true),
            });*/
        }
    }
}