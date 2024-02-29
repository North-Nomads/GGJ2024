using System.Collections;
using GGJ.Infrastructure;
using NPC.Components;
using NPC.Settings;
using UnityEngine;

namespace NPC.StateMachine.States
{
    public sealed class WalkState : BaseState
    {
        private readonly RouteProvider _routeProvider;
        private readonly Rigidbody _rigidbody;
        private readonly NpcSettings _settings;

        private Coroutine _walkRoutine;

        public WalkState(NpcStateMachine stateMachine, ICoroutineRunner coroutineRunner, RouteProvider routeProvider, Rigidbody rigidbody, NpcSettings settings) : base(stateMachine, coroutineRunner)
        {
            _routeProvider = routeProvider;
            _rigidbody = rigidbody;
            _settings = settings;

            //routeProvider.DestinationPointReached += StopRouteWalk;
        }

        public override void InitializeTransitions()
        {
            /*Transitions.AddRange(new[]
            {
                new Transition(StateMachine.GetState<IdleState>(), () => true),
            });*/
        }

        public override void Tick()
        {
            if (_routeProvider.Route != null && _walkRoutine == null) 
                _walkRoutine = CoroutineRunner.StartCoroutine(StartRouteWalk());
        }

        public override void Exit() => StopRouteWalk();

        private IEnumerator StartRouteWalk()
        {
            while (true)
            {
                Vector3 nextPoint =
                    _rigidbody.transform.InverseTransformPoint(_routeProvider.CurrentRoutePoint.transform.position) * 
                    (Time.fixedDeltaTime * _settings.MaxWalkSpeed);
                
                _rigidbody.MovePosition(_rigidbody.position + nextPoint);
                
                yield return new WaitForFixedUpdate();
            }
        }

        private void StopRouteWalk()
        {
            if (_walkRoutine != null)
            {
                CoroutineRunner.StopCoroutine(_walkRoutine);
                _walkRoutine = null;
            }
        }
    }
}