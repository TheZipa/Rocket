using System;
using System.Collections;
using Code.Services.EntityContainer;
using UnityEngine;

namespace Code.Core.Rocket
{
    public class Rocket : MonoBehaviour, IFactoryEntity
    {
        public event Action<Collider> OnCollect;
        public event Action<float> OnFuelChanged;
        public event Action OnExplode;
        public event Action OnUpdate;
        
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private RocketExplosion _explosionEffect;
        [SerializeField] private RocketMovement _movement;
        [SerializeField] private RocketCollision _collision;
        [SerializeField] private RocketView _view;
        private RocketFuel _fuel;

        public void Construct(float maxSpeed, float maxFuel, float consumeCoefficient)
        {
            _fuel = new RocketFuel(this, maxFuel, consumeCoefficient);
            _fuel.OnFuelEmpty += DisableFly;
            _fuel.OnFuelChanged += SendFuelChange;
            _movement.Construct(_fuel, maxSpeed);
        }

        public void Launch(float launchTime, Action onLaunched = null)
        {
            _explosionEffect.IsExploded = false;
            StartCoroutine(StartLaunch(launchTime, onLaunched));
        }

        public void EnableFly()
        {
            _view.EnableTrail();
            _movement.Enable();
        }

        public void DisableFly()
        {
            _view.DisableTrail();
            _movement.Disable();
        }

        public void Move(float angle) => _movement.Move(angle);

        public void Explode()
        {
            _view.Hide();
            _explosionEffect.Show();
            _movement.Disable();
            _fuel.DisableRestore();
            _rigidbody.isKinematic = true;
            OnExplode?.Invoke();
        }

        private void Awake()
        {
            _rigidbody.isKinematic = true;
            _collision.IsEnabled = false;
            _collision.OnExplode += Explode;
            _collision.OnCollect += SendCollect;
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
            _collision.IsEnabled = true;
            _fuel.RestoreFuelToMax();
            onLaunched?.Invoke();
        }

        private void SendCollect(Collider collectableCollider) => OnCollect?.Invoke(collectableCollider);

        private void SendFuelChange(float fuel)
        {
            OnFuelChanged?.Invoke(fuel);
            Debug.Log("Fuel changed - " + fuel);
        }

        private void Update() => OnUpdate?.Invoke();

        private void OnDestroy()
        {
            _collision.OnExplode -= Explode;
            _collision.OnCollect -= SendCollect;
            _fuel.OnFuelEmpty -= DisableFly;
            _fuel.OnFuelChanged -= SendFuelChange;
        }
    }
}