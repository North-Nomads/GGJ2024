using GGJ.Infrastructure;
using Logic;
using NPC.Components;
using NPC.Settings;

namespace NPC.StateMachine.States
{
    public sealed class IdleState : BaseState
    {
        private readonly NpcSettings _settings;
        private readonly RouteProvider _routeProvider;
        private readonly AnimatorAgent _animatorAgent;

        public IdleState(NpcStateMachine stateMachine, ICoroutineRunner coroutineRunner, NpcSettings settings, RouteProvider routeProvider, AnimatorAgent animatorAgent) : base(stateMachine, coroutineRunner)
        {
            _settings = settings;
            _routeProvider = routeProvider;
            _animatorAgent = animatorAgent;
        }
        
        public override void InitializeTransitions()
        {
            Transitions.AddRange(new[]
            {
                new Transition(StateMachine.GetState<WalkState>(), () => !_settings.IsShouldStay && _routeProvider.Route != null && _animatorAgent.State != AnimatorState.KnockOut),
            });
        }
    }
}