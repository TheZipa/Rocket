using System;
using System.Collections;
using Code.Data.StaticData;
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

        public RocketFuel Fuel { get; private set; }
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private RocketExplosion _explosionEffect;
        [SerializeField] private RocketMovement _movement;
        [SerializeField] private RocketCollision _collision;
        [SerializeField] private RocketView _view;

        public void Construct(RocketData rocketData)
        {
            Fuel = new RocketFuel(this, rocketData.MaxFuel, rocketData.ConsumeCoefficient, rocketData.RestoreDelay);
            Fuel.OnFuelChanged += SendFuelChange;
            Fuel.OnFuelEmpty += DisableFly;
            _movement.Construct(Fuel, rocketData.MaxRocketSpeed);
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
            Fuel.DisableRestore();
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
            Fuel.RestoreFuelToMax();
            onLaunched?.Invoke();
        }

        private void SendCollect(Collider collectableCollider) => OnCollect?.Invoke(collectableCollider);

        private void SendFuelChange(float fuel) => OnFuelChanged?.Invoke(fuel);

        private void Update() => OnUpdate?.Invoke();

        private void OnDestroy()
        {
            _collision.OnExplode -= Explode;
            _collision.OnCollect -= SendCollect;
            Fuel.OnFuelChanged -= SendFuelChange;
            Fuel.OnFuelEmpty -= DisableFly;
        }
    }
}