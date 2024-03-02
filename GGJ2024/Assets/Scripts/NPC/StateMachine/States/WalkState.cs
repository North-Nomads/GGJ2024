using System;
using System.Collections;
using GGJ.Infrastructure;
using Logic;
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
        private readonly AnimatorAgent _animatorAgent;
        private readonly NpcSettings _settings;

        private Coroutine _walkRoutine;

        public bool IsNeedToRun => _settings.CanRun && Random.Range(0f, 1f) >= 0.5f;

        public WalkState(NpcStateMachine stateMachine, ICoroutineRunner coroutineRunner, RouteProvider routeProvider, Rigidbody rigidbody, NavMeshAgent navMeshAgent, AnimatorAgent animatorAgent, NpcSettings settings) : base(stateMachine, coroutineRunner)
        {
            _routeProvider = routeProvider;
            _rigidbody = rigidbody;
            _navMeshAgent = navMeshAgent;
            _animatorAgent = animatorAgent;
            _settings = settings;

            routeProvider.DestinationPointReached += StopRouteWalk;
            routeProvider.CurrentPointChanged += OnCurrentPointChanged;
        }

        public override void InitializeTransitions()
        {
            Transitions.AddRange(new[]
            {
                new Transition(StateMachine.GetState<IdleState>(), () => _animatorAgent.State == AnimatorState.KnockOut),
            });
        }

        public override void Tick()
        {
            if (_routeProvider.Route != null && _walkRoutine == null) 
                _walkRoutine = CoroutineRunner.StartCoroutine(StartRouteWalk());
            
            base.Tick();
        }

        public override void Exit()
        {
            base.Exit();
            StopRouteWalk(this, null);
        }

        private void OnCurrentPointChanged()
        {
            _navMeshAgent.speed = IsNeedToRun ? _settings.MaxRunSpeed : _settings.MaxWalkSpeed;
        }
        
        private IEnumerator StartRouteWalk()
        {
            _navMeshAgent.isStopped = false;
            
            while (true)
            {
                if (_navMeshAgent.velocity.sqrMagnitude > Mathf.Epsilon) 
                    _rigidbody.transform.rotation = Quaternion.LookRotation(_navMeshAgent.velocity.normalized);
                
                _navMeshAgent.destination = _routeProvider.CurrentRoutePoint.transform.position;
                
                yield return new WaitForEndOfFrame();
            }
        }

        private void StopRouteWalk(object sender, EventArgs args)
        {
            if (_walkRoutine != null)
            {
                _navMeshAgent.isStopped = true;
                CoroutineRunner.StopCoroutine(_walkRoutine);
                _walkRoutine = null;
            }
        }
    }
}