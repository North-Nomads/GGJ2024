using System.Collections;
using GGJ.Dialogs;
using GGJ.Infrastructure;
using Logic;
using NPC.Components;
using NPC.StateMachine;
using UnityEngine;

namespace NPC
{
    [RequireComponent(typeof(Rigidbody))]
    public class WalkableNpc : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private DialogView dialogView;
        [SerializeField] private AnimatorAgent animatorAgent;
        [SerializeField] private LookAtPlayer lookAtPlayer;
        [SerializeField] private Rigidbody rigidbody;
        
        private NpcStateMachine _stateMachine;
        private RouteProvider _routeProvider;

        public DialogView DialogView => dialogView;
        public AnimatorAgent AnimatorAgent => animatorAgent;
        public LookAtPlayer LookAtPlayer => lookAtPlayer;
        public Rigidbody Rigidbody => rigidbody;
        public RouteProvider RouteProvider => _routeProvider;

        private void Awake()
        {
            _routeProvider = new RouteProvider(this);
            _stateMachine = new NpcStateMachine(this);
        }

        private void Update() => _stateMachine.Tick();

        private void FixedUpdate() => _stateMachine.FixedTick();
    }
}