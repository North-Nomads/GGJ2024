namespace NPC.StateMachine.States
{
    public sealed class IdleState : BaseState
    {
        public IdleState(NpcStateMachine stateMachine) : base(stateMachine) { }
        
        public override void InitializeTransitions()
        {
            Transitions.AddRange(new[]
            {
                new Transition(StateMachine.GetState<WalkState>(), () => true),
            });
        }
    }
}