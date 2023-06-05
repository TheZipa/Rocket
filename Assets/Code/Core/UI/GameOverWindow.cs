using System;
using Code.Services.EntityContainer;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Core.UI
{
    public class GameOverWindow : UIElement, IFactoryEntity
    {
        public event Action OnRetryClick;
        
        [SerializeField] private Button _retryButton;
        [SerializeField] private Animator _animator;

        private readonly int _showAnimationHash = Animator.StringToHash("Show");

        private void Start()
        {
            _retryButton.onClick.AddListener(SendRetry);
            Hide();
        }

        private void SendRetry() => OnRetryClick?.Invoke();

        private void OnDestroy() => _retryButton.onClick.RemoveListener(SendRetry);
    }
}