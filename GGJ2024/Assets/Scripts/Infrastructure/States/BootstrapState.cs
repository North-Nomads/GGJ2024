using System.ComponentModel;
using GGJ.Infrastructure.AssetManagement;
using GGJ.Infrastructure.Factories;
using GGJ.Infrastructure.Services;
using GGJ.Infrastructure.Services.Services.PersistentProgress;
using GGJ.Infrastructure.Services.Services.SaveLoad;
using GGJ.Infrastructure.States.Interfaces;

namespace GGJ.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine gameStateMachine, AllServices services)
        {
            _gameStateMachine = gameStateMachine;
            _services = services;

            RegisterServices();
        }

        public void Enter()
        {
            _gameStateMachine.EnterIn<LoadProgressState>();
        }

        public void Exit()
        {
            
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>()));
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>()));
        }
    }
}