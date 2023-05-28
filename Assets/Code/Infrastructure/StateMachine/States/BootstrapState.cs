using Code.Infrastructure.StateMachine.GameStateMachine;
using Code.Services.CoroutineRunner;
using Code.Services.Factories.UIFactory;
using Code.Services.Input;
using Code.Services.SceneLoader;
using Code.Services.StaticData;
using Code.Services.StaticData.StaticDataProvider;

namespace Code.Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ICoroutineRunner _coroutineRunner;

        public BootstrapState(IGameStateMachine gameStateMachine, ServiceContainer.ServiceContainer container,
            ICoroutineRunner coroutineRunner)
        {
            _gameStateMachine = gameStateMachine;
            _coroutineRunner = coroutineRunner;

            RegisterServices(container);
        }

        public void Enter() => _gameStateMachine.Enter<GameplayState>();

        public void Exit()
        {
        }
        
        private void RegisterServices(ServiceContainer.ServiceContainer container)
        {
            container.RegisterSingle<IGameStateMachine>(_gameStateMachine);
            container.RegisterSingle<ICoroutineRunner>(_coroutineRunner);
            container.RegisterSingle<IStaticDataProvider>(new StaticDataProvider());
            container.RegisterSingle<ISceneLoader>(new SceneLoader());
            container.RegisterSingle<IInputService>(new InputService(_coroutineRunner));
            
            RegisterStaticData(container);
            RegisterGameFactory(container);
            RegisterUIFactory(container);
        }

        private void RegisterStaticData(ServiceContainer.ServiceContainer container)
        {
            IStaticData staticData = new StaticData(container.Single<IStaticDataProvider>());
            staticData.LoadStaticData();
            container.RegisterSingle<IStaticData>(staticData);
        }

        private void RegisterGameFactory(ServiceContainer.ServiceContainer container) =>
            container.RegisterSingle<IGameFactory>(new GameFactory(container.Single<IStaticData>()));

        private void RegisterUIFactory(ServiceContainer.ServiceContainer container) =>
            container.RegisterSingle<IUIFactory>(new UIFactory(container.Single<IStaticData>()));
    }
}