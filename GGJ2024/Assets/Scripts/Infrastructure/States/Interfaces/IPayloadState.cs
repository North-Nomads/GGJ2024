namespace GGJ.Infrastructure.States.Interfaces
{
    public interface IPayloadState : IExitableState
    {
        void Enter<TPayload>(TPayload payload);
    }
}