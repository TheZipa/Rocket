using UnityEngine;

namespace Code.Core.UI
{
    public abstract class UIElement : MonoBehaviour
    {
        public virtual void Show() => gameObject.SetActive(true);
        
        public virtual void Hide() => gameObject.SetActive(false);
    }
}