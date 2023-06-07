using System;
using Code.Services.EntityContainer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Core.UI.Gameplay
{
    public class GameOverWindow : UIElement, IFactoryEntity
    {
        public event Action OnRetryClick;
        
        [SerializeField] private Button _retryButton;
        [SerializeField] private TextMeshProUGUI _meters;
        [SerializeField] private Animator _animator;

        private readonly int _showAnimationHash = Animator.StringToHash("Show");

        public void SetMeters(float meters) => _meters.text = $"{meters}m";

        private void Awake() => _retryButton.onClick.AddListener(SendRetry);

        private void SendRetry() => OnRetryClick?.Invoke();

        private void OnDestroy() => _retryButton.onClick.RemoveListener(SendRetry);
    }
}