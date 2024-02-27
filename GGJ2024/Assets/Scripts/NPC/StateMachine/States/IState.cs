namespace NPC.StateMachine.States
{
    public interface IState
    {
        void Enter();
        void Exit();
        void Tick();
        void FixedTick();
        void InitializeTransitions();
    }
}