using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Core.UI
{
    public class GameOverWindow : MonoBehaviour
    {
        public event Action OnRetryClick;
        
        [SerializeField] private Button _retryButton;
        [SerializeField] private Animator _animator;

        private readonly int _showAnimationHash = Animator.StringToHash("Show");

        public void Show()
        {
            gameObject.SetActive(true);
            //_animator.SetTrigger(_showAnimationHash);
        }

        public void Hide() => gameObject.SetActive(false);

        private void Start() => _retryButton.onClick.AddListener(SendRetry);

        private void SendRetry() => OnRetryClick?.Invoke();

        private void OnDestroy() => _retryButton.onClick.RemoveListener(SendRetry);
    }
}