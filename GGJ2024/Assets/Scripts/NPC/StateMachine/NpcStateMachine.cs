﻿using System;
using System.Collections.Generic;
using System.Linq;
using NPC.StateMachine.States;

namespace NPC.StateMachine
{
    public class NpcStateMachine
    {
        private readonly WalkableNpc _walkableNpc;
        private readonly Dictionary<Type, IState> _states;
        
        private IState _currentState;

        public IReadOnlyDictionary<Type, IState> States => _states;
        public WalkableNpc WalkableNpc => _walkableNpc;

        public NpcStateMachine(WalkableNpc walkableNpc)
        {
            _walkableNpc = walkableNpc;

            _states = new Dictionary<Type, IState>()
            {
                [typeof(IdleState)] = new IdleState(this),
                [typeof(WalkState)] = new WalkState(this),
            };
            
            foreach (IState state in _states.Values) 
                state.InitializeTransitions();

            EnterIn<WalkState>();
        }

        public void Tick() => _currentState.Tick();

        public void FixedTick() => _currentState.FixedTick();

        public void EnterIn<TState>(TState state) where TState : IState => 
            SwitchState(state).Enter();

        public void EnterIn<TState>() where TState : IState => 
            SwitchState<TState>().Enter();

        public IState GetState<TState>() where TState : IState => 
            _states[typeof(TState)];

        private IState SwitchState<TState>() where TState : IState
        {
            _currentState?.Exit();
            IState state = GetState<TState>();
            _currentState = state;

            return state;
        }
        
        private IState SwitchState<TState>(TState state) where TState : IState
        {
            if (_states.Values.Contains(state))
            {
                _currentState?.Exit();
                _currentState = state;

                return state;
            }

            throw new KeyNotFoundException($"State not found in dictionary {_states}");
        }
    }
}