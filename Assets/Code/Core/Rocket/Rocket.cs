using System;
using System.Collections;
using Code.Services.EntityContainer;
using UnityEngine;

namespace Code.Core.Rocket
{
    public class Rocket : MonoBehaviour, IFactoryEntity
    {
        public event Action<Collider> OnCollect;
        public event Action OnExplode;
        public event Action OnUpdate;

        [SerializeField] private TrailRenderer _trail;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private RocketExplosion _explosionEffect;
        [SerializeField] private RocketMovement _movement;
        [SerializeField] private Collider _collider;
        [SerializeField] private GameObject _view;

        private const string CollectableTag = "Collectable";
        private bool _isExploded;

        public void Construct(float maxSpeed) => _movement.Construct(maxSpeed);

        public void Launch(float launchTime, Action onLaunched = null)
        {
            _isExploded = false;
            StartCoroutine(StartLaunch(launchTime, onLaunched));
        }

        private IEnumerator StartLaunch(float launchTime, Action onLaunched)
        {
            EnableFly();
            float flyTime = 0f;
            while (flyTime < launchTime)
            {
                Move(0);
                flyTime += Time.deltaTime;
                yield return null;
            }
            DisableFly();
            _collider.enabled = true;
            onLaunched?.Invoke();
        }

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
            _isExploded = _rigidbody.isKinematic = true;
            OnExplode?.Invoke();
        }

        private void Start()
        {
            _rigidbody.isKinematic = true;
            _collider.enabled = false;
        }

        private void Update() => OnUpdate?.Invoke();

        private void OnCollisionEnter(Collision collision)
        {
            if (_isExploded) return;
            Explode();
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag(CollectableTag) == false) return;
            OnCollect?.Invoke(collider);
        }
    }
}