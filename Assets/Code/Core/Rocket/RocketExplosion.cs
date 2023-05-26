using System.Collections;
using UnityEngine;

namespace Code.Core.Rocket
{
    public class RocketExplosion : MonoBehaviour
    {
        [SerializeField] private Light _light;

        public void Show()
        {
            gameObject.SetActive(true);
            StartCoroutine(FadeLight());
        }

        private IEnumerator FadeLight()
        {
            while (_light.intensity > 0)
            {
                _light.intensity -= 0.2f;
                yield return null;
            }
            _light.enabled = false;
        }
    }
}