using System;
using Code.Services.EntityContainer;
using UnityEngine;

namespace Code.Core.Rocket
{
    public class Rocket : MonoBehaviour, IFactoryEntity
    {
        public event Action OnExplode;
        public event Action OnUpdate;

        [SerializeField] private TrailRenderer _trail;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private RocketExplosion _explosionEffect;
        [SerializeField] private RocketMovement _movement;
        [SerializeField] private Collider _collider;
        [SerializeField] private GameObject _view;

        public void Construct(float maxSpeed) => _movement.Construct(maxSpeed);

        public void Activate() => _collider.enabled = true;

        public void EnableFly()
        {
            _trail.emitting = true;
            _movement.Enable();
        }

        public void DisableFly()
        {
            _trail.emitting = false;
            _movement.Disable();
        }

        public void Move(float angle) => _movement.Move(angle);

        public void Explode()
        {
            _view.SetActive(false);
            _explosionEffect.Show();
            _movement.Disable();
            _rigidbody.isKinematic = true;
            OnExplode?.Invoke();
        }

        private void Start()
        {
            _rigidbody.isKinematic = true;
            _collider.enabled = false;
        }

        private void Update() => OnUpdate?.Invoke();

        private void OnCollisionEnter(Collision collision) => Explode();
    }
}