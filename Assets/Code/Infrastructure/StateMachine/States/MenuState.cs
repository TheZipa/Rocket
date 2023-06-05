using Code.Core.UI.Menu;
using Code.Infrastructure.StateMachine.GameStateMachine;
using Code.Services.EntityContainer;

namespace Code.Infrastructure.StateMachine.States
{
    public class MenuState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IEntityContainer _entityContainer;

        private MainMenu _mainMenu;

        public MenuState(IGameStateMachine stateMachine, IEntityContainer entityContainer)
        {
            _stateMachine = stateMachine;
            _entityContainer = entityContainer;
        }

        public void Enter()
        {
            _mainMenu = _entityContainer.GetEntity<MainMenu>();
            _mainMenu.Show();
            _mainMenu.OnPlayClick += SwitchToGameplay;
        }

        public void Exit()
        {
            _mainMenu.Hide();
            _mainMenu.OnPlayClick -= SwitchToGameplay;
        }

        private void SwitchToGameplay() => _stateMachine.Enter<GameplayState>();
    }
}