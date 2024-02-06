namespace GGJ.Infrastructure.States.Interfaces
{
    /// <summary>
    /// Represents a state that can to enter
    /// </summary>
    public interface IState : IExitableState
    {
        /// <summary>
        /// It should be invoked when state entered from <see cref="GameStateMachine"/>>
        /// </summary>
        void Enter();
    }
}