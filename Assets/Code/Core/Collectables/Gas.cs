using UnityEngine;

namespace Code.Core.Collectables
{
    public class Gas : CollectableItem
    {
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private ParticleSystem _collectEffect;

        public override void Collect()
        {
            base.Collect();
            _collectEffect.Play();
        }
        
        private void Update() => transform.Rotate(Vector3.forward, Time.deltaTime * _rotateSpeed);
    }
}