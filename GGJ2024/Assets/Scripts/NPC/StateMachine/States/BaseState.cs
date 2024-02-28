using System.Collections.Generic;
using GGJ.Infrastructure;
using UnityEngine;

namespace NPC.StateMachine.States
{
    public abstract class BaseState : IState
    {
        protected readonly List<Transition> Transitions = new();
        protected readonly NpcStateMachine StateMachine;
        protected readonly ICoroutineStopper CoroutineStopper;

        protected BaseState(NpcStateMachine stateMachine, ICoroutineStopper coroutineStopper)
        {
            StateMachine = stateMachine;
            CoroutineStopper = coroutineStopper;
        }

        public virtual void Enter() => Debug.Log($"{this.GetType()} entered");

        public virtual void Exit() => Debug.Log($"{this.GetType()} exited");

        public virtual void Tick()
        {
            foreach (Transition transition in Transitions)
                if (transition.IsAnyConditionCompleted)
                    StateMachine.EnterIn(transition.To);
        }

        public virtual void FixedTick() => Debug.Log($"{this.GetType()} Fixed Ticked");

        public abstract void InitializeTransitions();
    }
}