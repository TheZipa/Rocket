using System;
using System.Collections.Generic;
using Code.Infrastructure.StateMachine.States;
using Code.Services.CoroutineRunner;
using Code.Services.Factories.UIFactory;
using Code.Services.Input;
using Code.Services.SceneLoader;

namespace Code.Infrastructure.StateMachine.GameStateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(ServiceContainer.ServiceContainer container, ICoroutineRunner coroutineRunner)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, container, coroutineRunner),
                [typeof(GameplayState)] = new GameplayState(this, container.Single<ISceneLoader>(),
                    container.Single<IGameFactory>(), container.Single<IUIFactory>(), container.Single<IInputService>()),
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