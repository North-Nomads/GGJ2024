using GGJ.Infrastructure;
using NPC.Components;
using NPC.Settings;

namespace NPC.StateMachine.States
{
    public sealed class IdleState : BaseState
    {
        private readonly NpcSettings _settings;
        private readonly RouteProvider _routeProvider;
        
        public IdleState(NpcStateMachine stateMachine, ICoroutineRunner coroutineRunner, NpcSettings settings, RouteProvider routeProvider) : base(stateMachine, coroutineRunner)
        {
            _settings = settings;
            _routeProvider = routeProvider;
        }
        
        public override void InitializeTransitions()
        {
            Transitions.AddRange(new[]
            {
                new Transition(StateMachine.GetState<WalkState>(), () => !_settings.IsShouldStay && _routeProvider.Route != null),
            });
        }
    }
}