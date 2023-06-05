using Code.Core.Camera;
using Code.Core.Environment;
using Code.Core.Rocket;
using Code.Core.UI;
using Code.Infrastructure.StateMachine.GameStateMachine;
using Code.Services.Factories.GameFactory;
using Code.Services.Factories.UIFactory;
using Code.Services.Input;
using Code.Services.SceneLoader;
using Code.Services.StaticData;
using UnityEngine;

namespace Code.Infrastructure.StateMachine.States
{
    public class GameplayState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly IInputService _inputService;
        private readonly PermanentLevelSystem _permanentLevel;
        private readonly RocketInputHandler _rocketInputHandler;

        private const string GameScene = "Game";
        private GameOverWindow _gameOverWindow;
        private LevelCamera _levelCamera;
        private Rocket _rocket;

        public GameplayState(IGameStateMachine stateMachine, ISceneLoader sceneLoader, IGameFactory gameFactory,
            IUIFactory uiFactory, IInputService inputService, IStaticData staticData)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _inputService = inputService;
            _permanentLevel = new PermanentLevelSystem(gameFactory);
            _rocketInputHandler = new RocketInputHandler(inputService, staticData);
        }

        public void Enter() => _sceneLoader.LoadScene(GameScene, PrepareGame);

        public void Exit()
        {
            _rocketInputHandler.Dispose();
            _permanentLevel.Dispose();
            _rocket.OnExplode -= DefineGameOver;
            _gameOverWindow.OnRetryClick -= RetryGame;
        }

        private void PrepareGame()
        {
            CreateUI();
            CreateRocket();
            PreparePermanentLevel();
            PrepareLevelCamera();
            _rocketInputHandler.ConnectRocketInput(_rocket);
        }

        private void PreparePermanentLevel()
        {
            _permanentLevel.SetStarterParts();
            _permanentLevel.SetRocketView(_rocket);
        }

        private void CreateRocket()
        {
            _rocket = _gameFactory.CreateRocket();
            _rocket.OnExplode += DefineGameOver;
        }

        private void CreateUI()
        {
            Transform canvas = _uiFactory.CreateRootCanvas().transform;
            _gameOverWindow = _uiFactory.CreateGameOverWindow(canvas);
            _gameOverWindow.OnRetryClick += RetryGame;
        }

        private void PrepareLevelCamera()
        {
            _levelCamera = _gameFactory.CreateLevelCamera(Camera.main, _rocket);
            _levelCamera.EnableRocketTracking();
        }

        private void DefineGameOver()
        {
            _gameOverWindow.Show();
            _levelCamera.DisableRocketTracking();
            _inputService.Disable();
        }

        private void RetryGame()
        {
            _gameOverWindow.Hide();
            _stateMachine.Enter<GameplayState>();
        }
    }
}