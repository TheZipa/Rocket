using System.Collections;
using UnityEngine;

namespace Code.Core.Rocket
{
    public class RocketMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        private RocketFuel _fuel;
        private Coroutine _overclockRoutine;
        private float _maxSpeed;
        private float _currentSpeed;

        public void Construct(RocketFuel fuel, float maxSpeed)
        {
            _fuel = fuel;
            _maxSpeed = maxSpeed;
            _rigidbody.velocity = new Vector3(0, _maxSpeed, 0);
        }

        public void Enable()
        {
            _currentSpeed = _rigidbody.velocity.magnitude;
            _rigidbody.isKinematic = true;
            _fuel.DisableRestore();
            _overclockRoutine = StartCoroutine(Overclock());
        }

        public void Disable()
        {
            _rigidbody.isKinematic = false;
            _rigidbody.velocity = transform.up * _currentSpeed;
            _fuel.EnableRestore();
            if(_overclockRoutine != null) StopCoroutine(_overclockRoutine);
        }

        public void Move(float angle)
        {
            transform.rotation *= Quaternion.Euler(0, 0, angle);
            transform.position += transform.up * _currentSpeed * Time.deltaTime;
            _fuel.ConsumeFuel();
        }
        
        private void FixedUpdate()
        {
            if (!_rigidbody.isKinematic) return;
            _rigidbody.MovePosition(transform.position);
            _rigidbody.MoveRotation(transform.rotation);
        }
        
        private IEnumerator Overclock()
        {
            while (_currentSpeed < _maxSpeed)
            {
                yield return null;
                _currentSpeed += Time.deltaTime * 10;
            }

            _currentSpeed = _maxSpeed;
        }
    }
}