namespace GGJ.Infrastructure.States.Interfaces
{
    /// <summary>
    /// Represents a state that can to exit
    /// </summary>
    public interface IExitableState
    {
        /// <summary>
        /// It should be invoked when state exited from <see cref="GameStateMachine"/>>
        /// </summary>
        void Exit();
    }
}