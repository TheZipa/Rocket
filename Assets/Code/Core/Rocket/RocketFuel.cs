using System;
using System.Collections;
using UnityEngine;

namespace Code.Core.Rocket
{
    public class RocketFuel
    {
        public event Action<float> OnFuelChanged;
        public event Action OnFuelEmpty;

        public float FuelValue
        {
            get => _fuel;
            private set
            {
                _fuel = value;
                OnFuelChanged?.Invoke(FuelValue);
            }
        }

        private readonly MonoBehaviour _monoBehaviour;
        private readonly float _maxFuel, _consumeCoefficient;
        private readonly float _restoreDelay;
        private readonly float _restoreCoefficient;
        private float _fuel;
        private Coroutine _restoreRoutine;

        public RocketFuel(MonoBehaviour monoBehaviour, float maxFuel, float consumeCoefficient, float restoreDelay)
        {
            _monoBehaviour = monoBehaviour;
            FuelValue = _maxFuel = maxFuel;
            _consumeCoefficient = consumeCoefficient;
            _restoreDelay = restoreDelay;
            _restoreCoefficient = consumeCoefficient * 1.75f;
        }

        public void EnableRestore()
        {
            DisableRestore();
            _restoreRoutine = _monoBehaviour.StartCoroutine(RestoreFuel());
        }

        public void DisableRestore()
        {
            if (_restoreRoutine == null) return;
            _monoBehaviour.StopCoroutine(_restoreRoutine);
            _restoreRoutine = null;
        }

        public void ConsumeFuel()
        {
            FuelValue -= _consumeCoefficient * Time.deltaTime;
            if (FuelValue > 0) return;
            FuelValue = 0;
            OnFuelEmpty?.Invoke();
        }

        public void RestoreFuelToMax() => FuelValue = _maxFuel;

        private IEnumerator RestoreFuel()
        {
            yield return new WaitForSeconds(_restoreDelay);
            while (true)
            {
                yield return null;
                if (FuelValue >= _maxFuel)
                {
                    FuelValue = _maxFuel;
                    break;
                }
                FuelValue += _restoreCoefficient * Time.deltaTime;
            }
        }
    }
}