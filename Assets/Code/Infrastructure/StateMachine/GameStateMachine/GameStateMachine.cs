using System;
using System.Collections.Generic;
using Code.Infrastructure.StateMachine.States;
using Code.Services.CoroutineRunner;
using Code.Services.EntityContainer;
using Code.Services.Factories.GameFactory;
using Code.Services.Factories.UIFactory;
using Code.Services.Input;
using Code.Services.LoadingScreen;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Code.Services.SceneLoader;
using Code.Services.StaticData;

namespace Code.Infrastructure.StateMachine.GameStateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(ServiceContainer.ServiceContainer container, ICoroutineRunner coroutineRunner,
            ILoadingScreen loadingScreen)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, container, coroutineRunner, loadingScreen),
                [typeof(LoadProgressState)] = new LoadProgressState(this, 
                    container.Single<IPersistentProgress>(), container.Single<ISaveLoad>()),
                [typeof(LoadGameState)] = new LoadGameState(this, container.Single<ISceneLoader>(),
                        container.Single<IUIFactory>(), container.Single<IGameFactory>()),
                [typeof(MenuState)] = new MenuState(this, container.Single<IEntityContainer>(), loadingScreen),
                [typeof(GameplayState)] = new GameplayState(this, container.Single<IEntityContainer>(),
                    loadingScreen, container.Single<IInputService>(), 
                    container.Single<IPersistentProgress>(), container.Single<IStaticData>()),
            };
        }

        public void Enter<TState>() where TState : class, IState =>
            ChangeState<TState>().Enter();

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload> =>
            ChangeState<TState>().Enter(payload);

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
        
        ~GameStateMachine() => _activeState.Exit();
    }
}