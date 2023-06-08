using UnityEngine;

namespace Code.Core.Collectables
{
    public class Coin : CollectableItem
    {
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private ParticleSystem _collectEffect;
        [SerializeField] private MeshRenderer _mesh;
        
        public override void Collect()
        {
            base.Collect();
            _collectEffect.Play();
            _mesh.enabled = false;
        }

        public override void Refresh()
        {
            base.Refresh();
            _mesh.enabled = true;
        }

        private void Update() => transform.Rotate(Vector3.forward, Time.deltaTime * _rotateSpeed);
    }
}