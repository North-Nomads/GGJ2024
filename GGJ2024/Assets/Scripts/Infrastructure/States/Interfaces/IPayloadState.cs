namespace GGJ.Infrastructure.States.Interfaces
{
    /// <summary>
    /// Represents a state that have a parameter that given in <see cref="Enter{TPayload}"/> method
    /// </summary>
    public interface IPayloadState : IExitableState
    {
        /// <summary>
        /// It should be invoked when state entered from <see cref="GameStateMachine"/>>
        /// </summary>
        /// <param name="payload">Parameter that can be used in</param>
        /// <typeparam name="TPayload"></typeparam>
        void Enter<TPayload>(TPayload payload);
    }
}