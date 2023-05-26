using Code.Infrastructure.StateMachine.GameStateMachine;
using Code.Infrastructure.StateMachine.States;
using Code.Services.CoroutineRunner;
using UnityEngine;

namespace Code.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private GameStateMachine _gameStateMachine;

        private void Awake()
        {
            _gameStateMachine = new GameStateMachine(ServiceContainer.ServiceContainer.Container, this);
            _gameStateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}