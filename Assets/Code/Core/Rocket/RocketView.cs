using UnityEngine;

namespace Code.Core.Rocket
{
    public class RocketView : MonoBehaviour
    {
        [SerializeField] private TrailRenderer _trail;
        [SerializeField] private GameObject _model;

        public void EnableTrail() => _trail.emitting = true;

        public void DisableTrail() => _trail.emitting = false;

        public void Hide() => _model.SetActive(false);

        public void Show() => _model.SetActive(true);
    }
}