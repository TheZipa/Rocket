using System;
using Code.Core.UI.Gameplay;
using Code.Services.EntityContainer;

namespace Code.Core.MeterCounter
{
    public class MeterCounterSystem : IFactoryEntity
    {
        public float Meters { get; private set; }
        
        private readonly MeterCounterView _counterView;
        private readonly Rocket.Rocket _rocket;
        private float _lastRocketPosition;

        public MeterCounterSystem(MeterCounterView counterView, Rocket.Rocket rocket)
        {
            _counterView = counterView;
            _rocket = rocket;
            counterView.Hide();
        }

        public void StartCounting()
        {
            Meters = 0;
            _lastRocketPosition = _rocket.transform.position.y;
            _rocket.OnUpdate += CompareRocketRaise;
            _counterView.Show();
        }

        public void StopCounting()
        {
            _rocket.OnUpdate -= CompareRocketRaise;
            _counterView.Hide();
        }

        private void CompareRocketRaise()
        {
            float newRocketPosition = _rocket.transform.position.y;
            float difference = newRocketPosition - _lastRocketPosition;
            if (difference <= 0) return; 
            IncreaseCurrentMeters(difference, newRocketPosition);
        }

        private void IncreaseCurrentMeters(float difference, float newRocketPosition)
        {
            Meters = (float) Math.Round(Meters + difference, 2);
            _counterView.SetMeters(Meters);
            _lastRocketPosition = newRocketPosition;
        }
    }
}