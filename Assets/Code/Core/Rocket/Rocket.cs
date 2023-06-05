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
        [SerializeField] private RocketMovement _movement;
        [SerializeField] private GameObject _view;
        
        private const float StartVelocity = 6f;

        public void Construct(float maxSpeed) => _movement.Construct(maxSpeed);

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

        private void Start() => _rigidbody.velocity = new Vector3(0, StartVelocity, 0);

        private void Update() => OnUpdate?.Invoke();

        private void OnCollisionEnter(Collision collision) => Explode();
    }
}