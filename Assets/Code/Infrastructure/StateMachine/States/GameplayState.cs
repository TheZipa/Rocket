using Cinemachine;
using Code.Core.Environment;
using Code.Core.Rocket;
using Code.Core.UI;
using Code.Infrastructure.StateMachine.GameStateMachine;
using Code.Services.Factories.UIFactory;
using Code.Services.Input;
using Code.Services.SceneLoader;
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

        private const string GameScene = "Game";
        private Rocket _rocket;
        private GameOverWindow _gameOverWindow;

        public GameplayState(IGameStateMachine stateMachine, ISceneLoader sceneLoader, IGameFactory gameFactory,
            IUIFactory uiFactory, IInputService inputService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _inputService = inputService;
            _permanentLevel = new PermanentLevelSystem(gameFactory);
        }

        public void Enter() => _sceneLoader.LoadScene(GameScene, PrepareGame);

        private void PrepareInput()
        {
            _inputService.OnDragStart += _rocket.EnableFly;
            _inputService.OnDragEnd += _rocket.DisableFly;
            _inputService.OnDrag += AddForceForRocket;
            _inputService.Enable();
        }

        public void Exit()
        {
            _inputService.OnDragStart -= _rocket.EnableFly;
            _inputService.OnDragEnd -= _rocket.DisableFly;
            _inputService.OnDrag -= AddForceForRocket;
            _inputService.Disable();
            _permanentLevel.Dispose();
            _rocket.OnExplode -= _gameOverWindow.Show;
            _gameOverWindow.OnRetryClick -= RetryGame;
        }

        private void PrepareGame()
        {
            CreateUI();
            CreateRocket();
            PreparePermanentLevel();
            PrepareInput();
            CreateVirtualCamera();
        }

        private void PreparePermanentLevel()
        {
            _permanentLevel.SetStarterParts();
            _permanentLevel.SetRocketView(_rocket);
        }

        private void CreateRocket()
        {
            _rocket = _gameFactory.CreateRocket();
            _rocket.OnExplode += _gameOverWindow.Show;
        }

        private void CreateUI()
        {
            Transform canvas = _uiFactory.CreateRootCanvas().transform;
            _gameOverWindow = _uiFactory.CreateGameOverWindow(canvas);
            _gameOverWindow.OnRetryClick += RetryGame;
        }

        private void CreateVirtualCamera()
        {
            CinemachineVirtualCamera playerVirtualCamera = _gameFactory.CreatePlayerVirtualCamera();
            playerVirtualCamera.Follow = _rocket.transform;
        }

        private void AddForceForRocket(float screenX) =>
            _rocket.transform.Rotate(Vector3.forward, screenX / 3 * -Time.deltaTime);

        private void RetryGame()
        {
            _gameOverWindow.Hide();
            _stateMachine.Enter<GameplayState>();
        }
    }
}