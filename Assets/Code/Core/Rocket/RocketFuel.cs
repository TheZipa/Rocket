using System;
using System.Collections;
using UnityEngine;

namespace Code.Core.Rocket
{
    public class RocketFuel
    {
        public event Action<float> OnFuelChanged;
        public event Action OnFuelEmpty;

        public float Fuel
        {
            get => _fuel;
            private set
            {
                _fuel = value;
                OnFuelChanged?.Invoke(Fuel);
            }
        }

        private readonly MonoBehaviour _monoBehaviour;
        private readonly float _maxFuel, _consumeCoefficient, _restoreCoefficient;
        private float _fuel;
        private Coroutine _restoreRoutine;

        public RocketFuel(MonoBehaviour monoBehaviour, float maxFuel, float consumeCoefficient)
        {
            _monoBehaviour = monoBehaviour;
            Fuel = _maxFuel = maxFuel;
            _consumeCoefficient = consumeCoefficient;
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

        public void ConsumeFuel() => Fuel -= _consumeCoefficient * Time.deltaTime;

        public void RestoreFuelToMax() => Fuel = _maxFuel;

        private IEnumerator RestoreFuel()
        {
            while (true)
            {
                yield return null;
                if (Fuel >= _maxFuel)
                {
                    Fuel = _maxFuel;
                    continue;
                }
                Fuel += _restoreCoefficient * Time.deltaTime;
            }
        }
    }
}