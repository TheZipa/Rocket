using System;
using UnityEngine;

namespace Code.Core.Rocket
{
    public class Rocket : MonoBehaviour
    {
        public event Action OnExplode;
        public event Action OnUpdate;

        [SerializeField] private TrailRenderer _trail;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private RocketExplosion _explosionEffect;
        [SerializeField] private GameObject _view;
        
        private const float MaxYVelocity = 22.5f;
        private const float StartVelocity = 6f;
        private const float ForceCoefficient = 16f;

        public void EnableTrail() => _trail.emitting = true;

        public void DisableTrail() => _trail.emitting = false;

        public void AddVerticalForce()
        {
            if (_rigidbody.velocity.y >= MaxYVelocity) return;
            _rigidbody.AddForce(transform.up / ForceCoefficient, ForceMode.Impulse);
        }

        private void Start() => _rigidbody.velocity = new Vector3(0, StartVelocity, 0);

        private void Update() => OnUpdate?.Invoke();

        private void OnCollisionEnter(Collision collision)
        {
            _view.SetActive(false);
            _explosionEffect.Show();
            _rigidbody.isKinematic = true;
            OnExplode?.Invoke();
        }
    }
}