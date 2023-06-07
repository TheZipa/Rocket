using Code.Services.EntityContainer;
using TMPro;
using UnityEngine;

namespace Code.Core.UI.Gameplay
{
    public class MeterCounterView : UIElement, IFactoryEntity
    {
        [SerializeField] private TextMeshProUGUI _meters;

        public void SetMeters(float meters) => _meters.text = $"{meters}m";
    }
}