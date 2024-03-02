using System;
using GGJ.Dialogs;
using GGJ.Infrastructure;
using NPC.Components;
using NPC.Settings;
using NPC.StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    [RequireComponent(typeof(Rigidbody))]
    public class WalkableNpc : MonoBehaviour, ICoroutineRunner
    {
        private const string PlayerTag = "Player";

        [SerializeField] private DialogView dialogView;
        [SerializeField] private AnimatorAgent animatorAgent;
        [SerializeField] private Vision vision;
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private NpcSettings settings;
        
        private NpcStateMachine _stateMachine;
        private RouteProvider _routeProvider;
        private DialogSpeaker _dialogSpeaker;

        private float _knockOutTimer;
        
        public AnimatorAgent AnimatorAgent => animatorAgent;
        public NavMeshAgent NavMeshAgent => navMeshAgent;
        public Vision Vision => vision;
        public Rigidbody Rigidbody => rigidbody;
        public NpcSettings Settings => settings;
        public RouteProvider RouteProvider => _routeProvider;

        private void Awake()
        {
            _dialogSpeaker = new DialogSpeaker();
            _dialogSpeaker.Initialize(settings, dialogView, vision, this);
            
            _routeProvider = new RouteProvider(this);
            _stateMachine = new NpcStateMachine(this);
            
            navMeshAgent.isStopped = true;
        }

        private void OnDisable() => dialogView.gameObject.SetActive(false);

        private void Update()
        {
            _stateMachine.Tick();
            _dialogSpeaker.Tick();

            _knockOutTimer += Time.deltaTime;
        }

        private void FixedUpdate() => _stateMachine.FixedTick();

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag(PlayerTag) && _knockOutTimer > settings.KnockOutCooldown)
            {
                _dialogSpeaker.OnPlayerCollided();
                animatorAgent.PlayKnockOut();
                vision.TryLookAtPlayer(4f);
                
                _knockOutTimer = 0f;
            }
        }
    }
}