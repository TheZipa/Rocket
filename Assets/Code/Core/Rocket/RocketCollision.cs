using System;
using UnityEngine;

namespace Code.Core.Rocket
{
    public class RocketCollision : MonoBehaviour
    {
        public event Action OnExplode;
        public event Action<Collider> OnCollect;

        public bool IsEnabled
        {
            get => _collider.enabled;
            set => _collider.enabled = value;
        }
        
        [SerializeField] private RocketExplosion _explosionEffect;
        [SerializeField] private Collider _collider;
        
        private const string CollectableTag = "Collectable";
        
        private void OnCollisionEnter(Collision collision)
        {
            if (_explosionEffect.IsExploded) return;
            OnExplode?.Invoke();
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag(CollectableTag) == false) return;
            OnCollect?.Invoke(collider);
        }
    }
}