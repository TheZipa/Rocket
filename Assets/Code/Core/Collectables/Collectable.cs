using Code.Data.Enums;
using UnityEngine;

namespace Code.Core.Collectables
{
    public abstract class CollectableItem : MonoBehaviour
    {
        public Collider Collider;
        public CollectableType CollectableType;
        [SerializeField] protected MeshRenderer _mesh;

        public virtual void Collect() => _mesh.enabled = Collider.enabled = false;

        public virtual void Refresh() => _mesh.enabled = Collider.enabled = true;
    }
}