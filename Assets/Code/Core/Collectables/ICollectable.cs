using Code.Data.Enums;
using UnityEngine;

namespace Code.Core.Collectables
{
    public abstract class CollectableItem : MonoBehaviour
    {
        public Collider Collider;
        public CollectableType CollectableType;

        public virtual void Collect() => Collider.enabled = false;

        public virtual void Refresh() => Collider.enabled = true;
    }
}