using System;
using System.Collections.Generic;
using GGJ.Infrastructure.AssetManagement;
using GGJ.Infrastructure.Factories;
using GGJ.Infrastructure.Services;
using GGJ.Infrastructure.Services.Services.PersistentProgress;
using GGJ.Infrastructure.Services.Services.SaveLoad;
using GGJ.Infrastructure.States.Interfaces;

namespace GGJ.Infrastructure.States
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _currentState;

        public GameStateMachine(SceneLoader sceneLoader, AllServices services)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, services),
                [typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<IPersistentProgressService>(), services.Single<ISaveLoadService>()),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, services.Single<IPersistentProgressService>(), services.Single<IGameFactory>(), services.Single<IAssetProvider>()),
                [typeof(GameLoopState)] = new GameLoopState(this),
            };
        }

        public void EnterIn<TState>() where TState : class, IState => 
            SwitchState<TState>().Enter();

        public void EnterIn<TState, TPayload>(TPayload payload) where TState : class, IPayloadState => 
            SwitchState<TState>().Enter(payload);

        private TState SwitchState<TState>() where TState : class, IExitableState
        {
            _currentState?.Exit();
            TState state = GetState<TState>();
            _currentState = state;

            return state;
        }
        
        private TState GetState<TState>() where TState : class, IExitableState => 
            _states[typeof(TState)] as TState;
    }
}