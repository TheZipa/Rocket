using Code.Services.EntityContainer;
using UnityEngine;

namespace Code.Core.Camera
{
    public class LevelCamera : MonoBehaviour, IFactoryEntity
    {
        private Rocket.Rocket _rocket;
        private float _screenOffset;
        private bool _isTrackingEnabled;

        public void Construct(Rocket.Rocket rocket, float offset)
        {
            _rocket = rocket;
            _screenOffset = offset;
        }

        public void EnableRocketTracking() => _isTrackingEnabled = true;

        public void DisableRocketTracking() => _isTrackingEnabled = false;

        private void LateUpdate()
        {
            if (!_isTrackingEnabled) return;

            if (_rocket.transform.position.y < transform.position.y - _screenOffset) ExplodeRocket();
            if (_rocket.transform.position.y > transform.position.y) SetYPositionFromRocket();
        }

        private void ExplodeRocket()
        {
            _isTrackingEnabled = false;
            _rocket.Explode();
        }

        private void SetYPositionFromRocket()
        {
            Vector3 cameraPosition = transform.position;
            cameraPosition.y = _rocket.transform.position.y;
            transform.position = cameraPosition;
        }
    }
}