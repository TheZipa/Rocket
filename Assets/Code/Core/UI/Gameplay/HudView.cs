using Code.Services.EntityContainer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Core.UI.Gameplay
{
    public class HudView : UIElement, IFactoryEntity
    {
        [SerializeField] private Slider _fuelSlider;
        [SerializeField] private TextMeshProUGUI _meters;
        
        public void Construct(float maxFuel) => _fuelSlider.maxValue = maxFuel;
        
        public void SetMeters(float meters) => _meters.text = $"{meters}m";

        public void SetFuel(float fuel) => _fuelSlider.value = fuel;
    }
}