using Code.Core.Environment;
using Code.Infrastructure.StateMachine.GameStateMachine;
using Code.Services.Factories.GameFactory;
using Code.Services.Factories.UIFactory;
using Code.Services.SceneLoader;
using UnityEngine;

namespace Code.Infrastructure.StateMachine.States
{
    public class LoadGameState : IState
    {
        private const string GameScene = "Game";
        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IUIFactory _uiFactory;
        private readonly IGameFactory _gameFactory;

        public LoadGameState(IGameStateMachine stateMachine, ISceneLoader sceneLoader, 
            IUIFactory uiFactory, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
            _gameFactory = gameFactory;
        }

        public void Enter() => _sceneLoader.LoadScene(GameScene, PrepareGame);

        public void Exit()
        {
        }

        private void PrepareGame()
        {
            CreateUI();
            CreateGameplayComponents();
            _stateMachine.Enter<MenuState>();
        }

        private void CreateUI()
        {
            Transform rootCanvas = _uiFactory.CreateRootCanvas().transform;
            _uiFactory.CreateMainMenu(rootCanvas);
            _uiFactory.CreateGameOverWindow(rootCanvas);
            _uiFactory.CreateMeterCounterView(rootCanvas);
        }

        private void CreateGameplayComponents()
        {
            EnvironmentPart startEnvironmentPart = _gameFactory.CreateStartEnvironmentPart();
            float startYPosition = startEnvironmentPart.BeginPosition.position.y + 0.8f;
            _gameFactory.CreateRocket(startYPosition);
            _gameFactory.CreatePermanentEnvironmentSystem(startEnvironmentPart);
            _gameFactory.CreateLevelCamera(Camera.main, startYPosition);
            _gameFactory.CreateMeterCounterSystem();
        }
    }
}