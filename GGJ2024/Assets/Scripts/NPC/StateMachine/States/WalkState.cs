using System;
using System.Collections;
using GGJ.Infrastructure;
using NPC.Components;
using NPC.Settings;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace NPC.StateMachine.States
{
    public sealed class WalkState : BaseState
    {
        private readonly RouteProvider _routeProvider;
        private readonly Rigidbody _rigidbody;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly NpcSettings _settings;

        private Coroutine _walkRoutine;

        public bool IsNeedToRun => _settings.CanRun && Random.Range(0f, 1f) >= 0.5f;

        public WalkState(NpcStateMachine stateMachine, ICoroutineRunner coroutineRunner, RouteProvider routeProvider, Rigidbody rigidbody, NavMeshAgent navMeshAgent, NpcSettings settings) : base(stateMachine, coroutineRunner)
        {
            _routeProvider = routeProvider;
            _rigidbody = rigidbody;
            _navMeshAgent = navMeshAgent;
            _settings = settings;

            routeProvider.DestinationPointReached += StopRouteWalk;
            routeProvider.CurrentPointChanged += OnCurrentPointChanged;
        }

        public override void InitializeTransitions()
        {
            /*Transitions.AddRange(new[]
            {
                new Transition(StateMachine.GetState<IdleState>(), () => ),
            });*/
        }

        public override void Tick()
        {
            if (_routeProvider.Route != null && _walkRoutine == null) 
                _walkRoutine = CoroutineRunner.StartCoroutine(StartRouteWalk());
        }

        public override void Exit() => StopRouteWalk(this, null);

        private void OnCurrentPointChanged()
        {
            _navMeshAgent.speed = IsNeedToRun ? _settings.MaxRunSpeed : _settings.MaxWalkSpeed;
            _navMeshAgent.destination = _routeProvider.CurrentRoutePoint.transform.position;
        }
        
        private IEnumerator StartRouteWalk()
        {
            while (true)
            {
                if (_navMeshAgent.velocity.sqrMagnitude > Mathf.Epsilon) 
                    _rigidbody.transform.rotation = Quaternion.LookRotation(_navMeshAgent.velocity.normalized);
                
                yield return new WaitForEndOfFrame();
            }
        }

        private void StopRouteWalk(object sender, EventArgs args)
        {
            if (_walkRoutine != null)
            {
                CoroutineRunner.StopCoroutine(_walkRoutine);
                _walkRoutine = null;
            }
        }
    }
}