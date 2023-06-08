using Code.Infrastructure.StateMachine.GameStateMachine;
using Code.Infrastructure.StateMachine.States;
using Code.Services.CoroutineRunner;
using Code.Services.EntityContainer;
using Code.Services.LoadingScreen;
using UnityEngine;

namespace Code.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingScreen _loadingScreen;
        private GameStateMachine _gameStateMachine;

        private void Awake()
        {
            _gameStateMachine = new GameStateMachine(ServiceContainer.ServiceContainer.Container, this,
                _loadingScreen);
            _gameStateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }

        private void OnDestroy() => ServiceContainer.ServiceContainer.Container.Single<IEntityContainer>().Dispose();
    }
}